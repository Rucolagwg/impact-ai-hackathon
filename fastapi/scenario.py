import requests
from bs4 import BeautifulSoup
import google.generativeai as genai
import time
import json
import re
import argparse
from datetime import datetime

# ✅ Gemini API 키 설정
genai.configure(api_key="")

# ✅ Gemini 시스템 명령
scenario_model = genai.GenerativeModel(
    "gemini-2.0-flash",
    system_instruction="""
당신은 경제 시뮬레이션 게임의 시나리오 평가자이자 설계자입니다.

이 게임의 플레이어는 대학생이며, 뉴스 기사를 읽고 본인의 자산을 어떻게 운용할지 결정해야 합니다.

다음 형식을 따라 뉴스가 게임 시나리오로 적합한지 평가하고, 적합하다면 요약, 선택지 4개, 결과 4개, 그리고 각 선택지에 대한 결과의 "근거"를 3~4문장으로 설명하여 제공합니다.

형식:
[적합 여부]: Yes 또는 No  
[요약]: 뉴스 내용을 10문장이내로 요약 (경제 흐름 중심)  
[선택지]: 플레이어가 선택할 수 있는 자산 운용 방향 4가지  
[결과]: 각 선택지에 대한 게임 결과를 뉴스 이후 실제 흐름을 반영해 단정적으로 작성 (예: +5% 수익, -3% 손실, 0% 변화)  
[근거]: 각 결과가 도출된 원인을 3~4문장으로 설명 (시장 반응, 정책 변화, 관련 기업 주가 등)

주의:
- "결과"는 조건적 표현 없이 반드시 단정적으로 작성 ("~할 수 있음" ❌)
- 실제 기사 날짜 이후 자산 흐름을 근거로 작성
- 예시로 주식에 관한 선택지에 대한 결과는 실제 그 주식이 뉴스 시간 이후로 어떻게 변동되었는지 확인하여 결과 작성
- "근거"는 뉴스 내용 또는 실제 시장 흐름, 종목, 지표 등을 기반으로 설명
"""
)

# ✅ Step 1: 특정 page부터 크롤링

def crawl_naver_economy_news_from_page(start_page=1, max_articles=20):
    base_url = "https://news.naver.com/section/101"
    headers = {"User-Agent": "Mozilla/5.0"}
    articles = []
    page = start_page

    while len(articles) < max_articles:
        url = f"{base_url}?page={page}"
        res = requests.get(url, headers=headers)
        soup = BeautifulSoup(res.text, "html.parser")
        article_elements = soup.select("ul.sa_list li div.sa_text a.sa_text_title")

        if not article_elements:
            print("🔚 더 이상 기사를 찾을 수 없습니다.")
            break

        for a in article_elements:
            title = a.get_text(strip=True)
            link = a["href"]
            body, date = extract_article_body_and_date(link, headers)
            articles.append({"title": title, "link": link, "body": body, "date": date})
            if len(articles) >= max_articles:
                break

        page += 1

    return articles

# ✅ Step 2: 본문 및 날짜 추출
def extract_article_body_and_date(link, headers):
    try:
        res = requests.get(link, headers=headers)
        soup = BeautifulSoup(res.text, "html.parser")
        content = soup.select_one("div#newsct_article")
        date_tag = soup.select_one("span.media_end_head_info_datestamp_time")
        body = content.get_text(strip=True) if content else "본문 추출 실패"
        date_text = date_tag.get_text(strip=True) if date_tag else "날짜 없음"
        return body, date_text
    except:
        return "본문 요청 실패", "날짜 없음"

# ✅ Step 3: 수익률 수치 추출
def extract_returns(result_text):
    match = re.search(r'([+-]?\d+(\.\d+)?)\s*[%퍼센트]?', result_text)
    if match:
        return float(match.group(1))
    return 0.0

# ✅ Step 4: Gemini 분석

def evaluate_news_for_game(news):
    prompt = f"제목: {news['title']}\n날짜: {news['date']}\n본문: {news['body'][:1000]}..."
    try:
        response = scenario_model.generate_content(prompt)
        text = response.text.strip()

        if "[적합 여부]: Yes" not in text:
            return None

        summary_match = re.search(r"\[요약\]:(.*?)(\[선택지\]:|$)", text, re.DOTALL)
        summary = summary_match.group(1).strip() if summary_match else ""

        choices_match = re.findall(r"\[선택지\]:([\s\S]*?)\[결과\]:", text)
        results_match = re.findall(r"\[결과\]:([\s\S]*?)\[근거\]:", text)
        reasons_match = re.findall(r"\[근거\]:([\s\S]*)", text)

        def extract_numbered_list(section):
            return [
                line.strip()[3:].strip() for line in section.strip().split("\n")
                if line.strip().startswith(tuple(f"{i}." for i in range(1, 5)))
            ]

        choices = extract_numbered_list(choices_match[0]) if choices_match else []
        results = extract_numbered_list(results_match[0]) if results_match else []
        reasons = extract_numbered_list(reasons_match[0]) if reasons_match else []
        returns = [extract_returns(r) for r in results]

        if len(choices) != 4 or len(results) != 4 or len(reasons) != 4:
            print("⚠️ 선택지/결과/근거가 4개가 아님 → 생략")
            return None

        return {
            "title": news["title"],
            "link": news["link"],
            "date": news["date"],
            "summary": summary,
            "choices": choices,
            "results": results,
            "reasons": reasons,
            "returns": returns
        }

    except Exception as e:
        print(f"⚠️ Gemini 오류 발생: {e}")
        return None

# ✅ Step 5: 시나리오 처리 및 저장
def select_game_scenarios(start_page=1, save_path="scenarios_page1.json", articles_per_page=20):
    print(f"📡 Page {start_page}부터 기사 {articles_per_page}개 크롤링 중...")
    all_news = crawl_naver_economy_news_from_page(start_page, max_articles=articles_per_page)

    print("🤖 Gemini 시나리오 분석 중...")
    selected = []
    for news in all_news:
        result = evaluate_news_for_game(news)
        if result:
            selected.append(result)
        time.sleep(5)

    with open(save_path, "w", encoding="utf-8") as f:
        json.dump(selected, f, ensure_ascii=False, indent=2)
    print(f"✅ {len(selected)}개 시나리오가 '{save_path}'에 저장되었습니다.")
    return selected

# ✅ 실행 예시 (터미널에서 페이지 인자로 실행 가능)
if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--page", type=int, default=1, help="시작할 페이지 번호")
    parser.add_argument("--save", type=str, default="scenarios_page1.json", help="저장할 JSON 파일명")
    parser.add_argument("--count", type=int, default=20, help="수집할 기사 수")
    args = parser.parse_args()

    select_game_scenarios(start_page=args.page, save_path=args.save, articles_per_page=args.count)

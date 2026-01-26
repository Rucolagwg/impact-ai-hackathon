# Impact_AI_Hackathon

# 💰 [프로젝트 이름: 예) EcoLife / MoneyMaster]
> **청년들의 경제 무관심 해결을 위한 AI 기반 경제 시뮬레이션 게임**  
> **AI-Powered Economic Simulation Game to Solve Economic Apathy Among Youth**

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Unity](https://img.shields.io/badge/Unity-2021.3%2B-black.svg)](https://unity.com/)
[![Gemini](https://img.shields.io/badge/AI-Google%20Gemini-8E75B2.svg)](https://deepmind.google/technologies/gemini/)

---

## 📖 Table of Contents
1. [🇰🇷 프로젝트 소개 (Korean)](#-프로젝트-소개-korean)
2. [🇺🇸 Project Description (English)](#-project-description-english)

---

## 🇰🇷 프로젝트 소개

### 1. 배경 및 목적 (Background)
오늘날 많은 청년들이 복잡한 경제 용어와 시스템에 어려움을 느껴 경제 뉴스에 무관심해지는 경향이 있습니다.  
이 프로젝트는 **GDGoC GIST Impact AI Hackathon**에서 시작되었으며, 사용자가 게임을 통해 **실제 경제 뉴스를 접하고**, AI의 피드백을 통해 자산 관리의 중요성을 학습할 수 있도록 개발되었습니다.

### 2. 주요 기능 및 작동 원리 (Key Features & Mechanism)
*   **실시간 경제 뉴스 크롤링:** 웹상의 최신 경제 뉴스 헤드라인과 본문을 크롤링하여 게임 내 '뉴스 피드'로 가져옵니다.
*   **Gemini API 기반 분석:** 가져온 뉴스를 **Google Gemini API**에 전달하여, 현재 경제 상황(호재/악재)을 분석하고 게임 내 시장(주가, 금리 등)에 반영합니다.
*   **AI 투자 피드백:** 사용자가 뉴스에 대해 내린 결정(매수/매도/관망)이나 작성한 의견을 Gemini가 분석하여, 해당 선택이 논리적인지 혹은 위험한지 **개인화된 피드백**을 제공합니다.
*   **몰입형 시뮬레이션 (Unity):** Unity 엔진을 활용하여 시각적으로 직관적인 UI와 인터랙티브한 게임 환경을 구축했습니다.

### 3. 기술 스택 (Tech Stack)
*   **Game Engine:** Unity (C#)
*   **AI & Logic:** Google Gemini API (LLM), Python (Data Analysis)
*   **Crawling:** BeautifulSoup / Selenium
*   **Collaboration:** Git, Notion

### 4. 시스템 아키텍처 (System Architecture)
1.  **Crawler:** 주요 경제 뉴스 사이트에서 기사 데이터 수집
2.  **AI Server:** Gemini API를 호출하여 뉴스 요약 및 감성 분석 (Sentiment Analysis)
3.  **Unity Client:** 분석된 데이터를 바탕으로 게임 내 자산 가치 변동 및 UI 업데이트

---

## 🇺🇸 Project Description

### 1. Introduction
Many young people today struggle with complex economic terms, leading to indifference toward economic news.
Developed during the **GDGoC GIST Impact AI Hackathon**, this project is an AI-powered simulation game built with **Unity**. It helps users understand economic flows by interacting with **real-world news analyzed by Gemini API**.

### 2. Key Features & Mechanism
*   **Real-time News Crawling:** Crawls headlines and articles from major economic news websites to populate the in-game news feed.
*   **Gemini API Integration:** Utilizes **Google Gemini API** to analyze the sentiment of the crawled news and adjust in-game market indicators (stock prices, interest rates) accordingly.
*   **AI-Driven Feedback:** When a user makes an investment decision or responds to a news event, the AI analyzes their logic and provides **personalized feedback** on whether the decision was sound or risky.
*   **Immersive Environment (Unity):** Built on the Unity engine to provide a visually intuitive and interactive user experience.

### 3. Tech Stack
*   **Game Engine:** Unity (C#)
*   **AI & Logic:** Google Gemini API (LLM), Python (Data Analysis)
*   **Crawling:** BeautifulSoup / Selenium
*   **Collaboration:** Git, Notion

### 4. System Architecture
1.  **Crawler:** Collects article data from economic news platforms.
2.  **AI Server:** Calls Gemini API to summarize news and perform sentiment analysis.
3.  **Unity Client:** Updates asset values and UI based on the analyzed data.

---

## 👥 팀원 (Contributors)
*   **HuiJung (Unity):**
*   **SungHoon (AI):**
*   **Seulgi (Design):**

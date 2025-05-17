using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Game : EventBase
{

    bool isGameEnd = false;

    bool isFadeInEnd = false;

    NewsItem currentNewsItem;
    NewsItem previousNewsItem;
    int previousChoice = 0;

    public override void Enter(GameManager gm)
    {
        currentNewsItem = NewsManager.Instance.gameNewsList[gm.currentEventNum];
        previousNewsItem = NewsManager.Instance.gameNewsList[gm.currentEventNum-1];

        if(gm.currentEventNum == 1)
        {
            // 첫 이벤트
            StartCoroutine(StartSetupFirst(gm));

        }
        else if( gm.currentEventNum == 12)
        {
            // 마지막 이벤트


        }
        else
        {
            // 일반
            StartCoroutine(StartSetupNormal(gm));

        }

        


    }

    public override void Execute(GameManager gm)
    {
        if (!isFadeInEnd)
            return;


        
    }

    public override void Exit(GameManager gm)
    {
        StartCoroutine(gm.fade.eFadeOut());
    }


    IEnumerator StartSetupFirst(GameManager gm)
    {
        yield return StartCoroutine(gm.fade.eFadeIN());

        isFadeInEnd = true;


        // 모든 UI 키기
        UIManager.Instance.AllUIOn();

        // 메인 뉴스 업데이트 하기
        UIManager.Instance.UpdateUpper(currentNewsItem.title, currentNewsItem.summary);

        // 선택 버튼 업데이트 하기
        UIManager.Instance.UpdateChoice(currentNewsItem.choices);

        // 돈 , 시간 업데이트 하기
        UIManager.Instance.UpdateResource(GameManager.Instance.Money, gm.currentEventNum);
    }


    IEnumerator StartSetupNormal(GameManager gm)
    {
        yield return StartCoroutine(gm.fade.eFadeIN());

        isFadeInEnd = true;


        // 모든 UI 끄기
        UIManager.Instance.AllUIOff();

        // 요약 리포트 띄우기
        int choice = gm.priousChoiceNumber;
        UIManager.Instance.UpdateSummary(previousNewsItem.results[choice], previousNewsItem.choices[choice], previousNewsItem.reasons[choice]);

        // 메인 뉴스 업데이트 하기
        UIManager.Instance.UpdateUpper(currentNewsItem.title, currentNewsItem.summary);

        // 선택 버튼 업데이트 하기
        UIManager.Instance.UpdateChoice(currentNewsItem.choices);

        // 돈 계산하기

        float percent = previousNewsItem.returns[choice];

        GameManager.Instance.Money = GameManager.Instance.Money + (GameManager.Instance.Money) * (percent / 100);


        // 캐릭터 리액션 하기
        Player.Instance.ChangeAni(percent);

        // 돈 , 시간 업데이트 하기
        UIManager.Instance.UpdateResource(GameManager.Instance.Money, gm.currentEventNum);


    }

}

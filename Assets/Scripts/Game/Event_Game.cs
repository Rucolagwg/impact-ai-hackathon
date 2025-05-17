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

        StartCoroutine(StartSetup(gm));
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
    
    IEnumerator StartSetup(GameManager gm)
    {
        yield return StartCoroutine(gm.fade.eFadeIN());

        isFadeInEnd = true;


        // 모든 UI 끄기
        UIManager.Instance.AllUIOff();

        // 요약 리포트 띄우기


        // 선택 버튼 업데이트 하기


        // 돈 계산하기

        // 캐릭터 리액션 하기

        // 돈 , 시간 업데이트 하기
   

    }

}

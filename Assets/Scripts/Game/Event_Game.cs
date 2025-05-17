using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Game : EventBase
{

    bool isFadeINDone = false;
    bool isFadeOutDone = false;
    bool isEnd = false;

    public override void Enter(GameManager gm)
    {
        StartCoroutine(FadeIN(gm));
    }

    public override void Execute(GameManager gm)
    {
        if (isFadeINDone)
            return;

        //---- 끝내는 함수 ----

        if(!isEnd)
        {
            isEnd = true;
            StartCoroutine(FadeOut(gm));
            return;
        }

        
    }

    public override void Exit(GameManager gm)
    {

    }

    IEnumerator FadeIN(GameManager gm)
    {
        yield return gm.fade.eFadeIN();

        isFadeINDone = true;

    }

    IEnumerator FadeOut(GameManager gm)
    {
        yield return gm.fade.eFadeOut();

        isFadeINDone = true;
        gm.NextEvent();
    }


}

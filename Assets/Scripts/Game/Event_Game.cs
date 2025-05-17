using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Game : EventBase
{

    bool isGameEnd = false;

    bool isFadeInEnd = false;

    public override void Enter(GameManager gm)
    {
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

    }

}

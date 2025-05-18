using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using DG.Tweening;

public class Event_Tuto : EventBase
{


    bool isEventEnd = false;

    public override void Enter(GameManager gm)
    {
        MasterAudio.PlaySound("MainBGM");

        DOVirtual.DelayedCall(4f, () =>
        {
            gm.NextEvent();
        });
        print("???????? ?????? ????");
    }

    public override void Execute(GameManager gm)
    {

        
    }

    public override void Exit(GameManager gm)
    {
        print("???????? ?????? ??");
        StartCoroutine(gm.fade.eFadeOut());
    }
}

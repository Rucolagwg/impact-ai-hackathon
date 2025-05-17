using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Tuto : EventBase
{
    public override void Enter(GameManager gm)
    {
        print("튜토리얼 이벤트 시작");
    }

    public override void Execute(GameManager gm)
    {
        gm.NextEvent();
        return;
    }

    public override void Exit(GameManager gm)
    {
        print("튜토리얼 이벤트 끝");

    }
}

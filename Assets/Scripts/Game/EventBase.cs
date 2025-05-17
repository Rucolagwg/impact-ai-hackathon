using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EventBase : MonoBehaviour
{

    public abstract void Enter(GameManager gm);
    public abstract void Execute(GameManager gm);
    public abstract void Exit(GameManager gm);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Animator animator;


    [ContextMenu("Victory")]
    public void Ani_Victory()
    {
        MasterAudio.PlaySound("Victory");
        animator.SetTrigger("Victory");
    }


    [ContextMenu("Defeat")]
    public void Ani_Defeat()
    {
        MasterAudio.PlaySound("Defeat");
        animator.SetTrigger("Defeat");
    }


    public void ChangeAni(float value)
    {

        if(value > 0)
        {
            Ani_Victory();
        }
        else if(value < 0)
        {
            Ani_Defeat();
        }
        else
        {

        }

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;


    [ContextMenu("Victory")]
    public void Ani_Victory()
    {
        animator.SetTrigger("Victory");
    }


    [ContextMenu("Defeat")]
    public void Ani_Defeat()
    {
        animator.SetTrigger("Defeat");
    }


}

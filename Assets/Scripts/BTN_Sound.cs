using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class BTN_Sound : MonoBehaviour
{
    public void BtnSound()
    {
        MasterAudio.PlaySound("button Click");
    }
}

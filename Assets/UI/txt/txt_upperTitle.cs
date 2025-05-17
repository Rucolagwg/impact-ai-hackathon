using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class txt_upperTitle : MonoBehaviour
{
    public string content;
    public TMP_Text text;

    void Start()
    {
        text.text = content;
    }

    void Update()
    {
        
    }
}

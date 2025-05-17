using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 오른쪽으로 이동하는 모
public class buttonRight : MonoBehaviour
{
    public GameObject button_obj;


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movezero()
    {
        button_obj.GetComponent<RectTransform>().DOAnchorPos(new Vector3(600,0,0), 2f).SetEase(Ease.InOutQuad);
    }


}

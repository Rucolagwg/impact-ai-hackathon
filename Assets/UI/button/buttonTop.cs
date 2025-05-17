using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class buttonTop : MonoBehaviour
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
        button_obj.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 4f).SetEase(Ease.InOutQuad);


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class buttonLeft : MonoBehaviour
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
        button_obj.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-800, 0, 0), 4f).SetEase(Ease.InOutQuad);
    }


}

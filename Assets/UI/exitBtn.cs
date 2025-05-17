using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class exitBtn : MonoBehaviour
{
    public GameObject exitBtnObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openFulltext()
    {
        exitBtnObj.SetActive(true);
        exitBtnObj.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.25f, 0.25f, 0.25f), 0.3f);
    }

    public void closeFullText()
    {
        exitBtnObj.SetActive(false);

    }
}

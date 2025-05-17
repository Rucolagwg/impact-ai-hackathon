using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class fullText : MonoBehaviour
{
    public GameObject fullTextObj;

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
        fullTextObj.SetActive(true);
        fullTextObj.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.25f, 0.25f, 0.25f), 0.3f);
    }

    public void closeFullText()
    {
        fullTextObj.SetActive(false);
    }

}


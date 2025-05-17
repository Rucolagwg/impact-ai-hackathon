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
        // 1. 현재(원래) 크기를 저장합니다.
        Vector3 originScale = fullTextObj.GetComponent<RectTransform>().localScale;

        // 2. DoTween을 사용하여 크기를 0.2로 줄이는 애니메이션을 시작합니다.
        fullTextObj.GetComponent<RectTransform>().DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                // 4. 애니메이션이 끝난 후 오브젝트를 비활성화합니다.
                fullTextObj.GetComponent<RectTransform>().localScale = originScale;
                fullTextObj.SetActive(false);
            });

        // 3. !!! 문제의 코드 !!!
        //    애니메이션이 시작됨과 동시에 오브젝트의 스케일을 다시 originScale로 즉시 설정합니다.
        //    DoTween 애니메이션은 시간이 걸리는 반면, 이 코드는 즉시 실행됩니다.
        
    }
}


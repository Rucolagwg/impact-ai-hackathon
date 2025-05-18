using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    
    public List<GameObject> panels = new List<GameObject>();
    public string mainSceneName = "";

    public GameObject FadePanel;

    [SerializeField]
    int currentNum = 0;

    public void Next()
    {

        if(currentNum >= panels.Count)
        {
            print("End 함수 실행 (프롤로그)");
           StartCoroutine( End() );
            return;
        }
        else
        {
            for (int i = 0; i < panels.Count; i++)
            {

                if (i == currentNum)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }

            }

            
        }
        currentNum++;



    }


    IEnumerator End()
    {

        FadePanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(mainSceneName);
    }

}

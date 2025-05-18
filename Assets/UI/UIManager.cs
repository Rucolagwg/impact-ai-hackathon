using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public GameObject summaryReport_Obj;
    public TMP_Text txt_summaryTitle;
    public TMP_Text txt_summaryChoice;
    public TMP_Text txt_summaryReason;

    [Space(10), Header("Upper")]
    public TMP_Text txt_NewsHeaderMini;
    public TMP_Text txt_FullTextMini;
    public TMP_Text txt_FullTextMain;

    [Space(10), Header("Resource")]
    public TMP_Text txt_Money;
    public TMP_Text txt_Month;

    [Space(10), Header("Choice")]
    public TMP_Text txt_ChoiceBnt1;
    public TMP_Text txt_ChoiceBnt2;
    public TMP_Text txt_ChoiceBnt3;
    public TMP_Text txt_ChoiceBnt4;



    public List< GameObject > Uis = new List< GameObject >();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 요약 리포트 띄우기

    // 선택 버튼 업데이트 하기

    // 돈 계산하기

    // 돈 , 시간 업데이트 하기

    public void UpdateSummary(string Title, string Choice, string Reason)
    {
        summaryReport_Obj.SetActive(true);
        txt_summaryTitle.text = Title;
        txt_summaryChoice.text = Choice;
        txt_summaryReason.text = Reason;
    }

    public void AllUIOff()
    {
        foreach (GameObject ui in Uis)
        {
            ui.SetActive(false);
        }
    }

    public void AllUIOn()
    {
        foreach (GameObject ui in Uis)
        {
            ui.SetActive(true);
        }
    }

    public void UpdateUpper(string mini, string main)
    {
        // 요약 제목 올리기
        txt_NewsHeaderMini.text = mini;

        // 자세한 내용 업로드
        txt_FullTextMini.text = mini;
        txt_FullTextMain.text = main;
    }

    public void UpdateResource(float money, int month)
    {
        txt_Money.text = money.ToString("F2") + " 원";
        txt_Month.text = month.ToString() + " 월";
    }

    public void UpdateChoice(List<string> choice)
    {
        txt_ChoiceBnt1.text = choice[0];
        txt_ChoiceBnt2.text = choice[1];
        txt_ChoiceBnt3.text = choice[2];
        txt_ChoiceBnt4.text = choice[3];
    }

}

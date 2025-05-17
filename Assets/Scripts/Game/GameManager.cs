using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // 인스턴스가 없으면 자신을 할당
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스는 제거
        }
    }


    public Fade fade;

    [SerializeField]
    List<EventBase> eventBases = new List<EventBase>();
    bool gameEnd = false;

    [SerializeField]
    EventBase currentBase = null;

    // 0번 튜토리얼 1~12 이벤트 
    [SerializeField]
    private int _currentEventnum = 0;
    public int currentEventNum { get { return _currentEventnum; } private set { _currentEventnum = value;  } }

    //돈
    public float Money = 1000000;


    // txt 돈
    [SerializeField]
    TMP_Text txt_Money;

    [SerializeField]
    TMP_Text txt_Month;


    // 예: 게임 초기화
    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if(currentBase != null)
        {
            currentBase.Execute(this);
        }
    }

    [ContextMenu("NextEvent")]
    public void NextEvent()
    {
        if (gameEnd)
            return;


        StartCoroutine(eNextEvent());


    }

    public IEnumerator eNextEvent()
    {

        _currentEventnum++;

        print($"--------{_currentEventnum} NextEvent--------");


        


        if (currentEventNum >= eventBases.Count)
        {
            //
            EndGame();
            gameEnd = true;
            yield return null;
        }
        else if(currentEventNum == 1)
        {
            // 튜토리얼

            if (currentBase != null)
                currentBase.Exit(this);

            // fade 기다리기
            yield return new WaitForSeconds(fade.fadeTime + 1f + 0.1f);

            currentBase = eventBases[_currentEventnum];
            currentBase.Enter(this);

            
        }
        else
        {

            if (currentBase != null)
                currentBase.Exit(this);

            // fade 기다리기
            yield return new WaitForSeconds(fade.fadeTime + 1f + 0.1f);

            currentBase = eventBases[_currentEventnum];
            currentBase.Enter(this);


        }


        print($"-------------------------------");


        yield return null;


    }

    void Setup()
    {
        // 이벤트 베이스 리스트 셋업

        for(int i = 0; i < this.transform.childCount; i++)
        {
            EventBase _base = this.transform.GetChild(i).GetComponent<EventBase>();


            if(_base != null)
            {
                eventBases.Add(_base);

            }
            else
            {
                print($"{this.transform.GetChild(i).name} 에 EventBase가 없음");
            }
        }

        currentBase = eventBases[0];
        currentBase.Enter(this);

    }

    void EndGame()
    {
        print("End게임 호출 됨");
    }

    public void UpdateUIInfo()
    {

        if(txt_Money != null)
        {
            txt_Money.text = Money.ToString("F2");
        }

        if (txt_Month != null)
        {
            txt_Month.text = $"Month : {currentEventNum}";
        }
    }


}

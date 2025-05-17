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

    List<EventBase> eventBases = new List<EventBase>();
    bool gameEnd = false;


    EventBase currentBase = null;

    // 0번 튜토리얼 1~12 이벤트 
    private int _currentEventnum = 0;
    public int currentEventNum { get { return _currentEventnum; } private set { _currentEventnum = value;  } }

    // 예: 게임 초기화
    private void Start()
    {
        Setup();
        NextEvent();
    }

    private void Update()
    {
        if(currentBase != null)
        {
            currentBase.Execute(this);
        }
    }


    public void NextEvent()
    {
        if (gameEnd)
            return;

        _currentEventnum++;
        
        
        if(currentEventNum >= eventBases.Count )
        {
            //
            EndGame();
            gameEnd = true;
            return;
        }

        if (currentBase != null)
            currentBase.Exit(this);

        currentBase = eventBases[_currentEventnum];
        currentBase.Enter(this);



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



    }

    void EndGame()
    {

    }
}

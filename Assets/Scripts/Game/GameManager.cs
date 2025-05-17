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

    // 0번 튜토리얼 1~12 이벤트 
    int currentEventNum = 0;

    // 예: 게임 초기화
    private void Start()
    {
        StartGame();
    }

    void StartGame()
    {



        NextEvent();
    }

    void NextEvent()
    {
        if (currentEventNum == 0)
        {
            //튜토리얼 함수
            EventTutorial();
            print("튜토리얼 함수 실행!");
        }
        else if (currentEventNum > 12)
        {
            // 게임종료 함수

            print("게임 종료 함수 실행!");
            return;
        }

        NextEvent();
    }

    // 튜토리얼 함수
    void EventTutorial()
    {

    }

    //
    void EventGameEnd()
    {

    }

}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.UI; // Button을 사용하기 위해 추가

public class txt_leftInput : MonoBehaviour
{
    public TMP_InputField inputField;    // 사용자가 입력할 InputField
    // public Button sendButton;        // 버튼을 사용하지 않으므로 주석 처리 또는 삭제
    public GeminiChatBot chatBot;        // GeminiChatBot 스크립트 참조
    public txt_left outputDisplay;       // txt_left 스크립트 참조 (결과를 표시할 곳)

    void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField가 txt_leftInput 스크립트에 할당되지 않았습니다!");
            enabled = false;
            return;
        }
        // if (sendButton == null) // 버튼을 사용하지 않으므로 주석 처리 또는 삭제
        // {
        //     Debug.LogError("Send Button이 txt_leftInput 스크립트에 할당되지 않았습니다!");
        //     enabled = false;
        //     return;
        // }
        if (chatBot == null)
        {
            Debug.LogError("GeminiChatBot이 txt_leftInput 스크립트에 할당되지 않았습니다!");
            enabled = false;
            return;
        }
        if (outputDisplay == null)
        {
            Debug.LogError("txt_left (outputDisplay)가 txt_leftInput 스크립트에 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        // 버튼 클릭 리스너 대신 InputField의 onSubmit 이벤트 사용
        // sendButton.onClick.AddListener(SendMessageToBot); // 주석 처리 또는 삭제
        inputField.onSubmit.AddListener(HandleSubmit); // 사용자가 엔터키를 누르거나 포커스 잃을 때 호출
    }

    // InputField에서 Submit 이벤트가 발생했을 때 호출될 함수
    // (인자로 입력된 텍스트가 전달됨)
    void HandleSubmit(string text)
    {
        SendMessageToBot();
    }

    void SendMessageToBot()
    {
        string userInput = inputField.text;

        if (!string.IsNullOrEmpty(userInput))
        {
            chatBot.OnSendEnter(userInput);
            // outputDisplay.SetText(botResponse);
            inputField.text = "";

            // 입력 후 다시 InputField에 포커스를 주고 싶다면 (선택 사항)
            // inputField.ActivateInputField();
        }
        else
        {
            Debug.Log("입력된 내용이 없습니다.");
        }
    }

    void Update()
    {
        // 만약 특정 키 (예: 엔터키)를 직접 감지하고 싶다면 여기서 처리할 수도 있습니다.
        // 하지만 TMP_InputField의 onSubmit 이벤트를 사용하는 것이 더 깔끔할 수 있습니다.
        // if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        // {
        //     if (inputField.isFocused) // InputField가 활성화 되어 있을 때만
        //     {
        //         SendMessageToBot();
        //     }
        // }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// UI는 직접 사용하지 않으므로 using문 제거 가능 (필요 시 다시 추가)

public class txt_left : MonoBehaviour
{
    // public string content; // 직접 사용하지 않으므로 제거하거나 주석 처리 가능
    public TMP_Text textComponent; // 텍스트를 표시할 TMP_Text 컴포넌트

    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("TextComponent가 txt_left 스크립트에 할당되지 않았습니다!");
            // textComponent = GetComponent<TMP_Text>(); // 만약 같은 GameObject에 있다면 이렇게 찾을 수도 있습니다.
            enabled = false;
            return;
        }
        // 초기 텍스트 설정 (선택 사항)
        // textComponent.text = "대기 중...";
    }

    // 외부에서 텍스트를 설정하는 메서드
    public void SetText(string message)
    {
        if (textComponent != null)
        {
            textComponent.text = message;
        }
        else
        {
            Debug.LogError("TextComponent가 null이어서 메시지를 설정할 수 없습니다.");
        }
    }

    // Update는 특별한 로직이 없다면 비워둡니다.
    void Update()
    {

    }
}
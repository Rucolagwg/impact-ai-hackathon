using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// UI는 직접 사용하지 않으므로 using문 제거 가능 (필요 시 다시 추가)


public class txt_left : MonoBehaviour
{
    public TMP_Text textComponent;
    public ScrollRect scrollRect;
    private RectTransform contentRectTransform; // Content의 RectTransform 캐싱

    void Awake() // Start보다 먼저 호출되어 참조 설정
    {
        if (textComponent == null)
        {
            Debug.LogError("TextComponent가 txt_left 스크립트에 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        // Content GameObject는 일반적으로 TMP_Text의 부모입니다.
        // ScrollRect의 content 필드에서 가져오는 것이 더 안전합니다.
        if (scrollRect != null && scrollRect.content != null)
        {
            contentRectTransform = scrollRect.content;
        }
        else if (textComponent.transform.parent != null && textComponent.transform.parent.GetComponent<ContentSizeFitter>() != null)
        {
            // TMP_Text의 부모가 Content이고 ContentSizeFitter를 가지고 있다면
            contentRectTransform = textComponent.transform.parent.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogWarning("Content RectTransform을 찾을 수 없습니다. LayoutRebuilder가 정확히 작동하지 않을 수 있습니다.");
        }


        if (scrollRect == null)
        {
            Debug.LogWarning("ScrollRect가 txt_left 스크립트에 할당되지 않았습니다. 자동 스크롤이 작동하지 않습니다.");
        }
    }

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

    // 외부에서 텍스트를 설정하거나 추가하는 메서드
    public void AddText(string message)
    {
        if (textComponent != null)
        {
            textComponent.text += message + "\n"; // 기존 내용에 추가

            // 레이아웃 업데이트 요청
            RequestLayoutRebuild();

            // 자동 스크롤
            if (scrollRect != null)
            {
                StartCoroutine(ScrollToBottomAfterLayoutUpdate());
            }
        }
        else
        {
            Debug.LogError("TextComponent가 null이어서 메시지를 추가할 수 없습니다.");
        }
    }

    // 텍스트를 완전히 새로 설정하는 메서드
    public void SetFullText(string fullMessage)
    {
        if (textComponent != null)
        {
            textComponent.text = fullMessage;

            // 레이아웃 업데이트 요청
            RequestLayoutRebuild();

            // 자동 스크롤
            if (scrollRect != null)
            {
                StartCoroutine(ScrollToBottomAfterLayoutUpdate());
            }
        }
        else
        {
            Debug.LogError("TextComponent가 null이어서 메시지를 설정할 수 없습니다.");
        }
    }

    private void RequestLayoutRebuild()
    {
        if (contentRectTransform != null)
        {
            // Content Size Fitter가 있는 Content RectTransform에 대해 레이아웃 재빌드를 요청합니다.
            LayoutRebuilder.MarkLayoutForRebuild(contentRectTransform);
        }
        else if (textComponent != null)
        {
            // Content를 못찾았다면 TMP_Text 자체라도 시도
            LayoutRebuilder.MarkLayoutForRebuild(textComponent.rectTransform);
        }
    }

    IEnumerator ScrollToBottomAfterLayoutUpdate()
    {
        // MarkLayoutForRebuild는 보통 다음 프레임에 레이아웃을 업데이트합니다.
        // 따라서 한 프레임 (또는 UI 업데이트 사이클 후) 기다립니다.
        yield return new WaitForEndOfFrame(); // 또는 yield return null;

        if (scrollRect != null)
        {
            // Content Size Fitter가 Content 크기를 조정한 후 스크롤 위치를 설정해야 정확합니다.
            // 필요한 경우, 한 번 더 대기하거나 Canvas.ForceUpdateCanvases()를 고려할 수 있으나,
            // 보통 WaitForEndOfFrame 이후면 충분합니다.
            Canvas.ForceUpdateCanvases(); // 필요한 경우에만 사용, 성능 영향 고려
            // scrollRect.content.localPosition = new Vector2(scrollRect.content.localPosition.x, 0); // 이렇게 해도 맨 위로 가는 경우가 있음

            scrollRect.verticalNormalizedPosition = 0f; // 0 = 맨 아래, 1 = 맨 위
        }
    }

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

    void Update()
    {
        // 특별한 로직이 없다면 비워둡니다.
    }
}
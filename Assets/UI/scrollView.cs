using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrollView : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private TextMeshProUGUI text;
    

    private void Awake()
    {
        scrollRect.onValueChanged.AddListener(OnScrollRectEvent);
    }

    public void OnScrollRectEvent(Vector2 position)
    {
        text.text = $"Scrollbar Position : {position}";
    }
}

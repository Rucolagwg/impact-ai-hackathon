using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Header("Fade")]
    public Image FadeImg;
    public TMP_Text Txt_Fade;
    [Range(0.1f, 5f)]
    public float fadeTime = 1f;
    public Ease ease_Fade = Ease.Linear;

    [ContextMenu("FadeIn")]
    public void FadeIN()
    {
        Txt_Fade.text = $"Month : {GameManager.Instance.currentEventNum}";

        Txt_Fade.gameObject.SetActive(true);
        FadeImg.DOFade(0f, fadeTime).SetEase(ease_Fade);
        Txt_Fade.DOFade(0f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(false);

        });

    }

    public IEnumerator eFadeIN()
    {


        yield return null;
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        FadeImg.DOFade(1f, fadeTime).SetEase(ease_Fade);
        Txt_Fade.DOFade(1f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(true);
            Txt_Fade.DOFade(1f, 1f).SetEase(ease_Fade);

        }); ;

    }
}

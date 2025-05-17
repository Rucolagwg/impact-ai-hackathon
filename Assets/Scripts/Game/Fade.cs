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
        

        Txt_Fade.gameObject.SetActive(true);
        FadeImg.DOFade(0f, fadeTime).SetEase(ease_Fade);
        Txt_Fade.DOFade(0f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(false);

        });

    }

    public IEnumerator eFadeIN()
    {
        print("eFadeIN 시작");
        Txt_Fade.gameObject.SetActive(true);
        FadeImg.DOFade(0f, fadeTime).SetEase(ease_Fade);
        Txt_Fade.DOFade(0f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(false);

        });
        

        yield return new WaitForSeconds(fadeTime+0.1f);
        print("eFadeIN 끝");
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        Txt_Fade.text = $"Month : {GameManager.Instance.currentEventNum}";

        print($"Month : {GameManager.Instance.currentEventNum}");

        FadeImg.DOFade(1f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(true);
            Txt_Fade.DOFade(1f, 1f).SetEase(ease_Fade);

        });

    }

    public IEnumerator eFadeOut()
    {
        print("eFadeOut 시작");

        Txt_Fade.text = $"Month : {GameManager.Instance.currentEventNum}";

        // print($"Month : {GameManager.Instance.currentEventNum}");

        FadeImg.DOFade(1f, fadeTime).SetEase(ease_Fade).OnComplete(() => {
            Txt_Fade.gameObject.SetActive(true);
            Txt_Fade.DOFade(1f, 1f).SetEase(ease_Fade);

        });
        
        yield return new WaitForSeconds(fadeTime +1f + 0.1f);
        print("eFadeOut 끝");
    }

    public IEnumerator eTutoFadeOut()
    {

        FadeImg.gameObject.SetActive(true);
        FadeImg.DOFade(1f, fadeTime).SetEase(ease_Fade);

        yield return new WaitForSeconds(fadeTime + 1f + 0.1f);
    }
}

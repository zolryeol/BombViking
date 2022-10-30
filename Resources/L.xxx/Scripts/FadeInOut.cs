using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 로고에 쓸 페이드아웃
/// </summary>

public class FadeInOut : MonoBehaviour
{
    public Image fadePanel;

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0; // 알파값
        while (fadeCount < 1.0f) // 255 가아니라 0~1사이값으로 쓴다.
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01 초마다 수행시킬것이다.

            fadePanel.color = new Color(0, 0, 0, fadeCount);
        }
    }


    IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1; // 알파값
        while (fadeCount > 0.0f) // 255 가아니라 0~1사이값으로 쓴다.
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01 초마다 수행시킬것이다.

            fadePanel.color = new Color(0, 0, 0, fadeCount);
        }
    }

}

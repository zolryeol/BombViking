using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ΰ� �� ���̵�ƿ�
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
        float fadeCount = 0; // ���İ�
        while (fadeCount < 1.0f) // 255 ���ƴ϶� 0~1���̰����� ����.
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01 �ʸ��� �����ų���̴�.

            fadePanel.color = new Color(0, 0, 0, fadeCount);
        }
    }


    IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1; // ���İ�
        while (fadeCount > 0.0f) // 255 ���ƴ϶� 0~1���̰����� ����.
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01 �ʸ��� �����ų���̴�.

            fadePanel.color = new Color(0, 0, 0, fadeCount);
        }
    }

}

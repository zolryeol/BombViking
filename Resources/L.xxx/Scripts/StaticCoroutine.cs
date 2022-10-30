using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڷ�ƾ�� ����ƽ���� �������� �̱������� ���� ����Ų��.
/// ���ӿ����� ���� �����ξ���
/// </summary>
public class StaticCoroutine : MonoBehaviour
{
    static public StaticCoroutine Instance;

    //     private static StaticCoroutine instance
    //     {
    //         get
    //         {
    //             if (m_Instance == null)
    //             {
    //                 m_Instance = GameObject.FindObjectOfType(typeof(StaticCoroutine)) as StaticCoroutine;
    //                 if (m_Instance == null)
    //                     m_Instance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
    //             }
    //             return m_Instance;
    //         }
    //     }

    private void Awake()
    {
        Instance = this;
        //         if (m_Instance == null)
        //         {
        //             m_Instance = this as StaticCoroutine;
        //         }
    }
    static int temp = 0;
    IEnumerator SlowDown()
    {
        SoundManager.Instance.dangerSound.Pause();

        while (0.2 <= Time.timeScale)
        {
            if (Time.timeScale <= 0) StopAllCoroutines();

            Time.timeScale -= 0.1f;

            Debug.Log(Time.timeScale);

            EffectManager.Instance.DyingBomb(temp); // 
            temp++;
            if (2 < temp) temp = 0;

            yield return new WaitForSeconds(0.2f);
        }
        Time.timeScale = 0;
        SoundManager.Instance.bgm.Pause();

        UIManager.Instance.GameOverPanel.SetActive(true);

        SoundManager.Instance.gameOverSound[Random.Range(0, 4)].Play();
    }

    static public void DoSlowDownCoroutine()
    {
        Instance.StartCoroutine(Instance.SlowDown());
    }

}

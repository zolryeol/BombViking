using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// �ӽ÷� ���� �Դٰ� �ٷ� Ÿ��Ʋ������ ������
/// </summary>
public class TempGameOver : MonoBehaviour
{

    private void Start()
    {
        Invoke("LoadTit", 2.3f);
    }

    void LoadTit()
    {
        SceneManager.LoadScene("01.Title");
    }
}


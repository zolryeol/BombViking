using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 임시로 여기 왔다가 바로 타이틀씬으로 보낸다
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


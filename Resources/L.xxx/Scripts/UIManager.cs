using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI°ü·Ã ½ºÅ©¸³Æ® ½Ì±ÛÅæ
/// </summary>

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text textTimer;
    public TextMesh bombDefuseCount;
    public Text countDownToStart;
    public Text debug;
    public Text debug2;
    public GameObject GameOverPanel;
    public Text distanceFromStart;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        GameOverPanel.SetActive(false);
    }


}

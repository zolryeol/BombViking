using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Ÿ��Ʋȭ�鿡 �ִ� ��ư���� Ű���� �� ���콺�� �����Ѵ�.
/// </summary>

public class ButtonController : MonoBehaviour
{
    [Header("�̹������� �ִ´�")]
    [SerializeField]
    GameObject[] startButton;
    [SerializeField]
    GameObject[] optionButton;
    [SerializeField]
    GameObject[] exitButton;

    [Header("���� �̹���")]
    [SerializeField]
    Image[] howToPlayImages = new Image[3];

    int nowSelectedIndex;

    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

    bool isHowToPlayGame = false;

    //GameObject[] ImageObject = GameObject.FindGameObjectsWithTag("MenuButton");
    private void Awake()
    {
        nowSelectedIndex = 2;
    }
    private void Update()
    {
        MouseRay();

        KeyInputTitle();
    }

    public void MouseRay()
    {

        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        //m_Raycaster.Raycast(m_PointerEventData, results);

        EventSystem.current.RaycastAll(m_PointerEventData, results);

        if (results.Count > 0) //{ Debug.Log(results[0].gameObject.name); }
        {
            if (results[0].gameObject.CompareTag("StartButton"))
            {
                nowSelectedIndex = 2;
            }
            else if (results[0].gameObject.CompareTag("OptionButton"))
            {
                nowSelectedIndex = 1;
            }
            else if (results[0].gameObject.CompareTag("ExitButton"))
            {
                nowSelectedIndex = 0;
            }
        }
    }

    public void ButtonEventStart()
    {
        SceneMG.Instance.LoadGame();
    }

    public void ButtonEventExit()
    {
        Application.Quit();
    }

    public void ButtonEventHowToPlay()
    {
        foreach (var image in howToPlayImages)
        {
            image.gameObject.SetActive(true);
        }
        isHowToPlayGame = true;
        howToPlayIndex = 1;
    }

    void KeyInputTitle()
    {
        if (!isHowToPlayGame && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (nowSelectedIndex < 2) nowSelectedIndex++;
        }

        if (!isHowToPlayGame && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (0 < nowSelectedIndex) nowSelectedIndex--;
        }

        startButton[1].SetActive(true);
        optionButton[1].SetActive(true);
        exitButton[1].SetActive(true);

        switch (nowSelectedIndex)
        {
            case 2: startButton[1].SetActive(false); break;
            case 1: optionButton[1].SetActive(false); break;
            case 0: exitButton[1].SetActive(false); break;
        }

        if (!isHowToPlayGame && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            switch (nowSelectedIndex)
            {
                case 2: SceneMG.Instance.LoadGame(); return;
                case 1: ButtonEventHowToPlay(); return;
                case 0: Application.Quit(); break;
            }
        }

        if (isHowToPlayGame && Input.anyKeyDown)    // ���� UI �¿���
        {
            howToPlayIndex++;

            Debug.Log($"���� �����ε��� {howToPlayIndex} ");

            switch (howToPlayIndex)
            {
                case 2:
                    howToPlayImages[2].gameObject.SetActive(false);
                    break;

                case 3:
                    howToPlayImages[1].gameObject.SetActive(false);
                    break;

                case 4:
                    howToPlayImages[0].gameObject.SetActive(false);
                    howToPlayIndex = 0;
                    nowSelectedIndex = 2;
                    isHowToPlayGame = false;
                    break;
            }
        }
    }

    int howToPlayIndex = 0;

}

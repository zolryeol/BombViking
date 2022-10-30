using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
///  게임오버나왔을때 쓸 용도로 급히만든 UI컨트롤러
/// </summary>
public class GameOverPanel : MonoBehaviour
{
    int nowSelectBar = 0;

    Transform[] menuBar = new Transform[2];

    Text[] scoreText = new Text[3];

    public static float resultForLeaderboard;

    private void Awake()
    {
        menuBar[0] = GameObject.Find("TryAgain").transform;
        menuBar[1] = GameObject.Find("TurnTitle").transform;
        //         menuBar[0] = this.gameObject.transform.GetChild(1).transform;
        //         menuBar[1] = this.gameObject.transform.GetChild(2).transform;

        scoreText[0] = GameObject.Find("DistanceScoreText").GetComponent<Text>();
        scoreText[1] = GameObject.Find("DifuseScoreText").GetComponent<Text>();
        scoreText[2] = GameObject.Find("ResultScoreText").GetComponent<Text>();
        //score = this.gameObject.transform.GetChild(3).GetComponent<Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(PrintScore());

        Achievement.Instance.AchieveCheck(eAchieveState.RankFisrt);
    }

    public void ButtonEventReStart()
    {
        SceneMG.Instance.LoadGame();
    }

    public void ButtonEventGoTitle()
    {
        SceneMG.Instance.LoadTitle();
    }

    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

    IEnumerator PrintScore()
    {
        //score.text = "<size=140>" + "<color=#ff0000>" + result.ToString("N2") + "</color>" + "</size>" + "    " + "Distance( " + PlayerMove.distanceFromStartLine.ToString("N2") + " ) + " + "DefuseCount" + "(" + GameDifficulty.bombDefuseCount + ") X 500";  // 결과

        yield return new WaitForSecondsRealtime(0.4f);

        scoreText[0].text = PlayerMove.distanceFromStartLine.ToString("N2");

        yield return new WaitForSecondsRealtime(0.4f);

        scoreText[1].text = GameDifficulty.bombDefuseCount.ToString() + " X " + "500";

        yield return new WaitForSecondsRealtime(0.8f);

        var result = PlayerMove.distanceFromStartLine + (GameDifficulty.bombDefuseCount * 500);

        resultForLeaderboard = (int)result;

        scoreText[2].text = result.ToString("N2");

        LeaderBoard.Instance.FindLeaderBoard();
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
                nowSelectBar = 0;
            }
            else if (results[0].gameObject.CompareTag("ExitButton"))
            {
                nowSelectBar = 1;
            }

            menuBar[nowSelectBar].GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nowSelectBar++;
            if (2 <= nowSelectBar) nowSelectBar = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nowSelectBar--;
            if (nowSelectBar < 0) nowSelectBar = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Return)))
        {
            switch (nowSelectBar)
            {
                case 0:
                    SceneMG.Instance.LoadGame();
                    return;
                case 1:
                    SceneMG.Instance.LoadTitle();
                    return;
                    //                 case 2:
                    //                     Application.Quit();
                    //                     return;
            }
        }

        foreach (var bar in menuBar)
        {
            bar.GetChild(0).gameObject.SetActive(false);
        }

        menuBar[nowSelectBar].GetChild(0).gameObject.SetActive(true);

        MouseRay();
    }
}

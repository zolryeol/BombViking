using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임매니저 안에서 게임 밸런스관련하여 다룬다.
/// 기울기, 속도, 장애물
/// </summary>

public class GameDifficulty : MonoBehaviour
{
    BalanceWeight characterSkewness; // 캐릭터 뒤틀림 Skewness 스쿠우니스 : 비대칭도, 뒤틀림

    float gameTimer;    // 그냥 1초마다 불리는 타이머

    int countDownTime = 3;

    public static float playTimer; // 게임에 영향을 주는 타이머

    public static int nowDifficulty;

    public static int bombDefuseCount = 0;
    public static stdeligate delPrintOnText;

    public delegate void stdeligate();

    [Header("몇초마다 속도업시킬 건지")]
    [SerializeField]
    int unitForDifficulty = 1;  // 몆초마다 난이도 상승시킬건지

    [Header("몇초마다 버튼 생성되게할것인지")]
    [SerializeField]
    int buttonGenInterval = 1;  // 몆초마다 난이도 상승시킬건지
    int buttonGenNextTime = 0;

    [Header("꺾일 확률 낮을수록 자주꺾임 최소1")]
    [SerializeField]
    int slopeChance = 1;

    PlayerMove CharacterSpeed;

    private GameObject timerUI;

    private void Awake()
    {
        gameTimer = 0;

        playTimer = 0;

        characterSkewness = this.gameObject.GetComponent<BalanceWeight>();

        CharacterSpeed = GameObject.Find("Player").GetComponent<PlayerMove>();

        nowDifficulty = 1;

        timerUI = GameObject.Find("Timer").gameObject;

        timerUI.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(CountDownToStart());

        StartCoroutine("BombTimer");

        // 시작하자마자 좌나 우로 꺾자

        StartLeftOrRight();

        bombDefuseCount = 0;
    }
    private void FixedUpdate()
    {
        GameTimer();

        UIManager.Instance.bombDefuseCount.text = "<size=>" + "Defuse" + "</size>" + "\n" + bombDefuseCount.ToString();

        if (delPrintOnText != null)
        {
            delPrintOnText();
        }
    }
    void StartLeftOrRight()
    {
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                characterSkewness.LeftSlope();
                break;
            case 1:
                characterSkewness.RightSlope();
                break;
        }
    }
    void Crash(Collider obstacle) // 물체와 충돌했을때
    {
        if (obstacle.tag == "obstacle")
        {
            characterSkewness.spine.rotation = Quaternion.Euler(characterSkewness.spine.rotation.eulerAngles.x,
                    characterSkewness.spine.rotation.eulerAngles.y,
                    characterSkewness.spine.rotation.eulerAngles.z);
        }
    }
    int nowSlope;
    void ChangeSkewness() // 일정 시간마다 기울기를 바꿀 떄
    {
        if (UnityEngine.Random.Range(0, slopeChance) == 0)
        {
            if (BalanceWeight.slopeAngle == 0)
            {
                StartLeftOrRight();
            }
            else if (0 < BalanceWeight.slopeAngle) // 왼쪽으로 꺾여있을때
            {
                characterSkewness.LeftSlope();      // 더 왼쪽으로 꺾는다.
            }
            else if (BalanceWeight.slopeAngle < 0)
            {
                characterSkewness.RightSlope();
            }
        }
    }
    void SpeedUp() // 일정시간마다 속도 업시킨다.
    {
        if (unitForDifficulty * nowDifficulty < playTimer && nowDifficulty < 30)
        {
            CharacterSpeed.speed--;
            nowDifficulty++;
        }
    }

    int preTime;
    void GameTimer()
    {
        gameTimer += Time.fixedDeltaTime;
        playTimer += Time.fixedDeltaTime;

        UIManager.Instance.textTimer.text = ((int)gameTimer).ToString();

        SpeedUp();

        if (preTime < (int)gameTimer)
        {
            ChangeSkewness();

            if (buttonGenNextTime < gameTimer)
            {
                buttonGenNextTime = (int)gameTimer + buttonGenInterval;
                AppearKey.deligatebutton();
            }
        }

        preTime = (int)gameTimer;
    }
    IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1f);

        if (0 <= BombWick.BombWickIndex)
        {
            BombWick.BombWickDown();

            StartCoroutine("BombTimer");
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator CountDownToStart() // 시작시점에 카운트다운 static Delicate에 넣어서 사용함
    {
        while (0 < countDownTime)
        {
            UIManager.Instance.countDownToStart.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }
        UIManager.Instance.countDownToStart.text = "Go!";

        yield return new WaitForSeconds(1f);

        UIManager.Instance.countDownToStart.gameObject.SetActive(false);

        gameTimer = 0;

        timerUI.SetActive(true);
    }
}

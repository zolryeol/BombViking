using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӸŴ��� �ȿ��� ���� �뷱�������Ͽ� �ٷ��.
/// ����, �ӵ�, ��ֹ�
/// </summary>

public class GameDifficulty : MonoBehaviour
{
    BalanceWeight characterSkewness; // ĳ���� ��Ʋ�� Skewness �����Ͻ� : ���Ī��, ��Ʋ��

    float gameTimer;    // �׳� 1�ʸ��� �Ҹ��� Ÿ�̸�

    int countDownTime = 3;

    public static float playTimer; // ���ӿ� ������ �ִ� Ÿ�̸�

    public static int nowDifficulty;

    public static int bombDefuseCount = 0;
    public static stdeligate delPrintOnText;

    public delegate void stdeligate();

    [Header("���ʸ��� �ӵ�����ų ����")]
    [SerializeField]
    int unitForDifficulty = 1;  // �p�ʸ��� ���̵� ��½�ų����

    [Header("���ʸ��� ��ư �����ǰ��Ұ�����")]
    [SerializeField]
    int buttonGenInterval = 1;  // �p�ʸ��� ���̵� ��½�ų����
    int buttonGenNextTime = 0;

    [Header("���� Ȯ�� �������� ���ֲ��� �ּ�1")]
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

        // �������ڸ��� �³� ��� ����

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
    void Crash(Collider obstacle) // ��ü�� �浹������
    {
        if (obstacle.tag == "obstacle")
        {
            characterSkewness.spine.rotation = Quaternion.Euler(characterSkewness.spine.rotation.eulerAngles.x,
                    characterSkewness.spine.rotation.eulerAngles.y,
                    characterSkewness.spine.rotation.eulerAngles.z);
        }
    }
    int nowSlope;
    void ChangeSkewness() // ���� �ð����� ���⸦ �ٲ� ��
    {
        if (UnityEngine.Random.Range(0, slopeChance) == 0)
        {
            if (BalanceWeight.slopeAngle == 0)
            {
                StartLeftOrRight();
            }
            else if (0 < BalanceWeight.slopeAngle) // �������� ����������
            {
                characterSkewness.LeftSlope();      // �� �������� ���´�.
            }
            else if (BalanceWeight.slopeAngle < 0)
            {
                characterSkewness.RightSlope();
            }
        }
    }
    void SpeedUp() // �����ð����� �ӵ� ����Ų��.
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

    IEnumerator CountDownToStart() // ���۽����� ī��Ʈ�ٿ� static Delicate�� �־ �����
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

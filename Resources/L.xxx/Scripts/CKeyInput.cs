using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여기서 내가 주는 input 정보와 button이 가지고있는 정보를 비교해서 box의 기능을 실행시키겠다.
/// 
/// </summary>
public class CKeyInput : MonoBehaviour
{
    static class ButtonsType
    {
        public const int TOP = 0;
        public const int RIGHT = 1;
        public const int LEFT = 2;
        public const int FIRST = 3;
        public const int SECOND = 4;
    }

    // 박스들을 받아둔다.
    // 박스들의 자식들에게 addforce를 한다.
    public Transform boxes;

    public int nowActivateCount = 0; // 피버버튼이 엑티베이트 된 것을 카운팅한다. 4개가 되면 해체카운트를 줄일것이다.

    [SerializeField]
    GameObject defuseCount;
    [SerializeField]
    Transform defuseCountPos;

    [SerializeField]
    GameObject character;

    [Range(1000f, 10000f)]
    [SerializeField]
    float jumpPower = 7000f;

    [Header("중력조절 - 붙일 것")]
    [SerializeField]
    float gravitySize = -50f;

    Rigidbody rb;   // 캐릭터 리지드바디
    [SerializeField]
    FeverBox tempBox;

    BoxOutCheck[] boxesCheck;       // 각 박스별로 기능을 넣을 것이다.
    private void Awake()
    {
        //adefuseCount = GameObject.Find("DefuseCount").GetComponent<>;

        nowActivateCount = 0;

        rb = character.GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(0, gravitySize, 0);

        boxesCheck = new BoxOutCheck[GameObject.Find("Boxes").transform.childCount];

        for (int i = 0; i < GameObject.Find("Boxes").transform.childCount; ++i)
        {
            if (GameObject.Find("Boxes").transform.GetChild(i).CompareTag("FeverBox"))
            {
                boxesCheck[i] = GameObject.Find("Boxes").transform.GetChild(i).GetComponent<FeverBox>();
            }
            else
                boxesCheck[i] = GameObject.Find("Boxes").transform.GetChild(i).GetComponent<BoxOutCheck>();
        }
    }
    private void Update()
    {
        InputFuction();
        //UIManager.Instance.debug2.text = nowActivateCount.ToString();
    }
    private void InputFuction() // 인풋을 대부분 여기서 처리할 것이다.
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AppearKey.deligatebutton();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BombWick.BombWickIndex = 0;
        }

        if (Input.anyKeyDown)
        {
            /// 키를 입력받아서 그키가 무슨 키인지 알아오는 방법 
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //Debug.Log("아무키다운" + vKey);
                    if (boxesCheck[ButtonsType.TOP].mountedButton.buttonType == vKey && JumpStoper.isGround == true)
                    {
                        //Debug.log("점프 작동");
                        Jump();
                        SoundManager.instance.pressInputSound.Play();
                    }
                    else if (boxesCheck[ButtonsType.RIGHT].mountedButton.buttonType == vKey)
                    {
                        //Debug.log("우측 버튼작동");
                        BalanceWeight.Instance.RightSlope();
                        SoundManager.instance.pressInputSound.Play();
                    }
                    else if (boxesCheck[ButtonsType.LEFT].mountedButton.buttonType == vKey)
                    {
                        //Debug.log("좌측 버튼작동");
                        BalanceWeight.Instance.LeftSlope();
                        SoundManager.instance.pressInputSound.Play();
                    }

                    #region 이제는 안쓰는 버튼
                    //                     else if (boxesCheck[ButtonsType.FIRST].mountedButton.buttonType == vKey)
                    //                     {
                    //                         Debug.Log("1번 버튼작동");
                    //                         FirstButtonFuction();
                    //                         SoundManager.instance.pressInputSound.Play();
                    //                     }
                    #endregion

                    /// 피버박스용
                    else if (CheckFeverBoxButton(vKey, out tempBox) == true) // 내가 누른것과 일치했을때
                    {
                        // 피버 박스에 들어있는 자식들을 조사한다.
                        for (int i = 0; i < tempBox.transform.childCount; ++i)
                        {
                            // 자식들중 버튼이 있다면
                            if (tempBox.transform.GetChild(i).gameObject.CompareTag("Button"))
                            {
                                //Debug.log(tempBox);
                                var button = tempBox.transform.GetChild(i).GetComponent<ButtonIdentity>();

                                if (button.nowButtonParentState == eButtonParentState.ONFEVERBOX && button.buttonType == tempBox.GetNeedButton())
                                {
                                    //Debug.Log("뭔가 아다리가 맞았다?" + vKey);

                                    tempBox.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;

                                    button.nowButtonParentState = eButtonParentState.FEVERACTIVATE; // 모든 버튼이 FEVERACTIVATE가 되면 카운트를 줄여야한다. 

                                    nowActivateCount++;

                                    SoundManager.Instance.getButton.Play();

                                    BoxClear();

                                }
                            }
                        }
                    }
                    else if (CheckFeverBoxButton(vKey, out tempBox) == false) // 누른것과 일치하지않았을때 // if문 맨위로 올리면 다른키도 예외없이 시킬듯하다.
                    {
                        foreach (var val in ButtonIDpocket.Instance.lFeverBox)
                        {
                            for (int i = 0; i < val.transform.childCount; ++i)
                            {
                                if (val.transform.GetChild(i).gameObject.CompareTag("Button"))
                                {
                                    val.transform.GetChild(i).GetComponent<ButtonIdentity>().nowButtonParentState = eButtonParentState.NONE;
                                }
                            }
                        }
                        nowActivateCount = 0;
                    }
                }

            }
        }
    }

    private void BoxClear()
    {
        if (3 <= nowActivateCount) // 3개의 피버박스를 엑티베이트 시켰다면
        {
            StartCoroutine(SoundManager.Instance.PlayDefuseButton());

            SoundManager.Instance.defuseBomb.Play();

            GameDifficulty.bombDefuseCount++; // 해체카운트 증가시킨다.

            switch (GameDifficulty.bombDefuseCount)
            {
                case 1: Achievement.Instance.AchieveCheck(eAchieveState.Defuse1); break;
                case 5: Achievement.Instance.AchieveCheck(eAchieveState.Defuse5); break;
                case 10: Achievement.Instance.AchieveCheck(eAchieveState.Defuse10); break;
                case 20: Achievement.Instance.AchieveCheck(eAchieveState.Defuse20); break;
            }



            StartCoroutine(DefuseTextMove()); // 텍스트생성하고 이동시킴

            while (BombWick.BombWickIndex < 100)
            {
                BombWick.BomWickUp();       // 풀피로 만들어준다
            }

            ButtonIDpocket.Instance.ResetButtonType();

            // 모든 피버박스의 needbutton을 다시 셋시킨다.
            foreach (var boxes in boxesCheck)
            {
                boxes.SetNeedButton();

                // 기존에 있던 친구들 밀어내자
                if (boxes.CompareTag("FeverBox"))
                {
                    boxes.FingerSnapButton();
                    EffectManager.Instance.PlayEffect(boxes.transform.position + new Vector3(0, 0, -3), Vector3.forward, boxes.transform, EffectManager.eEffectType.DefuseBomb);
                }
            }
        }
    }

    public IEnumerator DefuseTextMove()
    {
        yield return null;

        var text = Instantiate(defuseCount, defuseCountPos.transform.position, Quaternion.Euler(new Vector3(0, 90, 0)), defuseCountPos);

        while (text.transform.position.y < 20)
        {
            text.transform.Translate(Vector3.up * Time.deltaTime * 5);
            yield return null;
        }

        Destroy(text);
    }

    private void Jump() // 캐릭터 점프기능 애드포스활용
    {
        rb.AddForce(Vector3.up * jumpPower);
        JumpStoper.isGround = false;
    }
    private void FirstButtonFuction() // 생명이 찬다
    {
        BombWick.BomWickUp();
    }
    private void SecondButtonFunction()
    {
        BombWick.BomWickUp();
    }

    private bool CheckFeverBoxButton(KeyCode _keyCode, out FeverBox _feverBox)
    {
        // ButtonIDpoket에 들어있는 list를 검사한다.
        // 누른버튼과 하나라도 일치하면 true반납시킨다.
        foreach (var val in ButtonIDpocket.Instance.lFeverBox) // ebutton타입으로 나올것이다.
        {
            if (val.GetNeedButton() == _keyCode) // 박스에있는 need버튼과 내가누른 버튼이 일치하다면
            {
                _feverBox = val;
                return true;    // 참을 반납한다는 뜻은 피버박스안에 올바른 버튼이 장착되어있고 내가 해당 버튼을 눌렀다는 뜻이다.
            }
        }
        _feverBox = null;
        return false;
    }
}

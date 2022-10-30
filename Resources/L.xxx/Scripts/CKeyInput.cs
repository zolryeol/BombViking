using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���⼭ ���� �ִ� input ������ button�� �������ִ� ������ ���ؼ� box�� ����� �����Ű�ڴ�.
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

    // �ڽ����� �޾Ƶд�.
    // �ڽ����� �ڽĵ鿡�� addforce�� �Ѵ�.
    public Transform boxes;

    public int nowActivateCount = 0; // �ǹ���ư�� ��Ƽ����Ʈ �� ���� ī�����Ѵ�. 4���� �Ǹ� ��üī��Ʈ�� ���ϰ��̴�.

    [SerializeField]
    GameObject defuseCount;
    [SerializeField]
    Transform defuseCountPos;

    [SerializeField]
    GameObject character;

    [Range(1000f, 10000f)]
    [SerializeField]
    float jumpPower = 7000f;

    [Header("�߷����� - ���� ��")]
    [SerializeField]
    float gravitySize = -50f;

    Rigidbody rb;   // ĳ���� ������ٵ�
    [SerializeField]
    FeverBox tempBox;

    BoxOutCheck[] boxesCheck;       // �� �ڽ����� ����� ���� ���̴�.
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
    private void InputFuction() // ��ǲ�� ��κ� ���⼭ ó���� ���̴�.
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
            /// Ű�� �Է¹޾Ƽ� ��Ű�� ���� Ű���� �˾ƿ��� ��� 
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //Debug.Log("�ƹ�Ű�ٿ�" + vKey);
                    if (boxesCheck[ButtonsType.TOP].mountedButton.buttonType == vKey && JumpStoper.isGround == true)
                    {
                        //Debug.log("���� �۵�");
                        Jump();
                        SoundManager.instance.pressInputSound.Play();
                    }
                    else if (boxesCheck[ButtonsType.RIGHT].mountedButton.buttonType == vKey)
                    {
                        //Debug.log("���� ��ư�۵�");
                        BalanceWeight.Instance.RightSlope();
                        SoundManager.instance.pressInputSound.Play();
                    }
                    else if (boxesCheck[ButtonsType.LEFT].mountedButton.buttonType == vKey)
                    {
                        //Debug.log("���� ��ư�۵�");
                        BalanceWeight.Instance.LeftSlope();
                        SoundManager.instance.pressInputSound.Play();
                    }

                    #region ������ �Ⱦ��� ��ư
                    //                     else if (boxesCheck[ButtonsType.FIRST].mountedButton.buttonType == vKey)
                    //                     {
                    //                         Debug.Log("1�� ��ư�۵�");
                    //                         FirstButtonFuction();
                    //                         SoundManager.instance.pressInputSound.Play();
                    //                     }
                    #endregion

                    /// �ǹ��ڽ���
                    else if (CheckFeverBoxButton(vKey, out tempBox) == true) // ���� �����Ͱ� ��ġ������
                    {
                        // �ǹ� �ڽ��� ����ִ� �ڽĵ��� �����Ѵ�.
                        for (int i = 0; i < tempBox.transform.childCount; ++i)
                        {
                            // �ڽĵ��� ��ư�� �ִٸ�
                            if (tempBox.transform.GetChild(i).gameObject.CompareTag("Button"))
                            {
                                //Debug.log(tempBox);
                                var button = tempBox.transform.GetChild(i).GetComponent<ButtonIdentity>();

                                if (button.nowButtonParentState == eButtonParentState.ONFEVERBOX && button.buttonType == tempBox.GetNeedButton())
                                {
                                    //Debug.Log("���� �ƴٸ��� �¾Ҵ�?" + vKey);

                                    tempBox.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;

                                    button.nowButtonParentState = eButtonParentState.FEVERACTIVATE; // ��� ��ư�� FEVERACTIVATE�� �Ǹ� ī��Ʈ�� �ٿ����Ѵ�. 

                                    nowActivateCount++;

                                    SoundManager.Instance.getButton.Play();

                                    BoxClear();

                                }
                            }
                        }
                    }
                    else if (CheckFeverBoxButton(vKey, out tempBox) == false) // �����Ͱ� ��ġ�����ʾ����� // if�� ������ �ø��� �ٸ�Ű�� ���ܾ��� ��ų���ϴ�.
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
        if (3 <= nowActivateCount) // 3���� �ǹ��ڽ��� ��Ƽ����Ʈ ���״ٸ�
        {
            StartCoroutine(SoundManager.Instance.PlayDefuseButton());

            SoundManager.Instance.defuseBomb.Play();

            GameDifficulty.bombDefuseCount++; // ��üī��Ʈ ������Ų��.

            switch (GameDifficulty.bombDefuseCount)
            {
                case 1: Achievement.Instance.AchieveCheck(eAchieveState.Defuse1); break;
                case 5: Achievement.Instance.AchieveCheck(eAchieveState.Defuse5); break;
                case 10: Achievement.Instance.AchieveCheck(eAchieveState.Defuse10); break;
                case 20: Achievement.Instance.AchieveCheck(eAchieveState.Defuse20); break;
            }



            StartCoroutine(DefuseTextMove()); // �ؽ�Ʈ�����ϰ� �̵���Ŵ

            while (BombWick.BombWickIndex < 100)
            {
                BombWick.BomWickUp();       // Ǯ�Ƿ� ������ش�
            }

            ButtonIDpocket.Instance.ResetButtonType();

            // ��� �ǹ��ڽ��� needbutton�� �ٽ� �½�Ų��.
            foreach (var boxes in boxesCheck)
            {
                boxes.SetNeedButton();

                // ������ �ִ� ģ���� �о��
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

    private void Jump() // ĳ���� ������� �ֵ�����Ȱ��
    {
        rb.AddForce(Vector3.up * jumpPower);
        JumpStoper.isGround = false;
    }
    private void FirstButtonFuction() // ������ ����
    {
        BombWick.BomWickUp();
    }
    private void SecondButtonFunction()
    {
        BombWick.BomWickUp();
    }

    private bool CheckFeverBoxButton(KeyCode _keyCode, out FeverBox _feverBox)
    {
        // ButtonIDpoket�� ����ִ� list�� �˻��Ѵ�.
        // ������ư�� �ϳ��� ��ġ�ϸ� true�ݳ���Ų��.
        foreach (var val in ButtonIDpocket.Instance.lFeverBox) // ebuttonŸ������ ���ð��̴�.
        {
            if (val.GetNeedButton() == _keyCode) // �ڽ����ִ� need��ư�� �������� ��ư�� ��ġ�ϴٸ�
            {
                _feverBox = val;
                return true;    // ���� �ݳ��Ѵٴ� ���� �ǹ��ڽ��ȿ� �ùٸ� ��ư�� �����Ǿ��ְ� ���� �ش� ��ư�� �����ٴ� ���̴�.
            }
        }
        _feverBox = null;
        return false;
    }
}

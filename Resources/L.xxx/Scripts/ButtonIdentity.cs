using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// �� ��ư�� ������ �־���� ������
/// </summary>

public enum KeyCodeLast // Ű�ڵ忡 ���� ��Ÿ�������� foreach���� ���������� ���� ����.
{
    LAST = 510,
}

public enum eButtonType
{
    A = 97, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z/*, UP = 273, DOWN, RIGHT, LEFT, LEFTSHIFT = 304, ENTER, , LASTNUM*/
}

public enum eButtonParentState
{
    NONE, ONBUTTON, ONBOX, ONFEVERBOX, FEVERACTIVATE
}

public class ButtonIdentity : MonoBehaviour
{

    //[Header("������ư�ΰ�")]
    //     [SerializeField]
    //     public KeyCode buttonAlphabet;      // ��ưŰ ����

    [Header("������ư�ΰ�")]
    [SerializeField]
    public eButtonType whatButton;

    public KeyCode? buttonType;

    [Header("���� Material�� �־��ּ���")]
    public Material originalMaterial;

    [Header("�ٲ� Material�� �־��ּ���")]
    public Material coloredMaterial;

    public static int alphabetCount = 0;

    [Header("�� ��ġ��ŭ �־����� ��������Ų��")]
    public float distanceFromCharacter = 10f;

    public bool isSelectedButton = false;

    Transform characterTrasform;
    PlayerMove sPlayerMove;

    Renderer thisMaterial;

    public eButtonParentState nowButtonParentState;

    public KeyCode? GetKeyButtonType()
    {
        return buttonType;
    }

    public void SetKeyButtonType(KeyCode? _keycode)
    {
        buttonType = _keycode;
    }

    private void Awake()
    {
        thisMaterial = GetComponent<Renderer>();
        characterTrasform = GameObject.Find("Player").transform;
        sPlayerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

        switch (whatButton)
        {
            case eButtonType.A:
                buttonType = KeyCode.A;
                break;
            case eButtonType.B:
                buttonType = KeyCode.B;
                break;
            case eButtonType.C:
                buttonType = KeyCode.C;
                break;
            case eButtonType.D:
                buttonType = KeyCode.D;
                break;
            case eButtonType.E:
                buttonType = KeyCode.E;
                break;
            case eButtonType.F:
                buttonType = KeyCode.F;
                break;
            case eButtonType.G:
                buttonType = KeyCode.G;
                break;
            case eButtonType.H:
                buttonType = KeyCode.H;
                break;
            case eButtonType.I:
                buttonType = KeyCode.I;
                break;
            case eButtonType.J:
                buttonType = KeyCode.J;
                break;
            case eButtonType.K:
                buttonType = KeyCode.K;
                break;
            case eButtonType.L:
                buttonType = KeyCode.L;
                break;
            case eButtonType.M:
                buttonType = KeyCode.M;
                break;
            case eButtonType.N:
                buttonType = KeyCode.N;
                break;
            case eButtonType.O:
                buttonType = KeyCode.O;
                break;
            case eButtonType.P:
                buttonType = KeyCode.P;
                break;
            case eButtonType.Q:
                buttonType = KeyCode.Q;
                break;
            case eButtonType.R:
                buttonType = KeyCode.R;
                break;
            case eButtonType.S:
                buttonType = KeyCode.S;
                break;
            case eButtonType.T:
                buttonType = KeyCode.T;
                break;
            case eButtonType.U:
                buttonType = KeyCode.U;
                break;
            case eButtonType.V:
                buttonType = KeyCode.V;
                break;
            case eButtonType.W:
                buttonType = KeyCode.W;
                break;
            case eButtonType.X:
                buttonType = KeyCode.X;
                break;
            case eButtonType.Y:
                buttonType = KeyCode.Y;
                break;
            case eButtonType.Z:
                buttonType = KeyCode.Z;
                break;
                //             case eButtonType.LEFTSHIFT:
                //                 buttonType = KeyCode.LeftShift;
                //                 break;
                //             case eButtonType.UP:
                //                 buttonType = KeyCode.UpArrow;
                //                 break;
                //             case eButtonType.DOWN:
                //                 buttonType = KeyCode.DownArrow;
                //                 break;
                //             case eButtonType.LEFT:
                //                 buttonType = KeyCode.LeftArrow;
                //                 break;
                //             case eButtonType.RIGHT:
                //                 buttonType = KeyCode.RightArrow;
                //                 break;
                //             case eButtonType.ENTER:
                //                 buttonType = KeyCode.Return;
                //                 break;
        }
        if (this.gameObject.transform.GetChild(0).CompareTag("3DText"))
        {
            var text3d = this.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>();

            //text3d.transform.GetComponent<SetText>().myOriginalParent = this.gameObject.transform; // ���� �θ𿴴ٴ� ���� �����صд�. SetTExt����ϱ�����ģ������.

            text3d.text = this.buttonType.ToString();
        }

        nowButtonParentState = eButtonParentState.ONBUTTON;
    }

    private void Update()
    {
        // �θ� �Ϲ� �ڽ���� �ʷϻ����� �ٲ۴�.
        if (this.gameObject.transform.parent.CompareTag("Box") && this.gameObject.GetComponentInParent<BoxOutCheck>().mountedButton.buttonType == buttonType)
        {
            thisMaterial.material = coloredMaterial;
            nowButtonParentState = eButtonParentState.ONBOX;
            isSelectedButton = false;
        }
        //�ǹ��ڽ��̰� �� �ڽ��� �䱸�ϴ� ��ư�� ���� ���ٸ� �ϴ� �ʷϻ����� ĥ�غ���
        else if (this.gameObject.transform.parent.CompareTag("FeverBox") && buttonType == this.gameObject.transform.parent.GetComponent<FeverBox>().GetNeedButton() && nowButtonParentState != eButtonParentState.FEVERACTIVATE)
        {
            nowButtonParentState = eButtonParentState.ONFEVERBOX;
            thisMaterial.material = coloredMaterial;
            isSelectedButton = false;
        }
        else if (this.gameObject.transform.parent.name == "Buttons")
        {
            thisMaterial.material = originalMaterial;
            nowButtonParentState = eButtonParentState.NONE;
        }
        else if (nowButtonParentState == eButtonParentState.FEVERACTIVATE)
        {
            thisMaterial.material.color = Color.red;
        }
        // �θ� button�̸� �⺻������ ���ƿ����Ѵ�.
        else
        {
            thisMaterial.material = originalMaterial;
            isSelectedButton = false;
        }

        // ��ư�� ĳ���Ϳ��� �����Ÿ� �־����� ��������Ų��.
        if (distanceFromCharacter < Vector3.Distance(this.transform.position, characterTrasform.position))
        {
            if (this.transform.parent.name == "Buttons")
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                this.transform.position = new Vector3(characterTrasform.position.x + Random.Range(-12, 5), characterTrasform.position.y + Random.Range(1, 8), characterTrasform.position.z - Random.Range(30, 40));
            }
        }

        if (isSelectedButton) // �巡�׵Ǵٰ� ����� ��
        {
            this.transform.Translate(new Vector3(0, 0, 1) * sPlayerMove.speed * Time.deltaTime, Space.World);
        }

    }



    IEnumerator BeNotSelected()
    {
        yield return new WaitForSeconds(0.3f);

        isSelectedButton = false;
    }
}

// ��Ʈ������ ���� ���ڸ� ��� keycode�� ����� �� �� ��������;
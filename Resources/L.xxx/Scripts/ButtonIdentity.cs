using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 각 버튼이 가지고 있어야할 정보들
/// </summary>

public enum KeyCodeLast // 키코드에 끝을 나타내기위해 foreach돌때 문제가생겨 따로 뺐다.
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

    //[Header("무슨버튼인가")]
    //     [SerializeField]
    //     public KeyCode buttonAlphabet;      // 버튼키 종류

    [Header("무슨버튼인가")]
    [SerializeField]
    public eButtonType whatButton;

    public KeyCode? buttonType;

    [Header("원래 Material을 넣어주세요")]
    public Material originalMaterial;

    [Header("바뀔 Material을 넣어주세요")]
    public Material coloredMaterial;

    public static int alphabetCount = 0;

    [Header("이 수치만큼 멀어지면 리스폰시킨다")]
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

            //text3d.transform.GetComponent<SetText>().myOriginalParent = this.gameObject.transform; // 내가 부모였다는 것을 저장해둔다. SetTExt사용하기위한친구였다.

            text3d.text = this.buttonType.ToString();
        }

        nowButtonParentState = eButtonParentState.ONBUTTON;
    }

    private void Update()
    {
        // 부모가 일반 박스라면 초록색으로 바꾼다.
        if (this.gameObject.transform.parent.CompareTag("Box") && this.gameObject.GetComponentInParent<BoxOutCheck>().mountedButton.buttonType == buttonType)
        {
            thisMaterial.material = coloredMaterial;
            nowButtonParentState = eButtonParentState.ONBOX;
            isSelectedButton = false;
        }
        //피버박스이고 그 박스가 요구하는 버튼과 나의 같다면 일단 초록색으로 칠해보자
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
        // 부모가 button이면 기본색으로 돌아오게한다.
        else
        {
            thisMaterial.material = originalMaterial;
            isSelectedButton = false;
        }

        // 버튼이 캐릭터에서 일정거리 멀어지면 리스폰시킨다.
        if (distanceFromCharacter < Vector3.Distance(this.transform.position, characterTrasform.position))
        {
            if (this.transform.parent.name == "Buttons")
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                this.transform.position = new Vector3(characterTrasform.position.x + Random.Range(-12, 5), characterTrasform.position.y + Random.Range(1, 8), characterTrasform.position.z - Random.Range(30, 40));
            }
        }

        if (isSelectedButton) // 드래그되다가 쏘아질 때
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

// 스트링으로 받은 문자를 어떻게 keycode로 만들어 줄 수 있을까유;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIDpocket : MonoBehaviour
{
    private static ButtonIDpocket instance = null;
    /// <summary>
    /// 버튼의 값을 할당해준다.
    /// 또한 여기서 피버박스올바르게 누른 카운트를 세어준다. 
    /// </summary>

    static Dictionary<KeyCode?, bool> DbuttonID; // 식별용

    public static Dictionary<KeyCode?, bool> DbuttonCommand; // 스택용 쌓아서 누른버튼과 비교할 수 있게

    [SerializeField]
    public List<FeverBox> lFeverBox;

    private void PrintButtonID()
    {
        UIManager.Instance.debug.text = "clear";
        foreach (var s in DbuttonCommand.Keys)
        {
            UIManager.Instance.debug.text += s.ToString();
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        Init(); // new 해주는역할
    }

    private void Start()
    {
        PushButtonTypeForFeverBox();
    }

    public void PushButtonTypeForFeverBox() // 각 박스에다가 필요한 버튼을 넣어주기위한 함수
    {
        for (int i = 0; i < lFeverBox.Count; ++i)
        {
            lFeverBox[i].mountedButton.SetKeyButtonType(GetButtonType());
        }
    }

    public static ButtonIDpocket Instance
    {
        get { return instance; }
    }

    public static void Init()   // 초기화
    {
        DbuttonID = new Dictionary<KeyCode?, bool>();

        DbuttonCommand = new Dictionary<KeyCode?, bool>();

        for (int i = (int)eButtonType.A; i <= (int)eButtonType.Z; ++i)
        {
            DbuttonID.Add((KeyCode)i, true);
        }
    }


    public void ResetButtonType()
    {
        //for (int i = 0; i < DbuttonID.Count; ++i)

        for (int i = (int)eButtonType.A; i <= (int)eButtonType.Z; ++i)
        {
            DbuttonID[(KeyCode)i] = true;
        }

        //         foreach (var i in DbuttonID.Keys)
        //         {
        //             DbuttonID[i] = true;
        //         }
    }

    public KeyCode GetButtonType()
    {
        // 1. 랜덤한 수를 뽑는다. 
        // 2. 맵에 랜덤수로 key에 접근. 
        // 3. value를 false로 바꾼다. 
        // 4. 뽑힌 수를 리턴하다. 

        List<int> rangeInt = new List<int>();

        foreach (var val in Enum.GetValues(typeof(eButtonType)))
        {
            rangeInt.Add((int)val);
        }

        int rd = rangeInt[UnityEngine.Random.Range(0, rangeInt.Count)];

        if (DbuttonID[(KeyCode)rd] == true)
        {
            DbuttonID[(KeyCode)rd] = false;
            return (KeyCode)rd;
        }

        // 혹시 같은 이미 있는번호라면 재귀로 다시뽑아준다.

        return GetButtonType();
    }

    public void SetButtonType(KeyCode ret)
    {
        DbuttonID[ret] = true;
    }

}

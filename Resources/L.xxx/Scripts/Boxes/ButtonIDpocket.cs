using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIDpocket : MonoBehaviour
{
    private static ButtonIDpocket instance = null;
    /// <summary>
    /// ��ư�� ���� �Ҵ����ش�.
    /// ���� ���⼭ �ǹ��ڽ��ùٸ��� ���� ī��Ʈ�� �����ش�. 
    /// </summary>

    static Dictionary<KeyCode?, bool> DbuttonID; // �ĺ���

    public static Dictionary<KeyCode?, bool> DbuttonCommand; // ���ÿ� �׾Ƽ� ������ư�� ���� �� �ְ�

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

        Init(); // new ���ִ¿���
    }

    private void Start()
    {
        PushButtonTypeForFeverBox();
    }

    public void PushButtonTypeForFeverBox() // �� �ڽ����ٰ� �ʿ��� ��ư�� �־��ֱ����� �Լ�
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

    public static void Init()   // �ʱ�ȭ
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
        // 1. ������ ���� �̴´�. 
        // 2. �ʿ� �������� key�� ����. 
        // 3. value�� false�� �ٲ۴�. 
        // 4. ���� ���� �����ϴ�. 

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

        // Ȥ�� ���� �̹� �ִ¹�ȣ��� ��ͷ� �ٽû̾��ش�.

        return GetButtonType();
    }

    public void SetButtonType(KeyCode ret)
    {
        DbuttonID[ret] = true;
    }

}

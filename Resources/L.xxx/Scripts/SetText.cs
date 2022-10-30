using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �׻� ��ư�� ���ʿ� �����ϴ� UI�ν��� ����� �� �ؽ�Ʈ�� ���̰ڴ�.
/// </summary>
public class SetText : MonoBehaviour
{
    public Transform myOriginalParent;

    private void Awake()
    {
        this.gameObject.transform.SetParent(GameObject.Find("ButtonTexts").transform);

        GameDifficulty.delPrintOnText += this.PrintOnText;
    }

    public void PrintOnText()   // �����ϴ� ��ó�� ������ ����� �� ���� ���ϴ�.
    {
        this.transform.position = new Vector3(Mathf.Round(myOriginalParent.position.x * 10) / 10, Mathf.Round((myOriginalParent.position.y + 1.5f) * 10) / 10, Mathf.Round(myOriginalParent.position.z * 10) / 10);
    }
}

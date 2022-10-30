using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������ϸ鼭 Ű�� ������
public class AppearKey : MonoBehaviour
{
    Transform buttonParent;
    Transform characterTransform;

    int childIndex = 0;

    List<Transform> childrenButton = new List<Transform>();

    public static stdel deligatebutton; // �븮�ڸ� ����ƽ���� ���� �̱���ó�� ����� �� �ְ� �Ѵ�.

    private void Awake()
    {
        buttonParent = GameObject.Find("Buttons").transform;
        //childCount = buttonParent.childCount;
        characterTransform = GameObject.Find("Player").transform;
        childIndex = 0;
        deligatebutton = AppearKeyButton;

        foreach (Transform child in buttonParent)
        {
            childrenButton.Add(child);
        }
    }

    private void Start()
    {
        childIndex = 0;
    }

    public delegate void stdel();

    public void AppearKeyButton()   // ��Ȱ��ȭ ��ư�� Ȱ��ȭ ���� �����÷��� �� ��Ÿ���� �Ѵ�.
    {
        if (childrenButton.Count <= childIndex) return;

        var button = childrenButton[childIndex];
        button.transform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, characterTransform.position.z - 32);
        button.gameObject.SetActive(true);

        //Debug.Log("nowChildIndex = " + childIndex);

        childIndex++;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임진행하면서 키가 등장함
public class AppearKey : MonoBehaviour
{
    Transform buttonParent;
    Transform characterTransform;

    int childIndex = 0;

    List<Transform> childrenButton = new List<Transform>();

    public static stdel deligatebutton; // 대리자를 스테틱으로 만들어서 싱글톤처럼 사용할 수 있게 한다.

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

    public void AppearKeyButton()   // 비활성화 버튼을 활성화 시켜 게임플레이 중 나타나게 한다.
    {
        if (childrenButton.Count <= childIndex) return;

        var button = childrenButton[childIndex];
        button.transform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, characterTransform.position.z - 32);
        button.gameObject.SetActive(true);

        //Debug.Log("nowChildIndex = " + childIndex);

        childIndex++;
    }

}

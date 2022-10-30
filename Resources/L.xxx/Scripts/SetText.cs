using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 항상 버튼의 위쪽에 존재하는 UI로써의 기능을 할 텍스트를 붙이겠다.
/// </summary>
public class SetText : MonoBehaviour
{
    public Transform myOriginalParent;

    private void Awake()
    {
        this.gameObject.transform.SetParent(GameObject.Find("ButtonTexts").transform);

        GameDifficulty.delPrintOnText += this.PrintOnText;
    }

    public void PrintOnText()   // 진동하는 것처럼 보여서 사용할 수 없을 듯하다.
    {
        this.transform.position = new Vector3(Mathf.Round(myOriginalParent.position.x * 10) / 10, Mathf.Round((myOriginalParent.position.y + 1.5f) * 10) / 10, Mathf.Round(myOriginalParent.position.z * 10) / 10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  �巡�׵Ǵ� ������Ʈ�鿡�� ���� ��ũ��Ʈ
/// </summary>

public class DragObject : MonoBehaviour
{
    private Vector3 mouseOffset;

    private float mZCoord;
    public void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
        //         Debug.LogWarning("�׳� Ʈ������ ������" + transform.position);
        //         Debug.LogWarning("���� Ʈ������ ������" + transform.localPosition);
        //         Debug.LogWarning("���� Ʈ������ ������" + transform.TransformPoint(new Vector3(0, 0, 0)));
    }

}

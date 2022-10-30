using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  드래그되는 오브젝트들에게 붙일 스크립트
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
        //         Debug.LogWarning("그냥 트랜스폼 포지션" + transform.position);
        //         Debug.LogWarning("로컬 트랜스폼 포지션" + transform.localPosition);
        //         Debug.LogWarning("월드 트랜스폼 포지션" + transform.TransformPoint(new Vector3(0, 0, 0)));
    }

}

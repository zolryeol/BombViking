using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 부모가 가지고있는 스크립트에 나의 충돌 정보를 전달해서 넘겨주겠다.
public class ColliderBridge : MonoBehaviour
{
    PlayerMove listener;
    private void Awake()
    {
        listener = GetComponentInParent<PlayerMove>();
    }

    public void Initialize(PlayerMove _l)
    {
        listener = _l;
    }

    void OnTriggerEnter(UnityEngine.Collider collision)
    {
        listener.OnTriggerEnter(collision);
    }

}

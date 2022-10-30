using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// �θ� �������ִ� ��ũ��Ʈ�� ���� �浹 ������ �����ؼ� �Ѱ��ְڴ�.
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

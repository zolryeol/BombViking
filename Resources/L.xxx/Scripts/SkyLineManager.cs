using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스카이라인을 관리하는 매니저
/// </summary>

public class SkyLineManager : MonoBehaviour
{

    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;


    public Vector3 startPosition;
    private Vector3 nextPosition;

    private Queue<Transform> objectQueue;       // 큐를 이용한 오브젝트풀 관리

    private void Start()
    {
        objectQueue = new Queue<Transform>(numberOfObjects);

        nextPosition = startPosition;

        for (int i = 0; i < numberOfObjects; ++i)
        {
            Transform o = (Transform)Instantiate(prefab);
            o.localPosition = nextPosition;
            nextPosition.x += o.localScale.x;
            objectQueue.Enqueue(o);
        }
    }

}

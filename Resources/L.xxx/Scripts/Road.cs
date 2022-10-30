using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로드가 랜덤으로 계속 늘어나게 만든다.
/// 뒤에부분은 삭제해주어야한다.
/// </summary>

public class Road : MonoBehaviour
{
    public GameObject m_RoadPrefab;
    public GameObject sCharacter;

    int roadDeletCount;
    int roadlength;
    [Header("타일 세트 개수")]
    [SerializeField]
    int showRoadsCount;
    public int roadStartZPos = -100;

    static int length = 0;

    Queue<GameObject> ll_roads;

    GameObject roadParent;
    GameObject buildingParant;

    private void Awake()
    {
           length = 0;
        ll_roads = new Queue<GameObject>();
        roadlength = 25;
        roadDeletCount = 1;

        roadParent = GameObject.Find("RoadParent");
        buildingParant = GameObject.Find("BuildingParent");
    }

    void Update()
    {
        while (ll_roads.Count < showRoadsCount * 2)
        {
            var roadObj = ObjectPool.GetObject(eTileStyle.eRoad);

            //셋포지션과 로테이션을 같이해주는 함수가 있다?!
            roadObj.transform.SetPositionAndRotation(new Vector3(-4, 0, (-1 * (length * roadlength + roadStartZPos))), new Quaternion(0, 180, 0, 0));
            // 원래 사용했던 방법
            //            roadObj.transform.position = new Vector3(-4, 0, (-1 * (length * roadlength + roadStartZPos)));
            //            roadObj.transform.rotation = new Quaternion(0, 180, 0, 0);

            roadObj.transform.SetParent(roadParent.transform);
            ll_roads.Enqueue(roadObj);


            var buildingObj = ObjectPool.GetObject(eTileStyle.eBackGround);
            buildingObj.transform.position = new Vector3(0, 0, (-1 * (length * roadlength + roadStartZPos)));
            buildingObj.transform.SetParent(buildingParant.transform);
            ll_roads.Enqueue(buildingObj);


            length++;
            //             buildingObj.transform.position = new Vector3(4, 0, (roadStartZPos + (roadlength * lenth)));
            //             ll_roads.AddLast(buildingObj);

            //lenth++;
        }

        if (-1 * (sCharacter.transform.position.z) > roadDeletCount * 25)
        {
            ObjectPool.ReturnObject(ll_roads.Dequeue());

            ObjectPool.ReturnObject(ll_roads.Dequeue());

            roadDeletCount++;
        }
    }
}

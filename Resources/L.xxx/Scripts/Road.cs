using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ε尡 �������� ��� �þ�� �����.
/// �ڿ��κ��� �������־���Ѵ�.
/// </summary>

public class Road : MonoBehaviour
{
    public GameObject m_RoadPrefab;
    public GameObject sCharacter;

    int roadDeletCount;
    int roadlength;
    [Header("Ÿ�� ��Ʈ ����")]
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

            //�������ǰ� �����̼��� �������ִ� �Լ��� �ִ�?!
            roadObj.transform.SetPositionAndRotation(new Vector3(-4, 0, (-1 * (length * roadlength + roadStartZPos))), new Quaternion(0, 180, 0, 0));
            // ���� ����ߴ� ���
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

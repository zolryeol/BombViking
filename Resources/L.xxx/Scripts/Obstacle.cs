using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ֹ��� ����, �����ϴ� Ŭ����
/// 
/// ĳ������ ��ġ�� ��������  Z������ �������̸�ŭ ���� ��Ų��.
/// </summary>
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    List<GameObject> lObstacles = new List<GameObject>();

    public LinkedList<GameObject> llTrash;

    Transform characterPos;

    GameObject obstacleParent;

    [Header("��ֹ� ��ġ������")]
    [SerializeField]
    Vector3 obstaclePos = new Vector3(-4, 1.5f, 0);

    int counting = 1;

    [Header("ĳ���Ͱ� Distance��ŭ �̵��ϸ� ��ֹ� ������ �˻��Ѵ�")]
    [SerializeField]
    int distance = 10;

    [Header("��ֹ� ���� �� (���� �������� ���� ���� �ּ�1)")]
    [SerializeField]
    int frequancy = 10;
    private void Awake()
    {
        llTrash = new LinkedList<GameObject>();

        //lObstacles = new List<GameObject>();
        //         lObstacles.Add(Resources.Load<GameObject>("P.Resource/01.Props/PropsPrefabs/01.Medival Props/SM_Barrel01"));
        //         lObstacles.Add(Resources.Load<GameObject>("P.Resource/01.Props/PropsPrefabs/01.Medival Props/SM_Saw01"));
        //         lObstacles.Add(Resources.Load<GameObject>("P.Resource/01.Props/PropsPrefabs/01.Medival Props/SM_Grave002"));

        characterPos = GameObject.Find("Player").transform;
        obstacleParent = GameObject.Find("Obstacles"); ;
    }

    private void Update()
    {
        if (characterPos.position.z < -distance * counting)
        {
            counting++;
            if (Random.Range(0, frequancy) == 0)
            {
                var distance = characterPos.position.z - 40; //ĳ���ͺ��� 40��ŭ �տ��� ������ ���̴�.

                var temp = Instantiate(lObstacles[Random.Range(0, lObstacles.Count)], new Vector3(obstaclePos.x, obstaclePos.y, distance), Quaternion.identity);
                temp.tag = "Obstacle";
                temp.transform.SetParent(obstacleParent.transform);

                llTrash.AddLast(temp);
            }
        }

        if (llTrash.First != null)
        {
            if (characterPos.position.z + 5 < llTrash.First.Value.transform.position.z)
            {
                Destroy(llTrash.First.Value);

                llTrash.RemoveFirst();
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물을 생성, 삭제하는 클래스
/// 
/// 캐릭터의 위치를 기점으로  Z축으로 일정길이만큼 생성 시킨다.
/// </summary>
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    List<GameObject> lObstacles = new List<GameObject>();

    public LinkedList<GameObject> llTrash;

    Transform characterPos;

    GameObject obstacleParent;

    [Header("장애물 위치조절탭")]
    [SerializeField]
    Vector3 obstaclePos = new Vector3(-4, 1.5f, 0);

    int counting = 1;

    [Header("캐릭터가 Distance만큼 이동하면 장애물 유무를 검사한다")]
    [SerializeField]
    int distance = 10;

    [Header("장애물 등장 빈도 (값이 낮을수록 자주 등장 최소1)")]
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
                var distance = characterPos.position.z - 40; //캐릭터보다 40만큼 앞에서 등장할 것이다.

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

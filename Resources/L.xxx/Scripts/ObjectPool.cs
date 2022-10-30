using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로드 프리팹들을 가지고있는 오브젝트 풀을 만든다.
/// </summary>

public enum eTileStyle
{
    eRoad, eBackGround,
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public GameObject[] roadPrefab;
    GameObject[] buildingPrefab;

    Queue<GameObject> qPoolingRoad;
    Queue<GameObject> qPoolingBuilding;

    [SerializeField]
    int roadTileNum = 12; // 사용할 맵타일의 개수
    [SerializeField]
    int buildingNum = 12;

    private void Awake()
    {
        InitObjectPool();
    }

    public void InitObjectPool()
    {
        Instance = this;

        roadPrefab = new GameObject[roadTileNum];
        buildingPrefab = new GameObject[buildingNum];

        qPoolingRoad = new Queue<GameObject>();
        qPoolingBuilding = new Queue<GameObject>();


        LoadResources();
    }

    public void LoadResources()
    {
        // 일단 리소스를 로드한다.
        for (int i = 1; i < roadTileNum; ++i)
        {
            if (10 <= i)
            {
                roadPrefab[i] = Resources.Load<GameObject>("P.Resource/01.Props/SettingPrefabs/02.Road/Road Prefabs" + (i));
            }
            else
                roadPrefab[i] = Resources.Load<GameObject>("P.Resource/01.Props/SettingPrefabs/02.Road/Road Prefabs0" + (i));
        }

        for (int i = 1; i < buildingNum; ++i)
        {
            if (10 <= i)
            {
                buildingPrefab[i] = Resources.Load<GameObject>("P.Resource/01.Props/SettingPrefabs/01.BackGround/Building Prefabs" + (i));
            }
            else
                buildingPrefab[i] = Resources.Load<GameObject>("P.Resource/01.Props/SettingPrefabs/01.BackGround/Building Prefabs0" + (i));
        }

    }

    GameObject CreateObject(eTileStyle style)
    {
        switch (style)
        {
            case eTileStyle.eRoad:
                var roadObj = Instantiate(roadPrefab[Random.Range(1, roadTileNum - 1)]);
                qPoolingRoad.Enqueue(roadObj);
                return roadObj;

            case eTileStyle.eBackGround:
                var buildingObj = Instantiate(buildingPrefab[Random.Range(1, buildingNum - 1)]);
                qPoolingBuilding.Enqueue(buildingObj);
                return buildingObj;
            default: break;
        }
        return null;
    }

    public static GameObject GetObject(eTileStyle style)
    {
        int qStockCount = 30;

        switch (style)
        {
            case eTileStyle.eRoad:
                // Q안에 1개 이상 있다면
                if (qStockCount < Instance.qPoolingRoad.Count)
                {
                    var obj = Instance.qPoolingRoad.Dequeue();
                    obj.transform.SetParent(null);
                    obj.SetActive(true);
                    return obj;
                }
                // Q가 비어있다면 새로 만들어서 준다.
                else
                {
                    var newObject = Instance.CreateObject(style);
                    newObject.transform.SetParent(null);
                    newObject.SetActive(true);
                    return newObject;
                }

            case eTileStyle.eBackGround:
                if (qStockCount < Instance.qPoolingBuilding.Count)
                {
                    var obj = Instance.qPoolingBuilding.Dequeue();
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObject = Instance.CreateObject(style);
                    newObject.gameObject.SetActive(true);
                    newObject.transform.SetParent(null);
                    return newObject;
                }
        }

        return null;
    }

    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);


        if (obj.CompareTag("RoadTile"))
            Instance.qPoolingRoad.Enqueue(obj);

        else if (obj.CompareTag("BuildingTile"))
            Instance.qPoolingBuilding.Enqueue(obj);
    }

}

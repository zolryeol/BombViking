using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ȭ�� ����, �������� ĳ������ HP
/// </summary>

public class BombWick : MonoBehaviour
{
    static public int BombWickIndex = 102;

    public GameObject burnedWickPrefab;
    public GameObject nomalWickPrefab;
    static private Shader flicShader;
    static private Material flicMaterial;
    static private Material originalMaterial;

    public static List<GameObject> lWick;

    [SerializeField]
    Vector3 wickWorldPosition = new Vector3(-3, 12, -13);

    [SerializeField]
    Vector3 miniWickWorldPosition;

    [Header("��ȭ�� ����������")]
    [SerializeField]
    float miniWickWidthSize;

    float mostX = 0;    // ���� �˾Ƴ������� ����

    [SerializeReference]
    float boundWidth;   // ��ȭ�� ���λ�����

    MeshFilter mf;
    void Awake()
    {
        BombWickIndex = 102;
        lWick = new List<GameObject>();
        // �ѽ����� �ӽ����� ���� ��ġ�� �ε�, ���� ������ 12.3.��ŭ ���� ���̴�?
        miniWickWorldPosition = wickWorldPosition;

        var wick = Instantiate<GameObject>(burnedWickPrefab);
        wick.transform.position = wickWorldPosition;
        wick.transform.rotation = Quaternion.Euler(0, 90, 0);
        wick.transform.SetParent(this.transform);

        originalMaterial = wick.GetComponent<Renderer>().material;
        flicMaterial = (Material)Resources.Load("L.xxx/Materials/M_BombWickDanger");
        mf = wick.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;

        // ���� ���� �˾Ƴ���
        foreach (var vertice in vertices)
        {
            Vector3 pos = transform.TransformPoint(vertice);

            if (mostX < vertice.x)
            {
                mostX = vertice.x;
            }
        }
        // �������� ��ġ ��ų���̴�.
        miniWickWorldPosition.z += mostX;

        // ��ü�� ���λ�����ϱ�
        boundWidth = nomalWickPrefab.GetComponent<MeshRenderer>().bounds.size.z;

        for (int i = 0; i < 103; ++i)
        {
            var preWick = Instantiate<GameObject>(nomalWickPrefab);
            preWick.transform.localScale = new Vector3(miniWickWidthSize, miniWickWidthSize, miniWickWidthSize);
            preWick.transform.position = new Vector3(miniWickWorldPosition.x, miniWickWorldPosition.y, -0.24f + miniWickWorldPosition.z - (boundWidth * i));

            //preWick.transform.TransformPoint(miniWickWorldPosition);

            preWick.transform.rotation = Quaternion.Euler(0, 90, 0);
            preWick.transform.SetParent(this.transform);

            lWick.Add(preWick);
        }
    }

    private void Start()
    {
        BombWickIndex = 102;
    }

    static public void BombWickDown()
    {
        if (BombWickIndex < 1)
        {
            SoundManager.Instance.boom.Play();
            GameOver();
            return;
        }

        lWick[BombWickIndex].SetActive(false);

        BombWickIndex--;

        if (BombWickIndex < 11)
        {
            for (int i = 0; i < 11; ++i)
            {
                lWick[i].gameObject.GetComponent<Renderer>().material = flicMaterial;
            }
            SoundManager.Instance.dangerSound.Play();
        }

        else SoundManager.Instance.dangerSound.Pause();
    }

    static public void BomWickUp()
    {
        if (102 < BombWickIndex + 1) return;

        lWick[BombWickIndex + 1].SetActive(true);
        BombWickIndex++;

        if (11 <= BombWickIndex)
        {
            for (int i = 0; i < 11; ++i)
                lWick[i].gameObject.GetComponent<Renderer>().material = originalMaterial;
        }
    }

    static void GameOver()
    {
        //StartCoroutine(SlowDown());
        //Time.timeScale = 0;
        //SceneMG.Instance.LoadLogo();
        StaticCoroutine.DoSlowDownCoroutine();
    }

    //     IEnumerator SlowDown()
    //     {
    //         if (0 < Time.timeScale)
    //         {
    //             Time.timeScale -= 0.1f;
    //             yield return null;
    //             //StartCoroutine(SlowDown());
    //         }
    //         else
    //         {
    //             yield return null;
    //         }
    //     }
}

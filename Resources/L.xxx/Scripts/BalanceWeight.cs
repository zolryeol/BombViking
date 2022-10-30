using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �߽� �� �¿�� ����
/// �ð��� ���� ��ȭ, ĳ���� �浹�� ���� ��ȭ, ��ǲ�� ���� ��ȭ�Ѵ�.
/// </summary>


/// ������ CharacterTwitst Ŭ������ �־����� ���Ĺ��ȴ�.


/// <summary>
/// ĳ���Ͱ� �¿� ���̴� ���� ��Ÿ���� Ŭ����
/// 
/// �ִϸ����Ϳ� ���� ����� �̿��ϰ� �;����� �� �ȵǼ� �����ϰ� ����غ���.
/// </summary>
/// 

public class BalanceWeight : MonoBehaviour
{
    private static BalanceWeight instance = null;

    public GameObject balanceWeight;    // ������ ����� �� ���̴�.

    [Header("0�� ���� 1�� ������")]
    public GameObject[] bombObject = new GameObject[2];

    public Material balanceWeightDangerMaterial;
    Material balanceWeightOriginalMaterial;
    Renderer balanceWeightRenderer;

    public Material BombDangerMaterial;
    Material BombOriginalMaterial;

    Renderer[] bombRenderer = new Renderer[2];


    private Quaternion bwQuaternion;

    PlayerMove sPlayerMove;

    float leastY = 0;

    public float balanceWeightAngle = 0;

    /// characterTwitst Ŭ������ �ִ� ����
    [Header("���� ����")]
    public static float slopeAngle;
    [Header("ĳ���Ϳ� mixamorig:Spine �� �־���Ѵ�")]
    public Transform spine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        balanceWeightRenderer = balanceWeight.GetComponent<Renderer>();
        balanceWeightOriginalMaterial = balanceWeightRenderer.material;     // �������� �⺻ ���͸����� �����صд�.

        bombRenderer[0] = bombObject[0].gameObject.GetComponent<Renderer>();    // ���� ��ź
        bombRenderer[1] = bombObject[1].gameObject.GetComponent<Renderer>();    // ������ ��ź

        BombOriginalMaterial = bombRenderer[0].material;

        sPlayerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

        var mf = balanceWeight.GetComponent<MeshFilter>();

        Vector3[] vertices = mf.mesh.vertices;

        bwQuaternion = balanceWeight.transform.rotation;

        foreach (var vertice in vertices)
        {
            if (vertice.y < leastY)
            {
                leastY = vertice.y;
            }
        }
    }

    private void Start()
    {
        balanceWeightAngle = 0;

        slopeAngle = 0;
    }

    public static BalanceWeight Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void RotateBalanceWeight(float angle)
    {
        //balanceWeight.transform.Rotate(new Vector3(balanceWeight.transform.position.x, balanceWeight.transform.position.y, leastY), angle);
        balanceWeight.transform.Rotate(new Vector3(leastY, balanceWeight.transform.position.y, balanceWeight.transform.position.x), angle);
    }

    //     IEnumerator EffectTwice(int num)
    //     {
    //         for (int i = 0; i < 4; ++i)
    //         {
    //             EffectManager.Instance.PlayEffect(bombObject[num].gameObject.transform.position, Vector3.forward, bombObject[num].gameObject.transform, EffectManager.eEffectType.BombBoom);
    //             yield return new WaitForSeconds(0.2f);
    //         }
    //     }

    private void LateUpdate()
    {
        spine.rotation = Quaternion.Euler(spine.rotation.eulerAngles.x, spine.rotation.eulerAngles.y, spine.rotation.eulerAngles.z + slopeAngle);

        if (slopeAngle <= -90 || 90 <= slopeAngle)  // 90���̻� �Ѿ�� �ε��� �Լ� ȣ�� / �׸��� �⺻������
        {
            SoundManager.Instance.boom.Play();

            if (slopeAngle <= -90)  // ����Ʈ�߻�! 2�� ���� ��Ű������ �ڷ�ư
                StartCoroutine(EffectManager.Instance.BombEffectAfter(1, bombObject));

            else if (90 <= slopeAngle)
                StartCoroutine(EffectManager.Instance.BombEffectAfter(0, bombObject));

            // EffectManager.Instance.PlayEffect(bombObject[0].gameObject.transform.position, Vector3.forward, bombObject[1].gameObject.transform, EffectManager.eEffectType.BombBoom);

            sPlayerMove.Crashed();
            slopeAngle = 0;
            spine.rotation = Quaternion.Euler(spine.rotation.eulerAngles.x, spine.rotation.eulerAngles.y, slopeAngle);
            balanceWeight.transform.rotation = bwQuaternion;
        }

        if (-60 < slopeAngle && slopeAngle < 60 && balanceWeightRenderer.material != balanceWeightOriginalMaterial) //������ �����϶� ���� ���͸����� �������
        {
            balanceWeightRenderer.material = balanceWeightOriginalMaterial;
            bombRenderer[0].material = BombOriginalMaterial;
            bombRenderer[1].material = BombOriginalMaterial;
        }
    }

    public void flickerObject()
    {
        if (slopeAngle <= -60) // ������
        {
            balanceWeightRenderer.material = balanceWeightDangerMaterial;
            bombRenderer[1].material = BombDangerMaterial;
            //StartCoroutine(EffectManager.Instance.BombEffectTwice(1, bombObject));
        }
        else if (60 <= slopeAngle)  // ����
        {
            balanceWeightRenderer.material = balanceWeightDangerMaterial;
            bombRenderer[0].material = BombDangerMaterial;
        }
        //         else
        //         {
        //             balanceWeightRenderer.material = balanceWeightOriginalMaterial;
        //             bombRenderer[0].material = BombOriginalMaterial;
        //             bombRenderer[1].material = BombOriginalMaterial;
        //         }
    }

    public void RightSlope()
    {
        flickerObject();

        if (slopeAngle <= -60)
            StartCoroutine(EffectManager.Instance.BombEffectTwice(1, bombObject));

        slopeAngle -= 5f;

        RotateBalanceWeight(5);
    }
    public void LeftSlope()
    {
        flickerObject();

        if (60 <= slopeAngle)
            StartCoroutine(EffectManager.Instance.BombEffectTwice(0, bombObject));

        slopeAngle += 5;

        RotateBalanceWeight(-5);
    }
}

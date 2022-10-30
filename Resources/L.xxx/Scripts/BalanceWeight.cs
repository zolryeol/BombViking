using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 중심 추 좌우로 기울기
/// 시간에 따라 변화, 캐릭터 충돌에 의해 변화, 인풋에 의해 변화한다.
/// </summary>


/// 원래는 CharacterTwitst 클래스에 있었으나 합쳐버렸다.


/// <summary>
/// 캐릭터가 좌우 꺾이는 것을 나타내는 클래스
/// 
/// 애니메이터와 본의 기능을 이용하고 싶었으나 잘 안되서 무식하게 사용해본다.
/// </summary>
/// 

public class BalanceWeight : MonoBehaviour
{
    private static BalanceWeight instance = null;

    public GameObject balanceWeight;    // 무게추 끌어다 쓸 것이다.

    [Header("0이 왼쪽 1이 오른쪽")]
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

    /// characterTwitst 클래스에 있던 변수
    [Header("현재 각도")]
    public static float slopeAngle;
    [Header("캐릭터에 mixamorig:Spine 을 넣어야한다")]
    public Transform spine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        balanceWeightRenderer = balanceWeight.GetComponent<Renderer>();
        balanceWeightOriginalMaterial = balanceWeightRenderer.material;     // 무게추의 기본 머터리얼을 저장해둔다.

        bombRenderer[0] = bombObject[0].gameObject.GetComponent<Renderer>();    // 왼쪽 폭탄
        bombRenderer[1] = bombObject[1].gameObject.GetComponent<Renderer>();    // 오른쪽 폭탄

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

        if (slopeAngle <= -90 || 90 <= slopeAngle)  // 90도이상 넘어가면 부딪힘 함수 호출 / 그리고 기본값으로
        {
            SoundManager.Instance.boom.Play();

            if (slopeAngle <= -90)  // 이팩트발생! 2번 연속 시키기위해 코루튼
                StartCoroutine(EffectManager.Instance.BombEffectAfter(1, bombObject));

            else if (90 <= slopeAngle)
                StartCoroutine(EffectManager.Instance.BombEffectAfter(0, bombObject));

            // EffectManager.Instance.PlayEffect(bombObject[0].gameObject.transform.position, Vector3.forward, bombObject[1].gameObject.transform, EffectManager.eEffectType.BombBoom);

            sPlayerMove.Crashed();
            slopeAngle = 0;
            spine.rotation = Quaternion.Euler(spine.rotation.eulerAngles.x, spine.rotation.eulerAngles.y, slopeAngle);
            balanceWeight.transform.rotation = bwQuaternion;
        }

        if (-60 < slopeAngle && slopeAngle < 60 && balanceWeightRenderer.material != balanceWeightOriginalMaterial) //안정된 상태일때 경고용 머터리얼을 원래대로
        {
            balanceWeightRenderer.material = balanceWeightOriginalMaterial;
            bombRenderer[0].material = BombOriginalMaterial;
            bombRenderer[1].material = BombOriginalMaterial;
        }
    }

    public void flickerObject()
    {
        if (slopeAngle <= -60) // 오른쪽
        {
            balanceWeightRenderer.material = balanceWeightDangerMaterial;
            bombRenderer[1].material = BombDangerMaterial;
            //StartCoroutine(EffectManager.Instance.BombEffectTwice(1, bombObject));
        }
        else if (60 <= slopeAngle)  // 왼쪽
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

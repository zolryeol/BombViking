using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 스피드 조절
/// </summary>

public class PlayerMove : MonoBehaviour
{
    struct RunBoolSet
    {
        public bool run10;
        public bool run50;
        public bool run100;
        public bool run200;
        public bool run500;
        public bool run1000;
        public bool run2000;
    }

    //private CharacterController controller;

    public Animator animator;
    public Collider targetCollider;

    [Header("캐릭터 이동속도 앞에 - 붙일것")]
    [SerializeField]
    public float speed = -3f;

    [Header("애니메이터 속도조절")]
    [SerializeField]
    float animatorSpeed = 0.3f;

    BoxOutCheck[] boxes;
    FeverBox[] feverBox;

    public static bool isStart = false;

    public static float distanceFromStartLine;

    Transform startLine;

    RunBoolSet runBoolSet;
    public float GetSpeed()
    {
        return speed;
    }

    private void Awake()
    {
        speed = -3f;

        distanceFromStartLine = 0;

        startLine = GameObject.Find("StartLine").transform;

        boxes = new BoxOutCheck[3];
        feverBox = new FeverBox[3];

        targetCollider = GameObject.Find("Skeltal_Character").GetComponent<Collider>();

        // 콜라이더 브릿지를 활용해서 대상의 충돌체크를 나에게로 들고온다
        if (targetCollider.gameObject != this.gameObject)
        {
            ColliderBridge cb = targetCollider.gameObject.GetComponent<ColliderBridge>();
            cb.Initialize(this);
        }
        //controller = GetComponent<CharacterController>();

        for (int i = 0; i < 3; ++i) //
        {
            boxes[i] = this.transform.Find("Boxes").transform.GetChild(i).GetComponent<BoxOutCheck>();

            feverBox[i] = this.transform.Find("Boxes").transform.GetChild(3 + i).GetComponent<FeverBox>();
        }
    }
    // Update is called once per frame

    private void Start()
    {
        runBoolSet.run10 = false;
        runBoolSet.run50 = false;
        runBoolSet.run100 = false;
        runBoolSet.run200 = false;
        runBoolSet.run500 = false;
        runBoolSet.run1000 = false;
        runBoolSet.run2000 = false;
    }

    void Update()
    {
        distanceFromStartLine = (-1 * this.transform.position.z + startLine.position.z) / 3;   // 스타트라인으로부터의 거리를 잰다.
        UIManager.Instance.distanceFromStart.text = "<size= 40>" + "Distance" + "</size>" + "\n" + distanceFromStartLine.ToString("N2");

        if ((int)eAchieveState.Run2000 < distanceFromStartLine && runBoolSet.run2000 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run2000);
            runBoolSet.run2000 = true;
        }
        if ((int)eAchieveState.Run1000 < distanceFromStartLine && runBoolSet.run1000 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run1000);
            runBoolSet.run1000 = true;
        }
        if ((int)eAchieveState.Run500 < distanceFromStartLine && runBoolSet.run500 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run500);
            runBoolSet.run500 = true;
        }
        if ((int)eAchieveState.Run200 < distanceFromStartLine && runBoolSet.run200 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run200);
            runBoolSet.run200 = true;
        }
        if ((int)eAchieveState.Run100 < distanceFromStartLine && runBoolSet.run100 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run100);
            runBoolSet.run100 = true;
        }
        if ((int)eAchieveState.Run50 < distanceFromStartLine && runBoolSet.run50 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run50);
            runBoolSet.run50 = true;
        }
        if ((int)eAchieveState.Run10 < distanceFromStartLine && runBoolSet.run10 == false)
        {
            Achievement.Instance.AchieveCheck(eAchieveState.Run10);
            runBoolSet.run10 = true;
        }


        animator.SetFloat("CharacterMoveSpeed", -speed * animatorSpeed);

        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // 컨트롤러를 쓴다면
        //controller.Move(Vector3.forward * Time.deltaTime * speed);

        //(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // 스텐드업이 끝나면 일어나는 일들
        if (animator.GetBool("IsCollision") == false)
        {
            if (1.0f <= animator.GetCurrentAnimatorStateInfo(0).normalizedTime &&
                        animator.GetCurrentAnimatorStateInfo(0).IsName("StandUp"))
            {
                speed = -3;
                GameDifficulty.nowDifficulty = 1;
                GameDifficulty.playTimer = 0;
            }
        }
    }
    public void OnTriggerEnter(UnityEngine.Collider other)  // 캐릭터가 부딪히면!
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            SoundManager.Instance.boom.Play();

            Transform temp = other.transform;

            StartCoroutine(DisappearObstacle(temp));

            speed = 0; // 일단 멈춘다.

            animator.Play("FallDown");

            animator.SetBool("IsCollision", false);

            StartCoroutine(EffectManager.Instance.Crashing(targetCollider.gameObject));// 이팩트

            Crashed();  /// 버튼날리는거임 그리고 데미지 30줌;

            other.tag = "Untagged";

            GameDifficulty.nowDifficulty *= 10;
        }
    }

    public void Crashed()
    {
        for (int i = 0; i < 3; ++i)
        {
            boxes[i].FingerSnapButton();
            feverBox[i].FingerSnapButton();
        }

        // 45회 충격줄거임
        for (int i = 0; i < 45; ++i)
        {
            BombWick.BombWickDown();
            if (BombWick.BombWickIndex < 1) break;
        }
    }

    IEnumerator DisappearObstacle(Transform _collision)
    {
        yield return new WaitForSeconds(1.0f);
        //Error(_collision.name);
        _collision.gameObject.SetActive(false);  // 장애물을 없앤다 이팩트를 넣을지 말지 생각해보자
                                                 //yield break;
    }

}

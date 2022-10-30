using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 박스안에 버튼이 들어있나 안들어있나 체크
/// 들어있다 나갈때  힘을 가해준다.
/// </summary>
/// 
public class BoxOutCheck : MonoBehaviour
{
    [HideInInspector]
    public bool isGet = false;

    [Header("나갈때 튕기는 정도")]
    [SerializeField]
    float force = 5;
    //public Material beChangedMaterial; // 바뀔 버티리얼

    //private Material originalMaterial; //원래 머티리얼
    protected Transform buttonsParent;

    [HideInInspector]
    public ButtonIdentity mountedButton;   // 장착된 버튼

    protected Rigidbody holdRigidBody;

    public Material middleMaterial;

    public Transform assembleButton;

    private void Awake()
    {
        buttonsParent = GameObject.Find("Buttons").transform;
        /// MonoBehaviour이 붙은 클래스는 new를 할 수 없다. 그래서 addComponent 혹은 getcomponent로 붙여놔야한다.
        mountedButton = this.gameObject.AddComponent<ButtonIdentity>();
        this.gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("L .xxx/Materials/TransViewBOx");
    }

    virtual public void SetNeedButton()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // 새로 들어왔다면 가운데 위치에 고정시켜준다.
        if (other.CompareTag("Button"))
        {
            //middleMaterial = other.GetComponent<Renderer>().material;

            //Debug.Log(this.name + "으로부터 " + other.name + "가 들어왔다");

            other.transform.position = this.gameObject.transform.position;          // 버튼을 박스 중앙으로 넣어준다

            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);        // addforce로 날라가던 상태를 멈춘다.

            holdRigidBody = other.attachedRigidbody;                                // 나중에 다시 날리기위해 잠시 리지드바디를 받아둔다.

            mountedButton.buttonType = other.GetComponent<ButtonIdentity>().buttonType; // 장착된 버튼타입을 복사한다.

            other.transform.SetParent(this.transform);                                  // 들어온 버튼의 부모로 '나' 를 지정해준다.

            SoundManager.instance.buttonHold.Play();                                // 사운드 재생
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 나가면서 바꾸어준다.
        if (other.CompareTag("Button"))
        {
            other.transform.SetParent(buttonsParent.transform);     // 부모를 원래대로 바꾸어준다.

            //other.GetComponent<Renderer>().material = originalMaterial;

            holdRigidBody = null;
        }
        // 박스 안에 들어있다가 나갔다면 튕겨준다.
        //{
        //    //Debug.Log(this.name + "으로부터 " + other.name + "가 나갔다");

        //    //other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1) * force, ForceMode.Impulse);

        //    //mountedButton.buttonType = null;

        //    //StartCoroutine(WaitForGet());   // 계속 들어왔다 나갔다 무한반복하는 것을 방지하기위해
    }

    public void FingerSnapButton() // 버튼 튕겨내기
    {
        // 캐릭터가 장애물을 만나 넘어졌을 때 호출 한다.박스안에 있는 버튼들을 keyassemble 위치로 날린다.
        if (holdRigidBody)
        {
            var direc = assembleButton.transform.position - holdRigidBody.transform.position;
            holdRigidBody.AddForce(direc.normalized * force, ForceMode.Impulse);
            EffectManager.Instance.PlayEffect(this.gameObject.transform.position, Vector3.forward, this.gameObject.transform.GetChild(0), EffectManager.eEffectType.RedSmoke);
        }
        //holdRigidBody.AddForce(new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * force, ForceMode.Impulse); 기존 에드포스

    }

    public virtual void Update()
    {
        if (this.transform.childCount <= 2)
        {
            /// 현재 자식에 이팩트가 붙으면서  count 가 3으로 취급됌
            mountedButton.buttonType = null;
        }
//         else if (3 < this.transform.childCount) // 혹시 여러개가 박스에 들어왔다면
//         {
//             for (int i = 2; i < this.transform.childCount; ++i)
//             {
//                 if (this.transform.GetChild(i).transform.CompareTag("Button") == true 
//                     && this.mountedButton.buttonType != transform.GetChild(i).GetComponent<ButtonIdentity>().buttonType)
//                     this.transform.GetChild(i).GetComponent<Renderer>().material = middleMaterial;
//             }
//         }
    }
    IEnumerator WaitForGet()
    {
        yield return new WaitForSeconds(0.5f);
        //isGet = false;
        mountedButton.buttonType = null;
    }
}

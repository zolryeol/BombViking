using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ڽ��ȿ� ��ư�� ����ֳ� �ȵ���ֳ� üũ
/// ����ִ� ������  ���� �����ش�.
/// </summary>
/// 
public class BoxOutCheck : MonoBehaviour
{
    [HideInInspector]
    public bool isGet = false;

    [Header("������ ƨ��� ����")]
    [SerializeField]
    float force = 5;
    //public Material beChangedMaterial; // �ٲ� ��Ƽ����

    //private Material originalMaterial; //���� ��Ƽ����
    protected Transform buttonsParent;

    [HideInInspector]
    public ButtonIdentity mountedButton;   // ������ ��ư

    protected Rigidbody holdRigidBody;

    public Material middleMaterial;

    public Transform assembleButton;

    private void Awake()
    {
        buttonsParent = GameObject.Find("Buttons").transform;
        /// MonoBehaviour�� ���� Ŭ������ new�� �� �� ����. �׷��� addComponent Ȥ�� getcomponent�� �ٿ������Ѵ�.
        mountedButton = this.gameObject.AddComponent<ButtonIdentity>();
        this.gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("L .xxx/Materials/TransViewBOx");
    }

    virtual public void SetNeedButton()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // ���� ���Դٸ� ��� ��ġ�� ���������ش�.
        if (other.CompareTag("Button"))
        {
            //middleMaterial = other.GetComponent<Renderer>().material;

            //Debug.Log(this.name + "���κ��� " + other.name + "�� ���Դ�");

            other.transform.position = this.gameObject.transform.position;          // ��ư�� �ڽ� �߾����� �־��ش�

            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);        // addforce�� ���󰡴� ���¸� �����.

            holdRigidBody = other.attachedRigidbody;                                // ���߿� �ٽ� ���������� ��� ������ٵ� �޾Ƶд�.

            mountedButton.buttonType = other.GetComponent<ButtonIdentity>().buttonType; // ������ ��ưŸ���� �����Ѵ�.

            other.transform.SetParent(this.transform);                                  // ���� ��ư�� �θ�� '��' �� �������ش�.

            SoundManager.instance.buttonHold.Play();                                // ���� ���
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // �����鼭 �ٲپ��ش�.
        if (other.CompareTag("Button"))
        {
            other.transform.SetParent(buttonsParent.transform);     // �θ� ������� �ٲپ��ش�.

            //other.GetComponent<Renderer>().material = originalMaterial;

            holdRigidBody = null;
        }
        // �ڽ� �ȿ� ����ִٰ� �����ٸ� ƨ���ش�.
        //{
        //    //Debug.Log(this.name + "���κ��� " + other.name + "�� ������");

        //    //other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1) * force, ForceMode.Impulse);

        //    //mountedButton.buttonType = null;

        //    //StartCoroutine(WaitForGet());   // ��� ���Դ� ������ ���ѹݺ��ϴ� ���� �����ϱ�����
    }

    public void FingerSnapButton() // ��ư ƨ�ܳ���
    {
        // ĳ���Ͱ� ��ֹ��� ���� �Ѿ����� �� ȣ�� �Ѵ�.�ڽ��ȿ� �ִ� ��ư���� keyassemble ��ġ�� ������.
        if (holdRigidBody)
        {
            var direc = assembleButton.transform.position - holdRigidBody.transform.position;
            holdRigidBody.AddForce(direc.normalized * force, ForceMode.Impulse);
            EffectManager.Instance.PlayEffect(this.gameObject.transform.position, Vector3.forward, this.gameObject.transform.GetChild(0), EffectManager.eEffectType.RedSmoke);
        }
        //holdRigidBody.AddForce(new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f)) * force, ForceMode.Impulse); ���� ��������

    }

    public virtual void Update()
    {
        if (this.transform.childCount <= 2)
        {
            /// ���� �ڽĿ� ����Ʈ�� �����鼭  count �� 3���� ��މ�
            mountedButton.buttonType = null;
        }
//         else if (3 < this.transform.childCount) // Ȥ�� �������� �ڽ��� ���Դٸ�
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǹ��ڽ��� ��ũ��Ʈ 
/// �⺻ �ڽ��� ��ӹ޾� �����ȴ�.
/// </summary>

public class FeverBox : BoxOutCheck
{
    [SerializeField]
    KeyCode? needButtonType = null;

    public TextMesh textMesh;

    private void Awake()
    {
        buttonsParent = GameObject.Find("Buttons").transform;
        mountedButton = this.gameObject.AddComponent<ButtonIdentity>();
        ButtonIDpocket.Instance.lFeverBox.Add(this); // �ϴ� �ʿ��� ��ư���� ��Ƽ� ButtonIDpocket���� ó������

        textMesh = GetComponentInChildren<TextMesh>(); // needButton ��¿�
    }

    public override void Update()
    {
        if (this.transform.childCount <= 3)
        {
            /// ���� �ڽĿ� ����Ʈ�� �����鼭  count �� 3���� ��މ�
            mountedButton.buttonType = null;
        }
        //         else if (4 < this.transform.childCount) // Ȥ�� �������� �ڽ��� ���Դٸ�
        //         {
        //             for (int i = 1; i < this.transform.childCount - 1; ++i)
        //             {
        //                 if (this.transform.GetChild(i).transform.CompareTag("Button") == true 
        //                     && this.mountedButton.buttonType != transform.GetChild(i).GetComponent<ButtonIdentity>().buttonType)
        //                     this.transform.GetChild(i).GetComponent<Renderer>().material = middleMaterial;
        //             }
        //         }
    }

    public KeyCode? GetNeedButton()
    {
        return needButtonType;
    }

    private void Start()
    {
        SetNeedButtonInit();

        Debug.Log("�䱸�ϴ� ��ư Ÿ�� = " + needButtonType);
    }

    private void SetNeedButtonInit()
    {
        switch (this.gameObject.name)
        {
            case "FeverBox1":
                needButtonType = KeyCode.A;
                PrintNeedButtonText();
                return;
            case "FeverBox2":
                needButtonType = KeyCode.B;
                PrintNeedButtonText();
                return;
            case "FeverBox3":
                needButtonType = KeyCode.C;
                PrintNeedButtonText();
                return;
            case "FeverBox4":
                needButtonType = KeyCode.D;
                PrintNeedButtonText();
                return;
        }
    }

    public override void SetNeedButton()  // �� �ǹ��ڽ����� �ʿ��� ��ư�� �����ش�.
    {
        switch (this.gameObject.name)
        {
            case "FeverBox1":
                needButtonType = ButtonIDpocket.Instance.GetButtonType();
                //ButtonIDpocket.Instance.lFeverBox[0].mountedButton.GetKeyButtonType();
                PrintNeedButtonText();
                return;
            case "FeverBox2":
                needButtonType = ButtonIDpocket.Instance.GetButtonType();
                //ButtonIDpocket.Instance.lFeverBox[1].mountedButton.GetKeyButtonType();
                PrintNeedButtonText();
                return;
            case "FeverBox3":
                needButtonType = ButtonIDpocket.Instance.GetButtonType();
                //ButtonIDpocket.Instance.lFeverBox[2].mountedButton.GetKeyButtonType();
                PrintNeedButtonText();
                return;
            case "FeverBox4":
                needButtonType = ButtonIDpocket.Instance.GetButtonType();
                //ButtonIDpocket.Instance.lFeverBox[3].mountedButton.GetKeyButtonType();
                PrintNeedButtonText();
                return;
        }
    }


    private void PrintNeedButtonText()
    {
        textMesh.text = this.needButtonType.ToString(); // �׽�Ʈ�� �ý�Ʈ���
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ���Դٸ� ��� ��ġ�� ���������ش�.
        if (other.CompareTag("Button"))
        {
            textMesh.gameObject.SetActive(false);

            //Debug.Log(this.name + "���κ��� " + other.name + "�� ���Դ�");

            other.transform.position = this.gameObject.transform.position;          // ��ư�� �ڽ� �߾����� �־��ش�

            var rg = other.GetComponent<Rigidbody>();        // addforce�� ���󰡴� ���¸� �����.
            rg.velocity = new Vector3(0, 0, 0);
            //rg.isKinematic = true; 

            holdRigidBody = other.attachedRigidbody;                                // ���߿� �ٽ� ���������� ��� ������ٵ� �޾Ƶд�.

            mountedButton.buttonType = other.GetComponent<ButtonIdentity>().buttonType; // ������ ��ưŸ���� �����Ѵ�.

            mountedButton.SetKeyButtonType(other.GetComponent<ButtonIdentity>().GetKeyButtonType()); /// Getcompnent ���̴� ��� �����ϱ�

            other.transform.SetParent(this.transform);                                  // �θ�� '��' �� �������ش�.

            SoundManager.instance.buttonHold.Play();

            // �䱸�ϴ� ��ư�� ���� ������ư�� ��ġ�Ѵٸ� ��ųʸ��� �߰��� ���̴�.True �� ŰȮ�ο����� ����� ����
            if (needButtonType == mountedButton.GetKeyButtonType() && !ButtonIDpocket.DbuttonCommand.ContainsKey(needButtonType))
            {
                ButtonIDpocket.DbuttonCommand.Add(mountedButton.GetKeyButtonType(), true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �����鼭 �ٲپ��ش�.
        if (other.CompareTag("Button"))
        {
            textMesh.gameObject.SetActive(true);

            other.transform.SetParent(buttonsParent.transform);     // �θ� ������� �ٲپ��ش�.

            //other.GetComponent<Renderer>().material = originalMaterial;

            holdRigidBody = null;

            // ������  Ű�� �ٽ� false�� 
            if (ButtonIDpocket.DbuttonCommand.ContainsKey(needButtonType))
                ButtonIDpocket.DbuttonCommand[needButtonType] = false;
        }
    }
}

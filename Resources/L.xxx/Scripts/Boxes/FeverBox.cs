using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 피버박스용 스크립트 
/// 기본 박스를 상속받아 구성된다.
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
        ButtonIDpocket.Instance.lFeverBox.Add(this); // 일단 필요한 버튼들을 담아서 ButtonIDpocket에서 처리하자

        textMesh = GetComponentInChildren<TextMesh>(); // needButton 출력용
    }

    public override void Update()
    {
        if (this.transform.childCount <= 3)
        {
            /// 현재 자식에 이팩트가 붙으면서  count 가 3으로 취급됌
            mountedButton.buttonType = null;
        }
        //         else if (4 < this.transform.childCount) // 혹시 여러개가 박스에 들어왔다면
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

        Debug.Log("요구하는 버튼 타입 = " + needButtonType);
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

    public override void SetNeedButton()  // 각 피버박스에서 필요한 버튼을 정해준다.
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
        textMesh.text = this.needButtonType.ToString(); // 테스트용 택스트출력
    }

    private void OnTriggerEnter(Collider other)
    {
        // 새로 들어왔다면 가운데 위치에 고정시켜준다.
        if (other.CompareTag("Button"))
        {
            textMesh.gameObject.SetActive(false);

            //Debug.Log(this.name + "으로부터 " + other.name + "가 들어왔다");

            other.transform.position = this.gameObject.transform.position;          // 버튼을 박스 중앙으로 넣어준다

            var rg = other.GetComponent<Rigidbody>();        // addforce로 날라가던 상태를 멈춘다.
            rg.velocity = new Vector3(0, 0, 0);
            //rg.isKinematic = true; 

            holdRigidBody = other.attachedRigidbody;                                // 나중에 다시 날리기위해 잠시 리지드바디를 받아둔다.

            mountedButton.buttonType = other.GetComponent<ButtonIdentity>().buttonType; // 장착된 버튼타입을 복사한다.

            mountedButton.SetKeyButtonType(other.GetComponent<ButtonIdentity>().GetKeyButtonType()); /// Getcompnent 줄이는 방법 생각하기

            other.transform.SetParent(this.transform);                                  // 부모로 '나' 를 지정해준다.

            SoundManager.instance.buttonHold.Play();

            // 요구하는 버튼이 현재 가진버튼과 일치한다면 딕셔너리에 추가할 것이다.True 는 키확인용으로 사용할 것임
            if (needButtonType == mountedButton.GetKeyButtonType() && !ButtonIDpocket.DbuttonCommand.ContainsKey(needButtonType))
            {
                ButtonIDpocket.DbuttonCommand.Add(mountedButton.GetKeyButtonType(), true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 나가면서 바꾸어준다.
        if (other.CompareTag("Button"))
        {
            textMesh.gameObject.SetActive(true);

            other.transform.SetParent(buttonsParent.transform);     // 부모를 원래대로 바꾸어준다.

            //other.GetComponent<Renderer>().material = originalMaterial;

            holdRigidBody = null;

            // 나가면  키값 다시 false로 
            if (ButtonIDpocket.DbuttonCommand.ContainsKey(needButtonType))
                ButtonIDpocket.DbuttonCommand[needButtonType] = false;
        }
    }
}

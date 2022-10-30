using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���콺�� ��ŷ�ϴ� ����� ������ ����
/// </summary>

struct tempVector3
{
    public float x;
    public float y;
    public float z;

    public void SetPosition(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }

    public Vector3 GetPosition()
    {
        Vector3 temp;
        temp.x = this.x;
        temp.y = this.y;
        temp.z = this.z;

        return temp;
    }
}

public class MousePicking : MonoBehaviour
{
    [Header("��ŷ������ �󸶳� �ָ����� ����")]
    [SerializeField]
    float distance;

    [Header("���콺 �� �� ���� ���� ũ��")]
    [SerializeField]
    float power;

    public GameObject testBox;

    float maxDistance = 1000f;
    Ray ray;
    RaycastHit hit;
    public LayerMask layerMask;

    // ���콺 ����
    private Vector3 mouseOffset;

    private float mZCoord;

    tempVector3 keepOBJpos;

    private bool targetOn = false;

    long tempTime;

    float sCharacterSpeed;

    PlayerMove sPlayerMove;

    private void Awake()
    {
        hit = new RaycastHit();
        ray = new Ray();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        sPlayerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                SoundManager.instance.buttonCatch.Play();
                // ������Ʈ�� ���� ��ġ�ߴ� ���� �����Ѵ�.
                keepOBJpos.SetPosition(hit.transform.position);

                //�ϴ� ī�޶������ ���δ�.
                hit.transform.position = hit.transform.position + ToCameraVector(hit);

                //ī�޶���� distance��ŭ ���� ������Ʈ�� �����ϴ� �������� �о��.
                int counting = 0;

                while (counting < distance)
                {
                    hit.transform.position = hit.transform.position + ToObjectVector(keepOBJpos);
                    counting++;
                }

                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

                //Debug.LogWarning(hit.transform.name);

                mZCoord = Camera.main.WorldToScreenPoint(hit.transform.position).z;

                mouseOffset = hit.transform.position - GetMouseWorldPos();

                targetOn = true;
            }
        }

        // ������Ʈ�� �������¶�� ���콺�� ��ġ�� ������Ʈ�� ��ġ��Ų��.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (targetOn)
            {
                hit.transform.position = GetMouseWorldPos() + mouseOffset;

                #region �׽�Ʈ��
                //Vector3 tempPos = GetMouseWorldPos() + mouseOffset;

                //hit.transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, GetMouseWorldPos().y + mouseOffset.y, Camera.main.transform.position.z);
                // 
                //                 Debug.Log("��ư Ȧ����");
                // 
                //                 Debug.Log("���콺 z��ǥ = " + mZCoord);
                // 
                //                 Debug.Log("���콺 ������ = " + mouseOffset);
                #endregion
            }
        }

        if (targetOn == true && Input.GetMouseButtonUp(0))
        {
            var buttonController = hit.transform.GetComponent<ButtonIdentity>();
            buttonController.isSelectedButton = true;
            // ��ư�� ���� ���콺�� �ִ� ��ġ�� ������.
            Vector3 offset = Input.mousePosition;

            //power = power -= sPlayerMove.speed;
            ray = Camera.main.ScreenPointToRay(offset); // ������ temp��� input.mousePosition
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue, 1.0f);

            hit.rigidbody.velocity = Vector3.zero;
            hit.rigidbody.AddForce(ray.direction * power, ForceMode.Impulse);
            targetOn = false;

            SoundManager.instance.buttonThrow.Play();
            // ��ư�� ���� ������ ���� �־��� �������� ������./*ToObjectVector(keepOBJpos) */
        }
    }
    private Vector3 ToCameraVector(RaycastHit targetObject)
    {
        Vector3 length;
        length = Camera.main.transform.position - targetObject.transform.position;
        return length;
        //return length.normalized;   // ������Ʈ�κ��� ī�޶� ���� ũ�Ⱑ 1�� vector 
    }

    private Vector3 ToObjectVector(tempVector3 targetObject)
    //ī�޶�� ������Ʈ�� ����� ���̸� ���غ���
    {
        Vector3 length;
        length = targetObject.GetPosition() - Camera.main.transform.position;
        return length.normalized;// �븻�������ؼ� ������ ���Ѵ�  ī�޶󿡼����� ������Ʈ�� ���� ũ�Ⱑ 1�� vector�� ���ð��̴�.
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;           // �⺻ ���콺 ������;

        mousePoint.z = mZCoord;                             // ī�޶��� �������� ���� -> ��ũ������ �ٲ��� �� Z��;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // 
    //     public void OnPointerDown(PointerEventData eventData)
    //     {
    //         Debug.Log("OnPointerDown");
    //         OnDrag(eventData);
    //     }
    // 
    //     public void OnDrag(PointerEventData eventData)
    //     {
    //         Debug.Log("OnDrag");
    //         targetObejct.transform.position = eventData.position;
    //     }
    // 
    //     public void OnEndDrag(PointerEventData eventData)
    //     {
    //         Debug.Log("OnEndDrag");
    //     }

    // 
    //     private void OnMouseDown()
    //     {
    //         mZCoord = Camera.main.WorldToScreenPoint(targetObejct.gameObject.transform.position).z;
    // 
    //         mouseOffset = targetObejct.transform.position - GetMouseWorldPos();
    //     }
    // 
    //     private void OnMouseDrag()
    //     {
    //         targetObejct.transform.position = GetMouseWorldPos() + mouseOffset;
    //     }
    // 
    //     private Vector3 GetMouseWorldPos()
    //     {
    //         Vector3 mousePoint = Input.mousePosition;
    // 
    //         mousePoint.z = mZCoord;
    // 
    //         return Camera.main.ScreenToWorldPoint(mousePoint);
    //     }

}
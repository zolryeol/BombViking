using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스로 피킹하는 기능을 구현할 것임
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
    [Header("피킹했을때 얼마나 멀리있을 건지")]
    [SerializeField]
    float distance;

    [Header("마우스 땔 때 날릴 힘의 크기")]
    [SerializeField]
    float power;

    public GameObject testBox;

    float maxDistance = 1000f;
    Ray ray;
    RaycastHit hit;
    public LayerMask layerMask;

    // 마우스 관련
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
                // 오브젝트가 원래 위치했던 곳을 저장한다.
                keepOBJpos.SetPosition(hit.transform.position);

                //일단 카메라앞으로 붙인다.
                hit.transform.position = hit.transform.position + ToCameraVector(hit);

                //카메라부터 distance만큼 원래 오브젝트가 존재하던 방향으로 밀어낸다.
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

        // 오브젝트를 누른상태라면 마우스의 위치에 오브젝트를 위치시킨다.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (targetOn)
            {
                hit.transform.position = GetMouseWorldPos() + mouseOffset;

                #region 테스트용
                //Vector3 tempPos = GetMouseWorldPos() + mouseOffset;

                //hit.transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, GetMouseWorldPos().y + mouseOffset.y, Camera.main.transform.position.z);
                // 
                //                 Debug.Log("버튼 홀드중");
                // 
                //                 Debug.Log("마우스 z좌표 = " + mZCoord);
                // 
                //                 Debug.Log("마우스 오프셋 = " + mouseOffset);
                #endregion
            }
        }

        if (targetOn == true && Input.GetMouseButtonUp(0))
        {
            var buttonController = hit.transform.GetComponent<ButtonIdentity>();
            buttonController.isSelectedButton = true;
            // 버튼을 때면 마우스가 있는 위치로 날린다.
            Vector3 offset = Input.mousePosition;

            //power = power -= sPlayerMove.speed;
            ray = Camera.main.ScreenPointToRay(offset); // 원래는 temp대신 input.mousePosition
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue, 1.0f);

            hit.rigidbody.velocity = Vector3.zero;
            hit.rigidbody.AddForce(ray.direction * power, ForceMode.Impulse);
            targetOn = false;

            SoundManager.instance.buttonThrow.Play();
            // 버튼을 때면 가지고 원래 있었던 방향으로 날린다./*ToObjectVector(keepOBJpos) */
        }
    }
    private Vector3 ToCameraVector(RaycastHit targetObject)
    {
        Vector3 length;
        length = Camera.main.transform.position - targetObject.transform.position;
        return length;
        //return length.normalized;   // 오브젝트로부터 카메라를 보는 크기가 1인 vector 
    }

    private Vector3 ToObjectVector(tempVector3 targetObject)
    //카메라와 오브젝트의 방향과 길이를 구해보자
    {
        Vector3 length;
        length = targetObject.GetPosition() - Camera.main.transform.position;
        return length.normalized;// 노말라이즈해서 방향을 구한다  카메라에서부터 오브젝트를 보는 크기가 1인 vector가 나올것이다.
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;           // 기본 마우스 포지션;

        mousePoint.z = mZCoord;                             // 카메라의 포지션을 월드 -> 스크린으로 바꾼후 의 Z값;

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
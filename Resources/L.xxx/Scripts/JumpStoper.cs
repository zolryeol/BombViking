using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStoper : MonoBehaviour
{
    [SerializeField]
    public string nowStateGround;
    public static bool isGround = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("RoadTile"))
        {
            isGround = true;
        }
    }
    private void Update()
    {
        if (nowStateGround != isGround.ToString())
            Debug.Log("현재 바닥인가?" + isGround);

        nowStateGround = isGround.ToString();
    }
}

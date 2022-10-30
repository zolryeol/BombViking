using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    public GameObject character;

    private Vector3 cameraPos;
    float offset = -12.1f;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = this.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.gameObject.transform.position = new Vector3(cameraPos.x, cameraPos.y, character.transform.position.z + offset);
    }
}

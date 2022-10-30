using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    BoxCollider boxCollider;

    [SerializeField]
    public float bouncePower = 1f;

    [Header("어느방향으로 튕길건지")]
    [SerializeField]
    int x = 0;
    [SerializeField]
    int y = 0;
    [SerializeField]
    int z = 0;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        switch (this.gameObject.name)
        {
            case "Top":
                //y = -1;
                break;
            case "Bottom":
                //y = 1;
                break;
            case "Left":
                //z = -1;
                break;
            case "Right":
                //z = 1;
                break;
            case "Front":
                //x = -1;
                break;
            case "Bottom2":
                //    y = 1;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, -z) * bouncePower, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, -z) * bouncePower, ForceMode.Impulse);
    }


}

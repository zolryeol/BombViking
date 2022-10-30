using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject character;

    public float speed;

    public Vector3 characterPosition;

    public static float distanceTraveled;

    private void Awake()
    {
        speed = 4f;
        character.transform.position = characterPosition;
    }
    void Update()
    {
        character.transform.Translate(0, 0, 1 * Time.deltaTime * speed);

        distanceTraveled = transform.localPosition.x;
    }
}

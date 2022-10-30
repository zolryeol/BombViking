using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruchButton : MonoBehaviour
{
    public AudioSource CrushButton;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 1)
        {
            CrushButton.Play();

        }
    }
}

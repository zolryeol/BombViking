using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    public AudioSource footSound;
    private void Step()
    {
        footSound.Play();
    }
    // Update is called once per frame
}

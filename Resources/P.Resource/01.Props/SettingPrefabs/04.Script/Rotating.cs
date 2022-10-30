using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
  
    float y;
    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime * 20;
        transform.rotation = Quaternion.Euler(0, 0
         , y);

    }
}

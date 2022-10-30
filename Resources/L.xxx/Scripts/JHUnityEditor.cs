using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHUnityEditor : MonoBehaviour
{
    public float width = 32.0f;
    public float height = 32.0f;

    public new Camera camera;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3[] frustumCorners = new Vector3[4];
        Vector3[] frustumCornersnear = new Vector3[4];


        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCornersnear);


        for (int i = 0; i < 4; ++i)
        {
            var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
            Gizmos.DrawRay(camera.transform.position, worldSpaceCorner);

            //             var worldSpaceCornernear = camera.transform.TransformVector(frustumCornersnear[i]);
            //             Gizmos.DrawLine(camera.transform.position, worldSpaceCornernear);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerUnlocked : MonoBehaviour
{
    public Transform target;
    public float pLerp = .02f;
    public float rLerp = .01f;

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, target.position, pLerp), Quaternion.Lerp(transform.rotation, target.rotation, rLerp));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Yeah ok. This controls like the thing cinemachine follows
// (so it has a fixed angle)
// no actual camera here

public class CamController : MonoBehaviour
{
    public Transform target;
    public float lerpMultiplier = .02f;
    public float offsetY, offsetZ, offsetXAngle;

    private void Start()
    {
        offsetY = transform.localPosition.y;
        offsetZ = transform.localPosition.z;
        offsetXAngle = transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetWithOffset = new Vector3(target.position.x, target.position.y + offsetY, target.position.z + offsetZ);
        //transform.position = (Vector3.Lerp(transform.position, targetWithOffset, lerpMultiplier));
        transform.position = targetWithOffset;
    }
}

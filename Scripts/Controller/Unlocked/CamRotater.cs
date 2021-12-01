using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This just does a lil zoom up thing..
// Kinda like the acnh camera?? When you pan in??

public class CamRotater : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = .5f;
    public float lerpMultiplier = .1f;

    // Update is called once per frame
    void Update()
    {
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        turn.y = Mathf.Clamp(turn.y, -10f, 5f);
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        GetComponent<CamController>().offsetY
            = Mathf.Lerp(GetComponent<CamController>().offsetY,
            3.6f - turn.y *.2f, lerpMultiplier);

        GetComponent<CamController>().offsetZ
            = Mathf.Lerp(GetComponent<CamController>().offsetZ,
            -4.2f + turn.y * .2f, lerpMultiplier);
    }
}

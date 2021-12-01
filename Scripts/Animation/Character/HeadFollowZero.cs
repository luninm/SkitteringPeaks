using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * ~ * ~ Welcome to rotation hell * ~ * ~

// Is it positive? is it negative? now its backwards?..Quaternion who?
// I'm losing my mind.
// ...
// This is so the characters awkwardly stare at each other.
// And sometimes a head will spin around backwards for added ~ * spook * ~
// 100% intentional yeah.

public class HeadFollowZero : MonoBehaviour
{
    public Transform headAim;
    public float yRotationLimit = 110f;
    public float xRotationLimit = 20f;
    public float lerp = .05f;
    public bool flipZ = false;

    private Quaternion initRoation;

    private void Start()
    {
        initRoation = transform.localRotation;
    }

    private void OnDisable()
    {
        transform.localRotation = initRoation;
    }

    void LateUpdate()
    {
        // Dont ask...

        // Negative angles are haunted so we must fix them yes.
        var parenty = transform.parent.parent.parent.eulerAngles.y;

        // Bug fix (side look)
        if (!headAim.GetComponent<Follow>().closeEnough)
            parenty = 0f;

        // Set adjusted values
        var rotx = 0f;
        var roty = headAim.eulerAngles.y - parenty;
        var rotz = -headAim.eulerAngles.x;

        if (flipZ)
            rotz *= -1f;

        // Limit horizontal rotation (so it doesn't spin backward like a demon head)
        // .. it still does though. Sometimes.
        if ((roty > yRotationLimit && roty < 360f - yRotationLimit) || roty < -yRotationLimit)
        {
            rotz = 0f;
            roty = 0f;
        }
        if ((rotz > xRotationLimit && rotz < 360f - xRotationLimit) || rotz < -xRotationLimit)
        {
            rotz = Mathf.Clamp(rotz, -xRotationLimit, xRotationLimit);
        }


        var newRotation = Quaternion.Euler(rotx, roty, rotz);

        Quaternion from = transform.localRotation;
        Quaternion to = newRotation;

        transform.localRotation = Quaternion.Lerp(from, to, lerp);

    }
}

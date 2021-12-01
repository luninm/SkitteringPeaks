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

public class HeadFollowAnim : MonoBehaviour
{
    public Transform headAim;
    public float yRotationLimit = 110f;
    public float xRotationLimit = 20f;
    public float lerp = .05f;

    public bool notMessedUp = false;



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
        var rotx = headAim.eulerAngles.z;
        var roty = headAim.eulerAngles.x;
        var rotz = headAim.eulerAngles.y - parenty;

        // Limit horizontal rotation (so it doesn't spin backward like a demon head)
        // .. it still does though. Sometimes.
        if ((rotz > yRotationLimit && rotz < 360f - yRotationLimit) || rotz < -yRotationLimit)
        {
            rotz = 0f;
            roty = 0f;
        }
        if ((roty > xRotationLimit && roty < 360f - xRotationLimit) || roty < -xRotationLimit)
        {
            roty = Mathf.Clamp(roty, -xRotationLimit, xRotationLimit);
            rotx = Mathf.Clamp(rotx, -xRotationLimit, xRotationLimit);
        }


        var newRotation = Quaternion.Euler(rotx, roty, rotz);

        Quaternion from = transform.localRotation;
        Quaternion to = newRotation;

        transform.localRotation = Quaternion.Lerp(from, to, lerp);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is for the thing that the head rotation will aim at
// When stalking something

// See HeadFollowAnim to see how horrible this turned out.

public class Follow : MonoBehaviour
{
    public Transform target;
    public Transform lookAt;
    public float lerp = .2f;
    public float lookAtRadius = 5f;
    public LayerMask layerMask = 3;
    public bool closeEnough; // For testing

    float distance;



    // Update is called once per frame
    void Update()
    {
        // Face any NPC in radius of player
        Vector3 p1 = target.position;
        Collider[] hitColliders = Physics.OverlapSphere(p1, lookAtRadius, layerMask);

        // Only face one
        if(hitColliders.Length > 0)
            lookAt = hitColliders[0].gameObject.transform;
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, lerp);

        if (lookAt != null)
            distance = Vector3.Distance(target.position, lookAt.position);
        else
            distance = 666f;

        if (distance > lookAtRadius)
        {
            closeEnough = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, lerp);
            return;
        }

        closeEnough = true;

        transform.LookAt(lookAt);
    }
}



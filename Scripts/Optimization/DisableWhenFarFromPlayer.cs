using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disables colliders when far enough from player
// literally brought the fps up like 20 frames...

public class DisableWhenFarFromPlayer : MonoBehaviour
{
    GameObject player;
    public float disableDistance = 10f;
    float distance;

    bool oldState;
    bool newState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > disableDistance)
            oldState = false;
        else
            oldState = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        // disable colliders;
        if (distance > disableDistance)
        {
            newState = false;

            if (oldState != newState)
            {
                oldState = newState;
                MeshCollider mc;
                TryGetComponent(out mc);
                if (mc) { mc.enabled = false; }

                BoxCollider bc;
                TryGetComponent(out bc);
                if (bc) { bc.enabled = false; }

                CapsuleCollider cc;
                TryGetComponent(out cc);
                if (cc) { cc.enabled = false; }

                SphereCollider sc;
                TryGetComponent(out sc);
                if (sc) { sc.enabled = false; }
            }
        }
        else
        {
            newState = true;

            if (oldState != newState)
            {
                oldState = newState;

                MeshCollider mc;
                TryGetComponent(out mc);
                if (mc) { mc.enabled = true; }

                BoxCollider bc;
                TryGetComponent(out bc);
                if (bc) { bc.enabled = true; }

                CapsuleCollider cc;
                TryGetComponent(out cc);
                if (cc) { cc.enabled = true; }

                SphereCollider sc;
                TryGetComponent(out sc);
                if (sc) { sc.enabled = true; }
            }
        }

    }


}

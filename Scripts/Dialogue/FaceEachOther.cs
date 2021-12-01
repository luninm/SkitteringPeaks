using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FaceEachOther : MonoBehaviour
{
    public float lerp = .1f;
    public CinemachineTargetGroup targetGroup;

    public Transform target1, target2;
    Vector3 lookPos;
    Quaternion rotation;

    
    
    // Update is called once per frame
    void Update()
    {
        if (targetGroup.m_Targets.Length > 1)
        {
            target1 = targetGroup.m_Targets[0].target;
            target2 = targetGroup.m_Targets[1].target;

            // first: usually the player
            lookPos = target2.position - target1.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
            target1.rotation = Quaternion.Slerp(target1.rotation, rotation, lerp);

            // if distance is too small scoot the player away
            var distance = Vector3.Distance(target1.position, target2.position);
            if (distance < 2.5f)
                target1.Translate(Vector3.back * Time.deltaTime, Space.Self);

            // second: npc
            lookPos = target1.position - target2.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
            target2.rotation = Quaternion.Slerp(target2.rotation, rotation, lerp);

            targetGroup.m_Targets[0].target = target1;
            targetGroup.m_Targets[1].target = target2;
        }
    }
}

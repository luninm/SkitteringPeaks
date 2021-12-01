using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uh so this is an animation system.
// This was a terrible idea.
// ...
// Yeah none of the models are rigged... to save time (lolol)
// boy was I wrong..
// Welcome to hell i guess

public class AnimManager : MonoBehaviour
{
    public Anim anim = Anim.idle;
    Anim currentAnim;
    public GameObject armL, armR, legL, legR, torso, head;
    public Rot idle, thinking, running, normal, yelling, worried, question;

    private void Start()
    {
        currentAnim = Anim.init;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnim == anim)
        {
            return;
        }

        if (anim == Anim.idle)
        {
            StartIdle();
            currentAnim = Anim.idle;
        }
        else if (anim == Anim.runnning)
        {
            StartRunning();
            currentAnim = Anim.runnning;
        }
        else if (anim == Anim.talking)
        {
            StartHead();
            currentAnim = Anim.talking;
        }
        else
        {
            StartIdle();
            currentAnim = Anim.idle;
        }

    }

    // NON DIALOGUE actions

    public void StartRunning()
    {
        StopHead();
        head.GetComponent<RotateAnim>().SetRotationValues(running.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(running.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(running.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(running.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(running.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(running.legR);
    }

    public void StartIdle()
    {
        StopHead();
        head.GetComponent<RotateAnim>().SetRotationValues(idle.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(idle.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(idle.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(idle.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(idle.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(idle.legR);
    }

    // DIALOGUE actions

    public void StartThinking()
    {
        head.GetComponent<RotateAnim>().SetRotationValues(thinking.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(thinking.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(thinking.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(thinking.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(thinking.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(thinking.legR);
    }

    public void StartNormal()
    {
        head.GetComponent<RotateAnim>().SetRotationValues(normal.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(normal.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(normal.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(normal.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(normal.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(normal.legR);
    }

    public void StartYelling()
    {
        head.GetComponent<RotateAnim>().SetRotationValues(yelling.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(yelling.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(yelling.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(yelling.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(yelling.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(yelling.legR);
    }

    public void StartWorried()
    {
        head.GetComponent<RotateAnim>().SetRotationValues(worried.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(worried.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(worried.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(worried.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(worried.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(worried.legR);
    }

    public void StartQuestion()
    {
        head.GetComponent<RotateAnim>().SetRotationValues(question.head);
        torso.GetComponent<RotateAnim>().SetRotationValues(question.torso);
        armL.GetComponent<RotateAnim>().SetRotationValues(question.armL);
        armR.GetComponent<RotateAnim>().SetRotationValues(question.armR);
        legL.GetComponent<RotateAnim>().SetRotationValues(question.legL);
        legR.GetComponent<RotateAnim>().SetRotationValues(question.legR);
    }


    void StartHead()
    {
        head.GetComponent<RotateAnim>().enabled = true;
    }

    void StopHead()
    {
        head.GetComponent<RotateAnim>().enabled = false;
    }

}

[System.Serializable]
public class Rot
{
    public RotValues torso, head, armL, armR, legL, legR;
}

[System.Serializable]
public class RotValues
{
    public Vector3 from;
    public Vector3 to;
    public float freq;
}

public enum Anim
{
    idle,
    runnning,
    talking,
    init,
}


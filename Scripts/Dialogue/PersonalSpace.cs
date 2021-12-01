using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalSpace : MonoBehaviour
{
    public string diaFile;
    public GameObject diaObj;
    public OnEvent onEvent;
    public Transform player;
    public float range = .1f;
    public ConvoManager convoManager;


    // public for testing
    public float closeness = 666f;
    public bool readyToStart;

    float timer;
    public float tooCloseTime = 3f;


    void OnEnable()
    {
        readyToStart = true;
        convoManager = gameObject.GetComponent<ConvoManager>();
        closeness = 666f;

    }

    // Update is called once per frame
    void Update()
    {
        closeness = Vector3.Distance(transform.position, player.position);

        if (closeness <= range)
        {
            timer += Time.deltaTime;

            // player is TOO close.. start complaining
            if (readyToStart && timer > tooCloseTime)
            {
                convoManager.enabled = false;
                readyToStart = false;
                onEvent.StartDialogue(diaFile);
            }
        }
        if (!readyToStart && !diaObj.activeInHierarchy)
        {
            timer = 0f;
            readyToStart = true;
            convoManager.enabled = true;
        }

    }

    public void ChangeConvo(string file)
    {
        diaFile = file;
    }
}

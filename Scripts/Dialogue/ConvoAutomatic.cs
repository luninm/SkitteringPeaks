using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConvoAutomatic : MonoBehaviour
{
    public string diaFile;
    public GameObject diaObj;
    public OnEvent onEvent;
    public Transform player;
    public float range = 4f;
    public ConvoManager convoManager;


    // public for testing
    public float closeness = 666f;
    public bool readyToStart;
    public string id;

    public bool sceneDoorOnFinish = false;
    public GameObject sceneDoor;


    void OnEnable()
    {
        // Only Run on first interaction
        id = gameObject.name + SceneManager.GetActiveScene().name + "Auto";
        if (PlayerPrefs.GetInt(id, 0) != 0)
        {
            enabled = false;
            return;
        }
        PlayerPrefs.SetInt(id, 1);


        readyToStart = true;
        convoManager = gameObject.GetComponent<ConvoManager>();
        convoManager.enabled = false;
        closeness = 666f;
    }

    // Update is called once per frame
    void Update()
    {
        closeness = Vector3.Distance(transform.position, player.position);

        if (closeness <= range)
        {
            // player is close enough, start listening for input
            if (readyToStart)
            {
                readyToStart = false;
                onEvent.StartDialogue(diaFile);
            }
        }
        if (!readyToStart && !diaObj.activeInHierarchy)
        {
            convoManager.enabled = true;

            // only if cutscene
            if (sceneDoorOnFinish)
                sceneDoor.SetActive(true);
        }
            
    }

    public void ChangeConvo(string file)
    {
        diaFile = file;
        readyToStart = true;
        convoManager.enabled = false;
    }
}

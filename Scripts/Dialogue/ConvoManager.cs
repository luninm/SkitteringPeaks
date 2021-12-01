using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConvoManager : MonoBehaviour
{
    public string[] diaFiles;
    public GameObject diaObj;
    public OnEvent onEvent;
    public Transform player;
    public float range = 4f;


    // public for testing
    public float closeness = 666f;
    public bool readyToStart;
    public int index;
    public string id;

    // Dialogue progression is saved in player prefs
    // When the dialogue sequence is finished the last file will be repeated
    private void OnEnable()
    {
        id = gameObject.name + SceneManager.GetActiveScene().name;
        index = PlayerPrefs.GetInt(id, 0);
    }

    // These be called from out there
    // for lack of better words...
    public void ChangeConvo(string[] files)
    {
        diaFiles = files;
        PlayerPrefs.SetInt(id, 0);
    }

    public void ResetConvo()
    {
        PlayerPrefs.SetInt(id, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        closeness = 666f;
    }

    // Update is called once per frame
    void Update()
    {
        index = PlayerPrefs.GetInt(id, 0);

        closeness = Vector3.Distance(transform.position, player.position);

        if(closeness <= range)
        {
            // player is close enough, start listening for input
            if (GotInput() && readyToStart)
            {
                readyToStart = false;
                onEvent.StartDialogue(diaFiles[index]);

                if (index < diaFiles.Length - 1)
                {
                    PlayerPrefs.SetInt(id, index + 1);
                }
            }
        }
        if (!readyToStart && !diaObj.activeInHierarchy)
            readyToStart = true;
    }

    bool GotInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            return true;
        return false;
    }
}

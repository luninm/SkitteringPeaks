using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEvent : MonoBehaviour
{
    public GameObject DialogueObject;

    // Info format: "test0.json,Player,NPC"
    public void StartDialogue(string info)
    {
        GetComponent<DialogueManager>().SetUpDialogue(info);
        DialogueObject.SetActive(true);
    }
}

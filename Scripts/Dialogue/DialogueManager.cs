using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Ok listen. This used to be a neat lil dialogue system
// But then yeah..
// ...
// All the important stuff happens on the text object in [DialogueAnimator]
// It's an absolute mess.

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject dialogueText;
    public GameObject targetGroup;

    // This function is called before enabling dialogue
    public void SetUpDialogue(string info)
    {
        // Parse the info
        string[] dialogueInfo = info.Split(',');

        // Get the file
        SetDialogueFile(dialogueInfo[0]);

        // Clear TargetGroup
        ResetTargets();

        // Add each target to list
        List<GameObject> targetList = new List<GameObject>();
        for (var i = 1; i < dialogueInfo.Length; i++)
            targetList.Add(GameObject.Find(dialogueInfo[i]));

        // Set targets in Targetgroup
        SetTargets(targetList);

    }

    public void SetDialogueFile(string dialogueFile)
    {
        dialogueText.GetComponent<TextDialogueAnimator>().readFromThisFile = dialogueFile;
    }

    public void SetTargets(List<GameObject> targets)
    {
        // player focus (right now its the same as others)
        targetGroup.GetComponent<CinemachineTargetGroup>().AddMember(targets[0].transform, 1f, 0f);

        // others
        for (var i = 1; i < targets.Count; i++)
        {
            targetGroup.GetComponent<CinemachineTargetGroup>().AddMember(targets[i].transform, 1f, 0f);
        }
    }

    public void ResetTargets()
    {
        targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets = new CinemachineTargetGroup.Target[0];
    }

}

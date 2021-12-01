using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// For characters
// yeah
public class CharacterDialogueAnimator : MonoBehaviour
{
    public GameObject dialogueObject;
    public DialogueManager dialogueManager;
    public TextDialogueAnimator textDialogueAnimator;
    public CinemachineTargetGroup targetGroup;

    Emotion emotion;
    Emotion oldEmotion;

    AnimManager playerAnim;
    Transform npcTarget;
    Transform player;

    // locks (so values only update on change, not every frame)
    bool lockPlayerEmote = true;
    bool lockResetEmote;

    private void Start()
    {
        oldEmotion = Emotion.init;
        player = GameObject.Find("Player").transform;
        playerAnim = player.gameObject.GetComponent<AnimManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueObject.activeInHierarchy)
        {
            TalkingAnimations();
            lockResetEmote = false;
        }
        else
        {
            // Set npc to idle if done talking
            if (npcTarget != null && npcTarget.gameObject.GetComponent<AnimManager>().anim == Anim.talking)
            {
                SetNPCIdle();
                //Debug.Log("set npc to idle in dia animator");
            }

            // reset the emote
            if (!lockResetEmote)
            {
                lockResetEmote = true;
                SetEmote(player, -1);
                SetEmote(npcTarget, -1);
            }
        }
    }

    private void OnEnable()
    {
        emotion = Emotion.normal;
    }

    // Camera zooms in on player if they are in the choice prompt
    void TalkingAnimations()
    {
        if (targetGroup.m_Targets.Length > 0)
        {
            if (textDialogueAnimator.inChoicePrompt)
            {
                // you stupid line stupid bug
                // you've wasted hours of my time. god damn.
                //targetGroup.m_Targets[1].target.gameObject.GetComponent<AnimManager>().anim = Anim.idle;

                // Player is talking
                playerAnim.anim = Anim.talking;
                playerAnim.StartThinking();

                if (!lockPlayerEmote)
                {
                    lockPlayerEmote = true;
                    SetEmote(player, 0);
                }

            }
            else
            {
                // NPC is talking here
                npcTarget = targetGroup.m_Targets[1].target;
                npcTarget.gameObject.GetComponent<AnimManager>().anim = Anim.talking;

                // Play the animations for the npc
                HandleNPCEmotions();

                // Player is not talking
                playerAnim.anim = Anim.idle;
                SetEmote(player, -1);
                lockPlayerEmote = false;
            }
        }
    }

    void HandleNPCEmotions()
    {
        emotion = textDialogueAnimator.activeEmotion;

        if (emotion == oldEmotion)
            return;

        // Debug.Log(dialogue.activeEmotion.ToString());

        // Play the animations for each emotion
        switch (emotion)
        {
            case Emotion.normal:
                SetEmote(npcTarget, -1); // set to none
                npcTarget.gameObject.GetComponent<AnimManager>().StartNormal();
                break;

            case Emotion.worried:
                SetEmote(npcTarget, 2);
                npcTarget.gameObject.GetComponent<AnimManager>().StartWorried();
                break;

            case Emotion.yelling:
                SetEmote(npcTarget, 1);
                npcTarget.gameObject.GetComponent<AnimManager>().StartYelling();
                break;

            case Emotion.thinking:
                SetEmote(npcTarget, 0);
                npcTarget.gameObject.GetComponent<AnimManager>().StartThinking();
                break;

            case Emotion.question:
                SetEmote(npcTarget, 3);
                npcTarget.gameObject.GetComponent<AnimManager>().StartQuestion();
                break;

            default:
                Debug.Log("some new emotion was found woah");
                break;
        }

        oldEmotion = emotion;
    }

    void SetNPCIdle()
    {
        if (npcTarget != null)
            npcTarget.gameObject.GetComponent<AnimManager>().anim = Anim.idle;
    }

    void SetEmote(Transform obj, int index)
    {
        if (obj == null)
            return;

        Transform emote = obj.GetChild(1);

        for (var i = 0; i < 4; i++)
            emote.GetChild(i).gameObject.SetActive(false);

        if (index == -1)
            return;

        if (index < 4)
            emote.GetChild(index).gameObject.SetActive(true);
    }

}

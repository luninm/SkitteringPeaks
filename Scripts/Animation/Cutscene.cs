using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [Header("Set Up")]
    public float startDistance = 1.8f;
    public float duration = 7f;

    [Header("Subject Focus")]
    public GameObject npc;
    public bool hasCamera;
    public GameObject targetCamera;

    GameObject player;
    CinemachineBrain mainCameraBrain;
    float distance = 666f;
    bool entered;
    float timer;
    string id;

    public bool doorOnEnd = false;
    public GameObject nextDoor;

    public bool audioReset = false;
    public GameObject audioObj;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraBrain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
        player = GameObject.Find("Player");
        npc.SetActive(false);
        targetCamera.SetActive(false);
        id = SceneManager.GetActiveScene().name + npc.name + "Cut";
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if ((distance <= startDistance) && (!entered) && (PlayerPrefs.GetInt(id, 0) == 0))
        {
            entered = true;
            PlayerPrefs.SetInt(id, 1);

            // camera stuff
            mainCameraBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 3f);
            if (hasCamera) { targetCamera.SetActive(true); }

            npc.SetActive(true);
            player.GetComponent<PlayerController>().StopMovement = true;
        }

        if (entered)
        {
            timer += Time.deltaTime;
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.identity, .1f);
        }

        if (timer >= duration)
        {
            // camera stuff
            mainCameraBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);
            if (hasCamera) { targetCamera.SetActive(false); }

            npc.SetActive(false);
            player.GetComponent<PlayerController>().StopMovement = false;

            //go to next scene if selected
            if (doorOnEnd)
                nextDoor.SetActive(true);

            // reset audio
            if (audioReset)
                audioObj.SetActive(true);

            enabled = false;
        }
    }
}

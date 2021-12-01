using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    public float minDistance = 1.8f;
    public bool startLit = false;
    public bool lit = false;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        DisableAllChildren();

        if (startLit)
        {
            EnableAllChildren();
            lit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NextKeyPressed())
        {
            lit = !lit;

            if (!lit)
                DisableAllChildren();
            else
                EnableAllChildren();
        }
    }

    void DisableAllChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }
    void EnableAllChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);
    }

    bool NextKeyPressed()
    {
        // distance between player and door
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            && (distance <= minDistance))
        {
            return true;
        }
        return false;
    }
}

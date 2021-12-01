using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableAfter : MonoBehaviour
{
    public float duration = 1f;
    float timer = 0f;
    bool done = false;

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                done = true;
            }
        }
    }


}

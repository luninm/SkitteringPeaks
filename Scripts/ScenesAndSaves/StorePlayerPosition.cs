using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlayerPosition : MonoBehaviour
{
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("playerXpos", transform.position.x);
        PlayerPrefs.SetFloat("playerYpos", transform.position.y);
        PlayerPrefs.SetFloat("playerZpos", transform.position.z);
    }

    private void OnEnable()
    {
        var x = PlayerPrefs.GetFloat("playerXpos", 0f);
        var y = PlayerPrefs.GetFloat("playerYpos", 1.8f);
        var z = PlayerPrefs.GetFloat("playerZpos", 0f);

        transform.position = new Vector3(x, y, z);
    }
}

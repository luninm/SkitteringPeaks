using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string sceneName;
    public bool clearPrefs = false;

    public float fadeDuration = 1f;
    public GameObject transExit;

    bool inFade = false;
    bool doneFade = false;
    float timer = 0f;

    public void LoadSceneNow()
    {
        if (clearPrefs)
            PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(sceneName);

    }

    public void LoadSceneWithFade()
    {
        inFade = true;
        transExit.SetActive(true);
    }

    private void Update()
    {
        if (inFade)
        {
            timer += Time.deltaTime;

            if((timer >= fadeDuration) && (!doneFade))
            {
                doneFade = true;
                LoadSceneNow();
                enabled = false;
            }
        }
    }
}

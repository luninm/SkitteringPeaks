using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public string sceneName;

    public GameObject player;
    public GameObject exitTransition;
    public float minDistance = .5f;
    public float transitionDuration = .8f;
    public bool requireInput;
    Coroutine transition;

    float timeSinceSceneLoaded;
    //float timeSinceLeftDoor;



    // Start is called before the first frame update
    void Start()
    {
        transition = null;
        exitTransition.SetActive(false);
    }

    private void OnEnable()
    {
        timeSinceSceneLoaded = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // this is for the spammers
        timeSinceSceneLoaded += Time.deltaTime;
        if (timeSinceSceneLoaded < transitionDuration)
            return;

        // distance between player and door
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // can use the door if close enough
        if (distance <= minDistance)
        {
            //timeSinceLeftDoor = 0f;
            if (transition == null) 
                transition = StartCoroutine(BeginTransition());
        }
    }

    IEnumerator BeginTransition()
    {
        // wait for input if required
        if (requireInput)
            yield return new WaitUntil(NextKeyPressed);

        // endable the animation
        exitTransition.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName); ;

        // Don't let the scene activate until animation plays
        asyncOperation.allowSceneActivation = false;

        // wait for animation
        yield return new WaitForSeconds(transitionDuration);

        // change scene
        asyncOperation.allowSceneActivation = true;

        transition = null;
        yield break;
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

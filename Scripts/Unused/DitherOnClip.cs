using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitherOnClip : MonoBehaviour
{
    public float currentAmount = 1f;
    public bool startDither;
    public bool endDither;
    public bool dithering;
    Coroutine coroutine;
    public Transform player, cam;
    public bool cliped;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        coroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Linecast(cam.position, player.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.name == gameObject.name)
            {
                if (cliped == false)
                {
                    cliped = true;
                    startDither = true;
                }
            }
            else
            {
                if (cliped)
                {
                    endDither = true;
                }
                cliped = false;
            }
        }
        else
        {
            if (cliped)
            {
                endDither = true;
            }
            cliped = false;
        }


        if (coroutine == null)
        {
            if (startDither)
            {
                startDither = false;
                coroutine = StartCoroutine(StartDither());
            }
            else if (endDither)
            {
                endDither = false;
                coroutine = StartCoroutine(EndDither());
            }
        }

        gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Opacity", currentAmount);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startDither = true;
            endDither = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endDither = true;
            startDither = false;
        }
    }*/


    IEnumerator StartDither()
    {
        dithering = true;
        currentAmount = 1f;
        for (var i = 0; i < 40; i++)
        {
            currentAmount -= .025f;
            yield return new WaitForSeconds(.01f);
        }
        currentAmount = 0f;
        coroutine = null;
        dithering = false;
        yield break;
    }

    IEnumerator EndDither()
    {
        dithering = true;
        currentAmount = 0f;
        for (var i = 0; i < 40; i++)
        {
            currentAmount += .025f;
            yield return new WaitForSeconds(.01f);
        }
        currentAmount = 1f;
        coroutine = null;
        dithering = false;
        yield break;
    }

    private void OnEnable()
    {
        currentAmount = 1f;
    }
    private void OnDisable()
    {
        currentAmount = 1f;
    }
}

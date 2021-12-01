using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    public AudioSource audioSource;

    Coroutine coroutine;
    public bool startAuto = true;

    private void OnEnable()
    {
        audioSource.Play();
        audioSource.volume = 0f;
        coroutine = null;
        StopAllCoroutines();

        if (startAuto)
            StartFadeIn();

    }

    public void StartFadeIn()
    {
        if (coroutine == null)
            StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        for (var i = 20; i > 0; i--)
        {
            yield return new WaitForSeconds(.05f);

            if (audioSource.volume > 1f / 20f)
            {
                audioSource.volume += 1f / 20f;
            }
            else
            {
                audioSource.volume = 1f;
                break;
            }
        }

        coroutine = null;
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;

    Coroutine coroutine;
    public bool startAuto = true;

    private void OnEnable()
    {
        coroutine = null;
        StopAllCoroutines();

        if (startAuto)
            StartFadeOut();
        
    }

    public void StartFadeOut()
    {
        if (coroutine == null)
            StartCoroutine(fadeOut());
    }

    IEnumerator fadeOut()
    {
        for (var i = 20; i > 0; i--)
        {
            yield return new WaitForSeconds(.025f);

            if (audioSource.volume > 1f / 20f)
            {
                audioSource.volume -= 1f / 20f;
            }
            else
            {
                audioSource.volume = 0f;
                break;
            }
        }

        coroutine = null;
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorLerp : MonoBehaviour
{
    public float frequency;
    public Color toColor;
    public Color fromColor;
    public bool useOriginal;


    // Start is called before the first frame update
    void Start()
    {
        if (useOriginal)
            fromColor = GetComponent<Light>().color;
    }

    // Update is called once per frame
    void Update()
    {
        var from = fromColor;
        var to = toColor;

        float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * frequency));
        GetComponent<Light>().color = Color.Lerp(from, to, lerp);
    }

    public void IncreaseIntensity()
    {
        GetComponent<Light>().intensity += .5f;
    }
}

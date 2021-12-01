using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    public Vector3 m_from, m_to;
    public float m_frequency = 1f;
    public bool resetRotation = true;

    private Quaternion init;

    private void OnEnable()
    {
        init = transform.localRotation;
    }

    private void OnDisable()
    {
        if (resetRotation)
            transform.localRotation = init;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion from = Quaternion.Euler(m_from);
        Quaternion to = Quaternion.Euler(m_to);

        float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * m_frequency));
        transform.localRotation = Quaternion.Lerp(from, to, lerp);
    }

    public void SetRotationValues(Vector3 from_, Vector3 to_, float freq_)
    {
        m_from = from_;
        m_to = to_;
        m_frequency = freq_;
    }

    public void SetRotationValues(RotValues rotv)
    {
        m_from = rotv.from;
        m_to = rotv.to;
        m_frequency = rotv.freq;
    }

}

using UnityEngine;

public class ShaderInteractor : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        Shader.SetGlobalVector("_PositionMoving", transform.position);
    }
}

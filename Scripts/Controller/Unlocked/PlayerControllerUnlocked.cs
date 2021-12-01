using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUnlocked : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 1f;
    public Vector3 deltaMove;
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        deltaMove = speed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(deltaMove);
    }
}

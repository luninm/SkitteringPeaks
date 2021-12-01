using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This does the control thing. The thing to the player. Yep.
public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    Vector3 rotation;
    Vector3 deltaMovement;
    public float speed = 4f;
    public float turnSpeed = 20f;
    public GameObject Dialogue;
    public GameObject ExitTransition;
    public bool StopMovement;

    float horizontal, vertical;
    AnimManager animManager;

    AudioSource audio;

    private void Start()
    {
        animManager = GetComponent<AnimManager>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.volume = 0f;
    }

    private void OnEnable()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleMovement();
    }

    bool MovementAllowed()
    {
        if (Dialogue.activeInHierarchy || ExitTransition.activeInHierarchy || StopMovement)
            return false;
        return true;
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (MovementAllowed() == false)
        {
            horizontal = 0f;
            vertical = 0f;
        }

    }

    void HandleMovement()
    {
        // Set direction vectors
        movement = new Vector3(-horizontal, 0, vertical);
        rotation = new Vector3(horizontal, 0, vertical);

        // Face movement direction
        if (IsMoving())
        {
            audio.volume = 1f;
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), Time.deltaTime * turnSpeed);
        }
        else
        {
            audio.volume = 0f;
            transform.Translate(Vector3.zero);
            return;
        }


        // Clamp magnitude of movement
        movement = Vector3.ClampMagnitude(movement, 1f);

        // Move
        deltaMovement = speed * Time.deltaTime * transform.TransformDirection(movement);
        transform.Translate(deltaMovement);
    }

    public bool IsMoving()
    {
        if (movement != Vector3.zero)
        {
            animManager.anim = Anim.runnning;
            return true;
        }

        if(animManager.anim != Anim.talking)
            animManager.anim = Anim.idle;

        return false;
    }
}


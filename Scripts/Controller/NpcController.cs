using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// thank lord and savior brackeys amen
public class NpcController : MonoBehaviour
{
    NavMeshAgent agent;
    Camera cam;
    AnimManager animManager;

    public List<Vector3> waypoints;
    public bool ReadyToPathfind;
    public bool wander;
    public float restingDuration = 1f;

    [Header("Testing")]
    public int currentIndex;
    public bool isPathfinding;

    float restTimer;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        animManager = GetComponent<AnimManager>();
        agent.SetDestination(transform.position); // start position is first waypoint
    }

    // Update is called once per frame
    void Update()
    {
        if (!ReadyToPathfind)
            return;

        StopPathfindingIfTalking();
        UpdateWaypoints();
        HandleAnimations();

    }

    void UpdateWaypoints()
    {
        // Go through the waypoints linearly or randomly
        if (wander) // randomly chose one, pause for a bit, keep chosing
        {
            if (AtWaypoint())
            {
                restTimer += Time.deltaTime;

                if (restTimer >= restingDuration)
                {
                    restTimer = 0f;
                    agent.SetDestination(waypoints[currentIndex]);
                    currentIndex = Random.Range(0, waypoints.Count);
                }
            }
        }
        else
        {
            if (AtWaypoint())
            {
                restTimer += Time.deltaTime;

                if ((restTimer >= restingDuration) && (currentIndex < waypoints.Count))
                {
                    restTimer = 0f;
                    agent.SetDestination(waypoints[currentIndex]);
                    currentIndex++;
                }
            }
        }
    }

    bool AtWaypoint()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            return true;
        return false;
    }

    void StopPathfindingIfTalking()
    {
        // Don't walk away when talking cause ya know.. rude
        if (animManager.anim == Anim.talking)
        {
            agent.isStopped = true;
            return;
        }
        if (animManager.anim != Anim.talking)
        {
            agent.isStopped = false;
        }
    }


    void HandleAnimations()
    {
        // Handle animations
        if ((agent.remainingDistance > agent.stoppingDistance) && (animManager.anim != Anim.talking))
        {
            animManager.anim = Anim.runnning;
        }
        else
        {
            if (animManager.anim != Anim.talking)
            {
                animManager.anim = Anim.idle;
            }
        }
    }
}

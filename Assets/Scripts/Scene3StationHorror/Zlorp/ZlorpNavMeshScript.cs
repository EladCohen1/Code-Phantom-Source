using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZlorpNavMeshScript : MonoBehaviour
{
    public float rotationSpeed = 720;
    [SerializeField] private Transform[] patrol;
    [SerializeField] private int secondsToWait;
    NavMeshAgent navMeshAgent;
    Animator anim;
    private int CurrentWayPointIndex = 0;
    private bool isWaiting = false;
    public bool isPatroling = false;
    private bool isFirstPatrol = true;
    void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        HandleRotation();
        if (isPatroling)
        {
            HandleDestination();
        }
    }

    void HandleAnimation()
    {
        if (transform.position != navMeshAgent.destination)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    void HandleRotation()
    {
        Vector3 movementDirection = (navMeshAgent.destination - transform.position).normalized;
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleDestination()
    {
        if (transform.position == navMeshAgent.destination && !isWaiting)
        {
            CurrentWayPointIndex = CurrentWayPointIndex + 1;
            if (CurrentWayPointIndex >= patrol.Length)
            {
                CurrentWayPointIndex = 0;
            }
            StartCoroutine(SetNewDestination());
        }
    }

    IEnumerator SetNewDestination()
    {
        isWaiting = true;
        if (!isFirstPatrol)
        {
            yield return new WaitForSeconds(secondsToWait);
        }
        isFirstPatrol = false;
        navMeshAgent.destination = patrol[CurrentWayPointIndex].position;
        isWaiting = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZlorpFOVScript : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool isLooking = true;
    public float delay = 0.2f;

    private bool isCoroutineRunning;

    [SerializeField] public AudioSource ZlorpScreamAudio;
    [SerializeField] Canvas deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning && isLooking)
        {
            StartCoroutine(FOVRoutine());
        }
    }

    private IEnumerator FOVRoutine()
    {
        while (isLooking)
        {
            yield return new WaitForSeconds(delay);
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (colliders.Length > 0)
        {
            Transform target = colliders[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    HandlePlayerFound();
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else if (player.GetComponent<PlayerScript>().characterAnimator.GetBool("IsMoving") || player.GetComponent<PlayerScript>().characterAnimator.GetBool("IsRunning"))
            {
                HandlePlayerFound();
            }
            else
                canSeePlayer = false;
        }
        else
            canSeePlayer = false;
    }

    void HandlePlayerFound()
    {
        canSeePlayer = true;
        ZlorpScreamAudio.enabled = true;

        Vector3 playerLookAtDirection = player.transform.forward.normalized;
        transform.position = player.transform.position + playerLookAtDirection * 0.7f;

        gameObject.GetComponent<ZlorpNavMeshScript>().isPatroling = false;
        gameObject.GetComponent<NavMeshAgent>().destination = transform.position;

        Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = toRotation;

        player.GetComponent<PlayerScript>().isControlled = false;
        gameObject.GetComponent<Animator>().SetBool("IsScreaming", true);
        player.GetComponent<Animator>().SetBool("IsFalling", true);
        StartCoroutine(startDeathScreen());
    }

    IEnumerator startDeathScreen()
    {
        isLooking = false;
        yield return new WaitForSeconds(2);
        deathScreen.enabled = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

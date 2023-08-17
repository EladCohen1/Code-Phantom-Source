using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    // speed and grace period
    private float speed;
    public float sprintCoefficient;
    public float sneakCoefficient;
    public float originalSpeed = 3;
    public float rotationSpeed = 720;

    // camera movement
    [SerializeField]
    private Transform cameraTransform;
    private CharacterController characterController;
    private float ySpeed = 0;

    // animator
    public Animator characterAnimator;

    // pause menu
    public Canvas pauseMenu;
    public CinemachineFreeLook cinemachineCam;

    // no control
    public bool isControlled = true;

    // stamina
    [SerializeField] private StaminaScript staminaScript;

    // first person view

    [SerializeField] private float mouseSens;
    private Vector2 look;
    [SerializeField] private GameObject firstPersonCam;
    private bool isFirstPerson;


    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirstPerson)
        {
            look.x = -transform.eulerAngles.y;
        }
        if (isControlled)
        {
            HandleMovement();
            HandlePause();
        }
        else
        {
            if (isFirstPerson)
            {
                firstPersonCam.transform.localPosition = new Vector3(0, 0.15f, 0.032f);
            }
            staminaScript.IncreaseStamina();
            characterAnimator.SetBool("IsRunning", false);
            characterAnimator.SetBool("IsMoving", false);
            characterAnimator.SetBool("IsSneaking", false);
        }
        HandleCamChange();
    }
    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            cinemachineCam.enabled = false;
            isControlled = false;
        }
    }
    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection;
        float magnitude;
        if (!isFirstPerson)
        {
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        }
        else
        {
            movementDirection = new Vector3();
            movementDirection += transform.forward * verticalInput;
            movementDirection += transform.right * horizontalInput;
            magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            UpdateLookFirstPersonCam();
        }

        movementDirection.Normalize();
        ySpeed += (Physics.gravity.y) * Time.deltaTime;
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;
        }
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // rotating the character to face where he is moving
        if (movementDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !staminaScript.isStaminaRecovering)
            {
                if (isFirstPerson)
                {
                    firstPersonCam.transform.localPosition = new Vector3(0, 0.15f, 0.032f);
                }
                characterAnimator.SetBool("IsRunning", true);
                characterAnimator.SetBool("IsMoving", false);
                characterAnimator.SetBool("IsSneaking", false);
                speed = originalSpeed * sprintCoefficient;
                staminaScript.DecreaseStamina();
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                if (isFirstPerson)
                {
                    firstPersonCam.transform.localPosition = new Vector3(0, 0.1f, 0.032f);
                }
                staminaScript.IncreaseStamina();
                characterAnimator.SetBool("IsSneaking", true);
                characterAnimator.SetBool("IsMoving", false);
                characterAnimator.SetBool("IsRunning", false);
                speed = originalSpeed * sneakCoefficient;
            }
            else
            {
                if (isFirstPerson)
                {
                    firstPersonCam.transform.localPosition = new Vector3(0, 0.15f, 0.032f);
                }
                staminaScript.IncreaseStamina();
                characterAnimator.SetBool("IsRunning", false);
                characterAnimator.SetBool("IsMoving", true);
                characterAnimator.SetBool("IsSneaking", false);
                speed = originalSpeed;
            }
            if (!isFirstPerson)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftControl) && isFirstPerson)
            {
                firstPersonCam.transform.localPosition = new Vector3(0, 0.15f, 0.032f);
            }
            else if (isFirstPerson)
            {
                firstPersonCam.transform.localPosition = new Vector3(0, 0.1f, 0.032f);
            }
            staminaScript.IncreaseStamina();
            characterAnimator.SetBool("IsMoving", false);
            characterAnimator.SetBool("IsRunning", false);
            characterAnimator.SetBool("IsSneaking", false);
        }
    }

    void UpdateLookFirstPersonCam()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSens;
        look.y += Input.GetAxis("Mouse Y") * mouseSens;
        look.y = Mathf.Clamp(look.y, -90, 90);
        firstPersonCam.transform.localRotation = Quaternion.Euler(look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, -look.x, 0);
    }

    private void HandleCamChange()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isFirstPerson)
            {
                firstPersonCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
                cinemachineCam.enabled = false;
            }
            else
            {
                cinemachineCam.enabled = true;
                firstPersonCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
            }
            isFirstPerson = !isFirstPerson;
        }
    }
    public void EnableMainCam()
    {
        if (isFirstPerson)
        {
            firstPersonCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
        }
        else
        {
            cinemachineCam.enabled = true;
        }
    }

    public void DisableMainCam()
    {
        if (isFirstPerson)
        {
            firstPersonCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
        else
        {
            cinemachineCam.enabled = false;
        }
    }
}

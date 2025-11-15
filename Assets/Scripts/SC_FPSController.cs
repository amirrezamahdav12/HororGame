using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public AudioClip WalkingSound;

    // FOV Variables
    public float normalFOV = 60f;
    public float zoomedFOV = 30f;
    private float currentFOV;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    // Audio Source for Walking Sound
    private AudioSource audioSource;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial FOV
        currentFOV = normalFOV;
        playerCamera.fieldOfView = currentFOV;

        // Setup Audio Source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = WalkingSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Handle Zoom with Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            currentFOV = zoomedFOV;
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentFOV = normalFOV;
        }

        // Update the camera's field of view
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, currentFOV, Time.deltaTime * 10f);

        // Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Walking Sound Logic
        bool isMoving = characterController.isGrounded && (curSpeedX != 0 || curSpeedY != 0);

        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Adjust pitch based on running
        audioSource.pitch = isRunning ? 1.3f : 1.0f;
    }
}

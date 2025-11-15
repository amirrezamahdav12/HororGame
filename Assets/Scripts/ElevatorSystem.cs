using System.Collections;
using UnityEngine;

public class ElevatorSystem : MonoBehaviour
{
    public GameObject elevator;
    public AudioSource elevatorAudio;

    public Transform topFloorPosition;
    public Transform bottomFloorPosition;
    public float moveSpeed = 2f;

    public KeyCode interactKey = KeyCode.E;
    public float interactionRange = 3f;

    private bool isMoving = false;
    public Camera playerCamera;  // Assign your Player/FirstPerson camera here

    public FuseBoxActivator fuseBoxActivator;

    void Start()
    {
        if (fuseBoxActivator == null)
            fuseBoxActivator = GetComponent<FuseBoxActivator>();

        elevator.SetActive(false);

        if (playerCamera == null)
            playerCamera = Camera.main;
        
        
    }

    void Update()
    {
        if (!elevator.activeSelf || isMoving) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (Input.GetKeyDown(interactKey))
            {
                // You can check by tag or specific object name
                if (hit.collider.CompareTag("ElevatorButtonUp"))
                {
                    StartCoroutine(MoveElevator(topFloorPosition.position));
                }
                else if (hit.collider.CompareTag("ElevatorButtonDown"))
                {
                    StartCoroutine(MoveElevator(bottomFloorPosition.position));
                }
            }
        }
    }

    public void ActivateElevatorSystem()
    {
        if (fuseBoxActivator != null)
        {
            Debug.Log("Ativating the system");
            elevator.SetActive(true);
        }
    }

    private IEnumerator MoveElevator(Vector3 targetPosition)
    {
        isMoving = true;
        if (elevatorAudio != null) elevatorAudio.Play();

        while (Vector3.Distance(elevator.transform.position, targetPosition) > 0.01f)
        {
            elevator.transform.position = Vector3.MoveTowards(
                elevator.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        elevator.transform.position = targetPosition;
        isMoving = false;
    }
}

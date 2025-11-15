using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FuseBoxActivator : MonoBehaviour
{
    // Variables
    public GameObject FuseAinactive, FuseBInactive;
    public AudioSource FuseSound;
    public ParticleSystem Sparke;

    public Text pickUpFuseText;
    public Text needFuseText;
    public Text elevatorRestoredText;

    public bool fuseACollected = false;
    public bool fuseBCollected = false;
    private bool fusesPlaced = false;
    private bool playerInTrigger = false;

    public ElevatorSystem elevatorSystem;

    void Start()
    {
        FuseAinactive.SetActive(false);
        FuseBInactive.SetActive(false);

        if (pickUpFuseText != null) pickUpFuseText.gameObject.SetActive(false);
        if (needFuseText != null) needFuseText.gameObject.SetActive(false);
        if (elevatorRestoredText != null) elevatorRestoredText.gameObject.SetActive(false);
    }

    // Trigger Enter to enable UI prompts
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (pickUpFuseText != null)
                pickUpFuseText.gameObject.SetActive(true); // Show "Press E to place fuses" text
        }
    }

    // Trigger Exit to hide UI prompts
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            if (pickUpFuseText != null)
                pickUpFuseText.gameObject.SetActive(false); // Hide text when out of range
        }
    }

    void Update()
    {
        // If the player is inside the trigger zone and presses "E"
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !fusesPlaced)
        {
            if (fuseACollected && fuseBCollected)
            {
                // Place fuses inside the fusebox
                FuseAinactive.SetActive(true);
                FuseBInactive.SetActive(true);
                
                // Stop fuse sound and particles
                if (Sparke != null) Sparke.Stop();
                if (FuseSound != null) FuseSound.Stop();

                fusesPlaced = true;
                if (pickUpFuseText != null) pickUpFuseText.gameObject.SetActive(false);

                // Activate the elevator system
                if (elevatorSystem != null)
                    elevatorSystem.ActivateElevatorSystem();

                StartCoroutine(ShowRestoredText());
            }
            else
            {
                // Show message for missing fuses
                if (needFuseText != null)
                {
                    string message = "Missing ";
                    if (!fuseACollected && !fuseBCollected) message += "Fuses A & B";
                    else if (!fuseACollected) message += "Fuse A";
                    else message += "Fuse B";

                    needFuseText.text = message;
                    needFuseText.gameObject.SetActive(true);
                    StartCoroutine(HideTextAfterDelay(needFuseText, 3f));
                }
            }
        }
    }

    // Show the elevator restored text for a short period
    private IEnumerator ShowRestoredText()
    {
        elevatorRestoredText.text = "Elevator Power Restored";
        elevatorRestoredText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        elevatorRestoredText.gameObject.SetActive(false);
    }

    // Hide the "Need Fuse" message after a short delay
    private IEnumerator HideTextAfterDelay(Text textObj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (textObj != null)
            textObj.gameObject.SetActive(false);
    }
}

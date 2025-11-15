using UnityEngine;

public class FusePickup : MonoBehaviour
{
    public string fuseID; // Set as "A" or "B" in Inspector
    public FuseBoxActivator fuseBoxActivator;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (fuseID == "A")
                fuseBoxActivator.fuseACollected = true;
            else if (fuseID == "B")
                fuseBoxActivator.fuseBCollected = true;

            //fuseBoxActivator.ShowPickupText(false);
            Destroy(gameObject);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
           // fuseBoxActivator.ShowPickupText(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //fuseBoxActivator.ShowPickupText(false);
        }
    }
}
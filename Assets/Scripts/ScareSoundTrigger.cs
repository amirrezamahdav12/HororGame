using UnityEngine;

public class ScareSoundTrigger : MonoBehaviour
{
    public AudioClip soundClip;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = soundClip;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            audioSource.Play();
        }
    }

    private void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopSound();
    }
}
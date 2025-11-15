using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Light Settings")]
    public Light lightSource; // نور مورد نظر
    public float minFlickerTime = 0.05f;
    public float maxFlickerTime = 0.2f;
    public bool startFlickeringOnStart = true;

    [Header("Flicker Pattern")]
    public bool flickerRandomly = true;
    public float flickerDuration = 3f; // چند ثانیه برق چشمک بزنه

    private bool isFlickering = false;

    void Start()
    {
        if (startFlickeringOnStart)
            StartCoroutine(FlickerSequence());
    }

    public void StartFlicker(float duration = -1f)
    {
        if (duration > 0f)
            flickerDuration = duration;

        StartCoroutine(FlickerSequence());
    }

    System.Collections.IEnumerator FlickerSequence()
    {
        isFlickering = true;
        float timer = 0f;

        while (timer < flickerDuration)
        {
            lightSource.enabled = false;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            lightSource.enabled = true;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            if (!flickerRandomly)
                timer += maxFlickerTime * 2;
            else
                timer += Random.Range(minFlickerTime, maxFlickerTime) * 2;
        }

        isFlickering = false;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class EyeBlink : MonoBehaviour
{
    public RectTransform upperLid; // نیم‌دایره بالایی
    public RectTransform lowerLid; // نیم‌دایره پایینی
    public float blinkSpeed = 0.1f;
    public float blinkInterval = 5f;

    private Vector2 upperStartPos;
    private Vector2 lowerStartPos;

    void Start()
    {
        upperStartPos = upperLid.anchoredPosition;
        lowerStartPos = lowerLid.anchoredPosition;
        InvokeRepeating("Blink", blinkInterval, blinkInterval);
    }

    void Blink()
    {
        StartCoroutine(BlinkEffect());
    }

    System.Collections.IEnumerator BlinkEffect()
    {
        float elapsed = 0f;
        while (elapsed < blinkSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / blinkSpeed;
            upperLid.anchoredPosition = Vector2.Lerp(upperStartPos, new Vector2(upperStartPos.x, 0), t);
            lowerLid.anchoredPosition = Vector2.Lerp(lowerStartPos, new Vector2(lowerStartPos.x, 0), t);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.05f); // یه مقدار خیلی کوتاه بسته بمونه

        elapsed = 0f;
        while (elapsed < blinkSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / blinkSpeed;
            upperLid.anchoredPosition = Vector2.Lerp(new Vector2(upperStartPos.x, 0), upperStartPos, t);
            lowerLid.anchoredPosition = Vector2.Lerp(new Vector2(lowerStartPos.x, 0), lowerStartPos, t);
            yield return null;
        }
    }
}
using UnityEngine;

public class HeadbobController : MonoBehaviour
{
    public float bobFrequency = 10f; // Speed of the bobbing
    public float bobAmount = 0.1f;   // Strength of the vertical bob
    public float horizontalAmount = 0.05f; // Side-to-side bob
    public float returnSpeed = 5f;   // How fast the camera resets

    private float timer = 0f;
    private CharacterController controller;
    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponentInParent<CharacterController>();
        startPosition = transform.localPosition;
    }

    void Update()
    {
        if (controller != null && controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            timer += Time.deltaTime * bobFrequency;
            float verticalBob = Mathf.Sin(timer) * bobAmount;
            float horizontalBob = Mathf.Cos(timer * 2) * horizontalAmount; // Side-to-side effect

            transform.localPosition = startPosition + new Vector3(horizontalBob, verticalBob, 0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * returnSpeed);
        }
    }
}

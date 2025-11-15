using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseUserInterface : MonoBehaviour
{
    // Variables
    public Text youNeedFuse;  // متن "نیاز به فیوز"
    public Text installFuse;  // متن "نصب فیوز"
    public Text pickUpFuse;   // متن "برداشتن فیوز"

    public float rayDistance = 3f; // فاصله Raycast
    public LayerMask interactableLayer; // لایه اشیای قابل تعامل

    private Camera playerCamera;
    private bool isLookingAtFuse = false;
    private bool isLookingAtFuseBox = false;

    // Start method
    private void Start()
    {
        playerCamera = Camera.main;

        // پنهان کردن تمام متن‌ها در ابتدا
        youNeedFuse.gameObject.SetActive(false);
        installFuse.gameObject.SetActive(false);
        pickUpFuse.gameObject.SetActive(false);
    }

    // Update method
    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // ارسال Ray از مرکز صفحه

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Fuse"))
            {
                ShowPickUpText();
            }
            else if (hit.collider.CompareTag("FuseBox"))
            {
                ShowInstallText();
            }
            else
            {
                HideUI();
            }
        }
        else
        {
            HideUI();
        }
    }

    // متد برای نمایش متن برداشتن فیوز
    private void ShowPickUpText()
    {
        isLookingAtFuse = true;
        isLookingAtFuseBox = false;

        pickUpFuse.gameObject.SetActive(true);  // نمایش "Press E to pick up fuse"
        installFuse.gameObject.SetActive(false);
        youNeedFuse.gameObject.SetActive(false);
    }

    // متد برای نمایش متن نصب فیوز
    private void ShowInstallText()
    {
        isLookingAtFuse = false;
        isLookingAtFuseBox = true;

        installFuse.gameObject.SetActive(true); // نمایش "Press E to install fuse"
        pickUpFuse.gameObject.SetActive(false);
        youNeedFuse.gameObject.SetActive(false);
    }

    // متد برای پنهان کردن تمام متن‌ها
    private void HideUI()
    {
        isLookingAtFuse = false;
        isLookingAtFuseBox = false;

        pickUpFuse.gameObject.SetActive(false);
        installFuse.gameObject.SetActive(false);
        youNeedFuse.gameObject.SetActive(false);
    }
}

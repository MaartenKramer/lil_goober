using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private InputActionReference interactInput;

    [SerializeField] private bool canScan;
    [SerializeField] private string failedScanText;

    [SerializeField] private float scanCooldown = 1f;

    void Start()
    {
        canScan = false;
    }

    
    void Update()
    {
        if (!canScan && interactInput.action.IsPressed())
        {
            StartCoroutine(Cooldown());
            playerUI.SetTopText(failedScanText, true, true, false);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractObject")
        {
            canScan = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "InteractObject")
        {
            if (canScan && interactInput.action.IsPressed())
            {
                StartCoroutine(Cooldown());
                canScan = false;
                other.GetComponent<Interactable>().IsScanned();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractObject")
        {
            canScan = false;
        }
    }

    private IEnumerator Cooldown()
    {
        interactInput.action.Disable();
        
        yield return new WaitForSeconds(scanCooldown);

        interactInput.action.Enable();
    }
}

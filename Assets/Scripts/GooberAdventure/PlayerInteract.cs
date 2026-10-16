using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputActionReference interactInput;

    [SerializeField] private bool canScan;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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
}

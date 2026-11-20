using UnityEngine;

public class ExitWarning : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;

    [TextArea] [SerializeField] private string warningText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerUI.SetTopText(warningText, true, false, false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerUI.RemoveTopText();
    }
}

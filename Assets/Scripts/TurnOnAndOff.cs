using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOnAndOff : MonoBehaviour
{
    [SerializeField] private bool use;

    [SerializeField] private InputActionReference generatorOnInput;
    [SerializeField] private InputActionReference gooberOnInput;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject noPowerGoober;
    [SerializeField] private GameObject noPowerRadar;
    [SerializeField] private GameObject noGoober;

    void Start()
    {
        if (use)
            TurnEverythingOff();
    }

    void Update()
    {
        if (generatorOnInput.action.IsPressed() && use)
        {
            GeneratorOn();
        }

        if (gooberOnInput.action.IsPressed() && use)
        {
            GooberOn();
        }
    }

    private void TurnEverythingOff()
    {
        playerMovement.enabled = false;
        noPowerGoober.SetActive(true);
        noPowerRadar.SetActive(true);
        noGoober.SetActive(true);
    }

    private void GeneratorOn()
    {
        noPowerGoober.SetActive(false);
        noPowerRadar.SetActive(false);
    }

    private void GooberOn()
    {
        playerMovement.enabled = true;
        noGoober.SetActive(false);
    }
}

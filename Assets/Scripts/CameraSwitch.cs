using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera gooberCam;
    [SerializeField] private Camera radarCam;
    [SerializeField] private GameObject radarButtons;
    [SerializeField] private GameObject screenshotCanvas;

    [SerializeField] private InputActionReference switchInput;

    public enum State { Goober, Radar }
    public State currentState;

    [SerializeField] private bool canSwitch;
    [SerializeField] private float waitTime = 1f;

    void Start()
    {
        currentState = State.Goober;
        radarButtons.SetActive(false);
        screenshotCanvas.SetActive(true);
        canSwitch = false;
        StartCoroutine(SwitchCooldown());
    }

    void Update()
    {
        if (switchInput.action.IsPressed() && canSwitch)
        {
            canSwitch = false;
            StartCoroutine(SwitchCooldown());

            if (currentState == State.Goober)
            {
                currentState = State.Radar;
            }
            else if (currentState == State.Radar)
            {
                currentState = State.Goober;
            }
        }

        switch (currentState)
        {
            case State.Goober:
                GooberState();
                break;
            case State.Radar:
                RadarState();
                break;
        }
    }

    private void GooberState()
    {
        gooberCam.enabled = true;
        radarCam.enabled = false;
        radarButtons.SetActive(false);
        screenshotCanvas.SetActive(true);
    }

    private void RadarState()
    {
        radarCam.enabled = true;
        gooberCam.enabled = false;
        radarButtons.SetActive(true);
        screenshotCanvas.SetActive(false);
    }

    private IEnumerator SwitchCooldown()
    {
        yield return new WaitForSeconds(waitTime);
        canSwitch = true;
    }
}

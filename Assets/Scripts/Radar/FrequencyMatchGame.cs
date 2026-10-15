using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FrequencyMatchGame : MonoBehaviour
{
    [SerializeField] private FrequencyLine radarLine;
    [SerializeField] private FrequencyLine antennaLine;

    [SerializeField] private Knob amplitudeKnob;
    [SerializeField] private Knob wavelengthKnob;

    [SerializeField] private GameObject playerIcon;
    [SerializeField] private GameObject interactableIcon1;
    [SerializeField] private GameObject interactableIcon2;
    [SerializeField] private GameObject interactableIcon3;
    [SerializeField] private GameObject interactableIcon4;

    [SerializeField] private EventTrigger amplitudeTrigger;
    [SerializeField] private EventTrigger wavelengthTrigger;

    void Start()
    {
        playerIcon.SetActive(false);
        interactableIcon1.SetActive(false);
        interactableIcon2.SetActive(false);
        interactableIcon3.SetActive(false);
        interactableIcon4.SetActive(false);
    }

    void Update()
    {
        antennaLine.amplitude = amplitudeKnob.knobValue;
        antennaLine.wavelength = wavelengthKnob.knobValue;

        if (radarLine.amplitude == antennaLine.amplitude && radarLine.wavelength == antennaLine.wavelength)
        {
            amplitudeTrigger.enabled = false;
            wavelengthTrigger.enabled = false;

            playerIcon.SetActive(true);
            interactableIcon1.SetActive(true);
            interactableIcon2.SetActive(true);
            interactableIcon3.SetActive(true);
            interactableIcon4.SetActive(true);
        }
    }
}

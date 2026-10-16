using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ExitWarning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile volumeProfile;
    [SerializeField] private FilmGrain filmGrain;

    [SerializeField] private float grainStartValue;
    [SerializeField] private float grainEndValue;
    [SerializeField] private float grainSpeed;

    void Start()
    {
        volumeProfile = volume.sharedProfile;
        if (volumeProfile.TryGet<FilmGrain>(out FilmGrain grain))
        {
            filmGrain = grain;
        }

        filmGrain.intensity.Override(grainStartValue);
        warningText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            warningText.enabled = false;
            filmGrain.intensity.Override(grainStartValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            warningText.enabled = true;
            filmGrain.intensity.Override(grainEndValue);
        }
    }
}

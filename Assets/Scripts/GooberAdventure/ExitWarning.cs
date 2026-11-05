using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ExitWarning : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private string warningText;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerUI.RemoveTopText();
            filmGrain.intensity.Override(grainStartValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerUI.SetTopText(warningText, true, false, false);
            filmGrain.intensity.Override(grainEndValue);
        }
    }
}

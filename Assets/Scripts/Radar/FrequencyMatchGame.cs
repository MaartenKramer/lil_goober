//#define USE_RADARPOS

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Math = System.Math;

public class FrequencyMatchGame : MonoBehaviour
{
    
    
    [SerializeField] private FrequencyLine radarLine;
    [SerializeField] private FrequencyLine antennaLine;

    [SerializeField] private Knob amplitudeKnob;
    [SerializeField] private Knob wavelengthKnob;
    
    [SerializeField] private List<GameObject> radarIcons;

    [SerializeField] private EventTrigger amplitudeTrigger;
    [SerializeField] private EventTrigger wavelengthTrigger;
    
    //TODO: THESE SHOULD NOT BE IN THE FREQUENCY MANAGER.AN OVERARCHING MANAGER SHOULD ASSIGN THESE VALUES BY REFERENCE.
    [SerializeField] private ProBuilderMesh ground;
    [SerializeField] private Transform playerTransform;
    
    private float levelWidth;
    private float levelHeight;
#if USE_RADARPOS
    [SerializeField] private float errorMargin = 3f;
#else
    [SerializeField] private float errorMargin = 0.1f;

#endif
    private bool radarIsOn = false;
    
    void Start()
    {
        foreach (GameObject radarIcon in radarIcons)
            radarIcon.SetActive(false);

        // Get bounds of level
        var (minBounds, maxBounds) = GetLevelBounds();
        // Use bounds to set amplitudes of Frequency-Lines
        levelWidth = Mathf.Abs(maxBounds.x - minBounds.x);
        levelHeight = Mathf.Abs(maxBounds.y - minBounds.y);
        
    }

    void Update()
    {
        // KNOB VALUES [0..1].. convert to coordinates
        antennaLine.amplitude = amplitudeKnob.knobValue;
        antennaLine.wavelength = wavelengthKnob.knobValue;
        // Set knob-values and draw
        radarLine.DrawLine();
        antennaLine.DrawLine();
        // Get coordinates from the knobs..
        var x = amplitudeKnob.knobValue * levelWidth;
        var y = wavelengthKnob.knobValue * levelHeight;
        Vector2 radarPosition = new Vector2(x, y);
        
        // Compare coordinates from know to player pos
        Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);

#if (USE_RADARPOS)

        if (Vector2.Distance(playerPos, radarPosition) <= errorMargin)
        {
            if (!radarIsOn)
            {
                ToggleRadar();
            }
        }
        else if (radarIsOn)
        {
            ToggleRadar();
        }
#else

        // If the two lines line up close enough..          (TODO: COULD JUST USE DISTANCE FUNCTION FOR PLAYER.POS && RADAR.POS)
        if (Math.Abs(radarLine.amplitude - antennaLine.amplitude) < errorMargin 
            && Math.Abs(radarLine.wavelength - antennaLine.wavelength) < errorMargin)
        {
            amplitudeTrigger.enabled = false;
            wavelengthTrigger.enabled = false;

            foreach (GameObject radarIcon in radarIcons)
                radarIcon.SetActive(true);
        }
#endif

}

    private void ToggleRadar()
    {
        // Flip the bool
        radarIsOn = !radarIsOn;
        // set each radar-icon to match the radarOn-bool
        foreach (GameObject radarIcon in radarIcons)
        {
            radarIcon.SetActive(radarIsOn);
        }
    }
   
    private (Vector2, Vector2) GetLevelBounds()
    {
        // Get all vertices of the level-shape..
        var vertices = ground.GetVertices();

        // Set accumulators
        float min_x = float.MaxValue, min_y = float.MaxValue;
        float max_x = float.MinValue, max_y = float.MinValue;
        
        // Find the bounds
        for (int i= 0; i < vertices.Length; i++)
        {
            var pos = vertices[i].position;
            if (pos.x < min_x) min_x = pos.x;
            if (pos.x > max_x) max_x = pos.x;
            if (pos.y < min_y) min_y = pos.y;
            if (pos.y > max_y) max_y = pos.y;
        }
        
        return (new Vector2(min_x, min_y), new Vector2(max_x, max_y));
    }
}

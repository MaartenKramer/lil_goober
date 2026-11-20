using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder;

public class FrequencyMatchGame : MonoBehaviour
{
    [SerializeField] private FrequencyLine radarLine;
    [SerializeField] private FrequencyLine antennaLine;

    [SerializeField] private GameObject radarBlue;
    [SerializeField] private GameObject radarRed;
    [SerializeField] private GameObject sweepLine;

    [SerializeField] private Knob amplitudeKnob;
    [SerializeField] private Knob wavelengthKnob;

    public List<GameObject> interactables;
    public List<GameObject> radarIcons;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject radarParent;

    [SerializeField] private EventTrigger amplitudeTrigger;
    [SerializeField] private EventTrigger wavelengthTrigger;
    
    //TODO: THESE SHOULD NOT BE IN THE FREQUENCY MANAGER.AN OVERARCHING MANAGER SHOULD ASSIGN THESE VALUES BY REFERENCE.
    [SerializeField] private ProBuilderMesh ground;
    [SerializeField] private Transform playerTransform;
    
    private float levelWidth;
    private float levelHeight;

    public float distancePlayerRadar;
    public float errorMargin = 0.1f;

    private bool radarIsOn = false;

    private Vector2 playerPos;
    private Vector2 radarPos;
    [SerializeField] private Vector2 radarOffset;
    
    void Start()
    {
        foreach (GameObject interactable in interactables)
        {
            GameObject newIcon = Instantiate(iconPrefab, interactable.transform.position, interactable.transform.rotation, radarParent.transform);
            radarIcons.Add(newIcon);
        }

        foreach (GameObject radarIcon in radarIcons)
            radarIcon.SetActive(false);

        // Get bounds of level
        var (minBounds, maxBounds) = GetLevelBounds();

        // Use bounds to set amplitudes of Frequency-Lines
        levelWidth = Mathf.Abs(maxBounds.x - minBounds.x);
        levelHeight = Mathf.Abs(maxBounds.y - minBounds.y);

        radarBlue.SetActive(false);
        radarRed.SetActive(true);
        sweepLine.SetActive(false);
    }

    void Update()
    {
        // Compare coordinates from knob to player pos
        playerPos = new Vector2(playerTransform.position.x, playerTransform.position.z);

        // KNOB VALUES [0..1].. convert to coordinates
        antennaLine.amplitude = amplitudeKnob.knobValue;
        antennaLine.wavelength = wavelengthKnob.knobValue;

        // Set knob-values and draw
        radarLine.amplitude = (playerPos.x + levelWidth/2) / levelWidth;
        radarLine.wavelength = (playerPos.y + levelHeight/2) / levelHeight;
        radarLine.DrawLine();
        antennaLine.DrawLine();

        // Get coordinates from the knobs..
        var x = amplitudeKnob.knobValue * levelWidth;
        var y = wavelengthKnob.knobValue * levelHeight;
        radarPos = new Vector2(x, y) - radarOffset;

        distancePlayerRadar = Vector2.Distance(playerPos, radarPos);

        if (distancePlayerRadar <= errorMargin)
        {
            if (!radarIsOn)
                ToggleRadar();
        }
        else if (radarIsOn)
            ToggleRadar();
}

    private void ToggleRadar()
    {
        if (radarIsOn)
        {
            radarBlue.SetActive(false);
            radarRed.SetActive(true);
            sweepLine.SetActive(false);

            foreach (GameObject radarIcon in radarIcons)
                radarIcon.SetActive(false);
        }
        else
        {
            radarBlue.SetActive(true);
            radarRed.SetActive(false);
            sweepLine.SetActive(true);

            foreach (GameObject radarIcon in radarIcons)
            {
                radarIcon.SetActive(true);
                radarIcon.GetComponent<Animator>().Play("Pulse", -1, 1);
            }
        }

        // Flip the bool
        radarIsOn = !radarIsOn;
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
            if (pos.z < min_y) min_y = pos.z;
            if (pos.z > max_y) max_y = pos.z;
        }
        
        return (new Vector2(min_x, min_y), new Vector2(max_x, max_y));
    }
}

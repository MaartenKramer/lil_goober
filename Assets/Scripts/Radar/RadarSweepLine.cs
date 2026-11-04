using UnityEngine;
using System.Collections;
using System;

public class RadarSweepLine : MonoBehaviour
{
    [SerializeField] private FrequencyMatchGame frequencyMatch;

    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private float radarSize = 30f;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Spin radar line
        this.gameObject.transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (GameObject interactable in frequencyMatch.interactables)
        {
            if (other == interactable.GetComponent<Collider>())
            {
                int index = frequencyMatch.interactables.IndexOf(interactable);
                Vector3 interactablePos = interactable.transform.position;

                if (Vector3.Distance(this.gameObject.transform.position, interactable.transform.position) > radarSize)
                {
                    Vector3 toRadar = interactablePos - transform.position;
                    toRadar = Vector3.ClampMagnitude(toRadar, radarSize);
                    Vector3 targetPos = transform.position + toRadar;
                    frequencyMatch.radarIcons[index].transform.position = targetPos;
                }
                else
                    frequencyMatch.radarIcons[index].transform.position = interactablePos;

                frequencyMatch.radarIcons[index].GetComponent<Animator>().Play("On");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject interactable in frequencyMatch.interactables)
        {
            if (other == interactable.GetComponent<Collider>())
            {
                int index = frequencyMatch.interactables.IndexOf(interactable);

                frequencyMatch.radarIcons[index].GetComponent<Animator>().Play("Pulse", -1, 0f);
            }
        }
    }
}

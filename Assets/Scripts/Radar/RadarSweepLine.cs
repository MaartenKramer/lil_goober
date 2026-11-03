using UnityEngine;
using System.Collections;
using System;

public class RadarSweepLine : MonoBehaviour
{
    [SerializeField] private FrequencyMatchGame frequencyMatch;

    //[SerializeField] private Transform goober;
    //[SerializeField] private GameObject playerIcon;

    [SerializeField] private float rotationSpeed = 150f;

    private void Start()
    {
        //playerIcon.GetComponent<Animator>().enabled = false;
        //playerIcon.transform.position = new Vector3(goober.position.x, playerIcon.transform.position.y, goober.position.z);
    }

    private void Update()
    {
        //Spin radar line
        this.gameObject.transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject interactable in frequencyMatch.interactables)
        {
            if (other == interactable.GetComponent<Collider>())
            {
                int index = frequencyMatch.interactables.IndexOf(interactable);
                frequencyMatch.radarIcons[index].transform.position = interactable.transform.position;
                frequencyMatch.radarIcons[index].GetComponent<Animator>().Play("On");
            }
        }



        //if (other.tag == "InteractObject")
            //other.GetComponent<Animator>().Play("Pulse", -1, 0f);

        //if (other.tag == "Player")
        //{
            //playerIcon.transform.position = new Vector3(goober.position.x, playerIcon.transform.position.y, goober.position.z);
            //playerIcon.GetComponent<Animator>().Play("Everything");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        //{
            //playerIcon.GetComponent<Animator>().enabled = true;
            //playerIcon.GetComponent<Animator>().Play("Pulse", -1, 0f);
        //}

        foreach (GameObject interactable in frequencyMatch.interactables)
        {
            if (other == interactable.GetComponent<Collider>())
            {
                int index = frequencyMatch.interactables.IndexOf(interactable);
                frequencyMatch.radarIcons[index].transform.position = interactable.transform.position;
                frequencyMatch.radarIcons[index].GetComponent<Animator>().Play("Pulse", -1, 0f);
            }
        }
    }
}

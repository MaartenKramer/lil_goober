using UnityEngine;
using System.Collections;

public class RadarSweepLine : MonoBehaviour
{
    [SerializeField] private Transform goober;
    [SerializeField] private GameObject playerIcon;

    private void Start()
    {
        playerIcon.GetComponent<Animator>().enabled = false;
        playerIcon.transform.position = new Vector3(goober.position.x, playerIcon.transform.position.y, goober.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InteractObject")
            other.GetComponent<Animator>().Play("Pulse", -1, 0f);

        if (other.tag == "Player")
        {
            playerIcon.transform.position = new Vector3(goober.position.x, playerIcon.transform.position.y, goober.position.z);
            playerIcon.GetComponent<Animator>().Play("Everything");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIcon.GetComponent<Animator>().enabled = true;
            playerIcon.GetComponent<Animator>().Play("Pulse", -1, 0f);
        }
    }
}

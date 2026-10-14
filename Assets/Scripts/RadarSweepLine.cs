using UnityEngine;
using System.Collections;

public class RadarSweepLine : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    private void Start()
    {
        playerAnim.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");

        if (other.tag == "InteractObject")
        {
            Debug.Log("Hit");
            other.GetComponent<Animator>().Play("Pulse", -1, 0f);
        }

        if (other.tag == "Player")
        {
            Debug.Log("Hit");
            other.GetComponent<Animator>().Play("Everything");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerAnim.enabled = true;
            other.GetComponent<Animator>().Play("Pulse", -1, 0f);
        }
    }
}

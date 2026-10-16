using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject radarIcon;

    public void IsScanned()
    {
        GooberCollectManager.instance.ScanMade();
        gameObject.GetComponent<SphereCollider>().enabled = false;
        radarIcon.SetActive(false);
    }
}

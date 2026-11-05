using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private FrequencyMatchGame frequencyMatch;
    [SerializeField] private int index = -1;

    private void Start()
    {
        foreach (GameObject interactable in frequencyMatch.interactables)
        {
            if (interactable.gameObject == this.gameObject)
                index = frequencyMatch.interactables.IndexOf(interactable);
        }

        if (index < 0)
            Debug.LogError("Interactable match not found");
    }

    public void IsScanned()
    {
        GooberCollectManager.instance.ScanMade(index);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        frequencyMatch.radarIcons[index].SetActive(false);
    }
}

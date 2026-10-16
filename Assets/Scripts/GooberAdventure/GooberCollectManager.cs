using UnityEngine;
using TMPro;
using System.Collections;

public class GooberCollectManager : MonoBehaviour
{
    public static GooberCollectManager instance;

    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI scanText;
    [SerializeField] private float textOnScreenTime = 1f;

    [SerializeField] private int totalScanAmount = 4;
    [SerializeField] private int currentScanAmount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        scanText.enabled = false;
    }

    public void ScanMade()
    {
        currentScanAmount++;

        if (currentScanAmount == totalScanAmount)
        {
            AdventureCompleted();
        }
        else
        {
            scanText.enabled = true;
            scanText.SetText(currentScanAmount + "/" + totalScanAmount + " things scanned");
            StartCoroutine(TextGoAway());
        }
    }

    private IEnumerator TextGoAway()
    {
        yield return new WaitForSeconds(textOnScreenTime);

        scanText.enabled = false;
    }

    private void AdventureCompleted()
    {
        scanText.enabled = true;
        scanText.SetText("You did it!");
    }
}

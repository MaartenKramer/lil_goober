using UnityEngine;

public class GooberCollectManager : MonoBehaviour
{
    public static GooberCollectManager instance;
    [SerializeField] private FrequencyMatchGame frequencyMatch;

    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private string baseReturnText;

    private int totalScanAmount = 0;
    private int currentScanAmount = 0;

    [SerializeField] private GameObject bunkerCollider;

    [SerializeField] private GameObject radarPlayerIcon;
    [SerializeField] private GameObject goober;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        bunkerCollider.SetActive(false);
        totalScanAmount = frequencyMatch.interactables.Count - 1;
    }

    public void ScanMade(int index)
    {
        if (index == frequencyMatch.interactables.Count - 1)
        {
            EndOfGame();
            return;
        }

        currentScanAmount++;

        if (currentScanAmount == totalScanAmount)
            AdventureCompleted();
        else
            playerUI.SetTopText($"{currentScanAmount} / {totalScanAmount} things collected", false, true, false);
    }

    private void AdventureCompleted()
    {
        playerUI.SetTopText(baseReturnText, false, false, true);
        bunkerCollider.SetActive(true);
    }

    private void EndOfGame()
    {
        radarPlayerIcon.SetActive(false);
        goober.SetActive(false);
        playerUI.EndOfGameUI();
    }
}

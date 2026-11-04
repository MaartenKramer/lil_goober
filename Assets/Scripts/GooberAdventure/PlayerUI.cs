using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private FrequencyMatchGame frequencyMatch;

    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI connectionText;
    [SerializeField] private Image black;
    [SerializeField] private Image tvStatic;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color transparent;

    [SerializeField] private float fadeDuration;

    [SerializeField] private int percentage = 0;

    private void Update()
    {
        SetConnectionText();
        SetBlackness();
        SetStaticLevel();
    }

    public void SetTopText(string text, float time, bool warning)
    {
        topText.SetText(text);

        if (warning)
            topText.color = warningColor;

        TextOnScreen(topText, time);
    }

    private void SetConnectionText()
    {
        float distance = frequencyMatch.distancePlayerRadar;
        float half = frequencyMatch.errorMargin;
        float min = half * 2;

        if (distance <= half)
            connectionText.color = baseColor;
        else
            connectionText.color = warningColor;

        if (distance > min)
            percentage = 0;
        else if (distance <= 0)
            percentage = 100;
        else
            percentage = 100 - (int)(distance * 10);

        connectionText.SetText($"Connection: {percentage}%");
    }

    private void SetBlackness()
    {
        if (percentage > 50)
            black.color = transparent;
        else
        {
            float transparency = (47 + (50 - (float)percentage)) / 100;
            black.color = new Color(0, 0, 0, transparency);
        }
    }

    private void SetStaticLevel()
    {
        if (percentage > 50)
            tvStatic.color = transparent;
        else
        {
            float transparency = (50 - (float)percentage) / 100;
            tvStatic.color = new Color(1, 1, 1, transparency);
        }
    }

    private IEnumerator TextOnScreen(TextMeshProUGUI text, float time)
    {
        yield return new WaitForSeconds(time);

        FadeText(text);
    }

    private void FadeText(TextMeshProUGUI text)
    {
        text.CrossFadeColor(transparent, fadeDuration, false, true, false);
    }
}

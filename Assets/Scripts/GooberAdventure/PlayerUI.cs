using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private FrequencyMatchGame frequencyMatch;

    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI connectionText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private Image blackness;
    [SerializeField] private Image tvStatic;
    [SerializeField] private Image endBackground;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color transparent;

    [SerializeField] private float textOnScreenTime = 2f;
    [SerializeField] private float fadeSpeed = 1f;

    private int connectionPercentage = 0;
    private string persistText;
    private bool textIsFading;
    private Color fadeStartColor;
    private float textTransparency;

    private void Start()
    {
        textTransparency = 1;
        endText.enabled = false;
        endBackground.enabled = false;
    }

    private void Update()
    {
        SetConnectionText();
        SetBlackness();
        SetStaticLevel();

        if (textIsFading)
            FadeText();
    }

    public void SetTopText(string text, bool warning, bool autoFade, bool persist)
    {
        StopAllCoroutines();
        textIsFading = false;
        textTransparency = 1;

        topText.SetText(text);

        if (warning)
            topText.color = warningColor;
        else
            topText.color = baseColor;

        if (autoFade)
            StartCoroutine(TextOnScreen(topText));

        if (persist)
            persistText = text;
    }

    private IEnumerator TextOnScreen(TextMeshProUGUI text)
    {
        fadeStartColor = text.color;

        yield return new WaitForSeconds(textOnScreenTime);

        textIsFading = true;
    }

    private void FadeText()
    {
        textTransparency = textTransparency - (fadeSpeed * Time.deltaTime);

        topText.color = new Color(fadeStartColor.r, fadeStartColor.g, fadeStartColor.b, textTransparency);

        if (textTransparency <= 0)
        {
            textIsFading = false;
            RemoveTopText();
            textTransparency = 1;
        }
    }

    public void RemoveTopText()
    {
        if (persistText != null)
        {
            topText.SetText(persistText);
            topText.color = baseColor;
        }
        else
            topText.SetText("");
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
            connectionPercentage = 0;
        else if (distance <= 0)
            connectionPercentage = 100;
        else
        {
            float a = 50 / half;
            connectionPercentage = 100 - (int)(a * distance);
        }

        connectionText.SetText($"Connection: {connectionPercentage}%");
    }

    private void SetBlackness()
    {
        if (connectionPercentage > 50)
            blackness.color = transparent;
        else
        {
            float transparency = (47 + (50 - (float)connectionPercentage)) / 100;
            blackness.color = new Color(0, 0, 0, transparency);
        }
    }

    private void SetStaticLevel()
    {
        if (connectionPercentage > 50)
            tvStatic.color = transparent;
        else
        {
            float transparency = (50 - (float)connectionPercentage) / 100;
            tvStatic.color = new Color(1, 1, 1, transparency);
        }
    }

    public void EndOfGameUI()
    {
        topText.enabled = false;
        connectionText.enabled = false;
        endText.enabled = true;
        endBackground.enabled = true;
    }
}

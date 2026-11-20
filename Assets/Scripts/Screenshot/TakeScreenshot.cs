using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] private Image whereToShowNewScreenshot;
    [SerializeField] private Image whereToShowOldScreenshot;

    [SerializeField] Slider oldScreenshotSlider;

    [SerializeField] private Camera gooberCamera;

    [SerializeField] private float sliderDownInterval = 0.05f;
    [SerializeField] private float sliderDownValue = 0.05f;

    private void Start()
    {
        oldScreenshotSlider.value = 1;
        TakeScreenshotAndShow();
        StartCoroutine(SliderDown());
    }

    private void Update()
    {
        if (oldScreenshotSlider.value <= 0)
        {
            StopAllCoroutines();
            whereToShowOldScreenshot.sprite = whereToShowNewScreenshot.sprite;
            TakeScreenshotAndShow();
            oldScreenshotSlider.value = 1;
            StartCoroutine(SliderDown());
        }
    }

    Texture2D RTImage(Camera gooberCamera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = gooberCamera.targetTexture;

        // Render the camera's view.
        gooberCamera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D screenshot = new Texture2D(gooberCamera.targetTexture.width, gooberCamera.targetTexture.height);
        screenshot.ReadPixels(new Rect(0, 0, gooberCamera.targetTexture.width, gooberCamera.targetTexture.height), 0, 0);
        screenshot.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return screenshot;
    }

    private void TakeScreenshotAndShow()
    {
        Texture2D screenshot = RTImage(gooberCamera);

        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        whereToShowNewScreenshot.enabled = true;
        whereToShowNewScreenshot.sprite = screenshotSprite;
    }

    private IEnumerator SliderDown()
    {
        oldScreenshotSlider.value -= sliderDownValue;

        yield return new WaitForSeconds(sliderDownInterval);

        StartCoroutine(SliderDown());
    }
}

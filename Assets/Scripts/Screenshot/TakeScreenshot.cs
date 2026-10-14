using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.InputSystem;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] Image whereToShowScreenshot;

    public InputActionReference screenshotInput;

    private void Update()
    {
        if (screenshotInput.action.IsPressed())
        {
            StartCoroutine(TakeScreenshotAndShow());
        }    
    }

    private IEnumerator TakeScreenshotAndShow()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        Texture2D newScreenshot = new Texture2D(screenshot.width, screenshot.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(screenshot.GetPixels());
        newScreenshot.Apply();

        Destroy(screenshot);

        Sprite screenshotSprite = Sprite.Create(newScreenshot, new Rect(0, 0, newScreenshot.width, newScreenshot.height), new Vector2(0.5f, 0.5f));

        whereToShowScreenshot.enabled = true;
        whereToShowScreenshot.sprite = screenshotSprite;
    }
}

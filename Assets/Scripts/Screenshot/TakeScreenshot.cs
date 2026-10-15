using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.InputSystem;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] Image whereToShowScreenshot;

    public InputActionReference screenshotInput;

    public Animator overlayAnimation;

    public Camera gooberCamera;

    private void Update()
    {
        if (screenshotInput.action.WasPressedThisFrame())
        {
            TakeScreenshotAndShow();

            PlayOverlayAnimation();
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

        whereToShowScreenshot.enabled = true;
        whereToShowScreenshot.sprite = screenshotSprite;
    }

        private void PlayOverlayAnimation()
    {
        overlayAnimation.Play("Printing", -1, 0f);
    }
}

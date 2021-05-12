using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFlash : MonoBehaviour
{
    Image im = null;
    Coroutine flashRoutine = null;

    private void Awake()
    {
        im = GetComponent<Image>();
    }

    public void TriggerFlash(float secondsForFlash, float maxAlpha, Color newColor)
    {
        im.color = newColor;

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(Flash(secondsForFlash, maxAlpha));
    }

    IEnumerator Flash(float secondsForFlash, float maxAlpha)
    {
        float flashInDuration = secondsForFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = im.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashInDuration);
            im.color = colorThisFrame;

            //wait until next frame
            yield return null;
        }

        // animate flash out
        float flashOutDuration = secondsForFlash / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = im.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            im.color = colorThisFrame;
            yield return null;
        }

        // ensure alpha is zero
        im.color = new Color32(0, 0, 0, 0);
    }
}

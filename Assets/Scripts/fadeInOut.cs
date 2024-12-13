using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fadeInOut : MonoBehaviour
{
    public Graphic uiElement;
    public float fadeDuration = 1f;
    public float delayBetweenFades = 1f;

    public void StartFadeSequence()
    {
        StartCoroutine(FadeInOutSequence());
    }

    private IEnumerator FadeInOutSequence()
    {
        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(delayBetweenFades);

        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        Color originalColor = uiElement.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            uiElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
    }
}
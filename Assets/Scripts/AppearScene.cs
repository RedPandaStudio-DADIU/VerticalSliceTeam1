using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearScene : MonoBehaviour
{
    [SerializeField] private Image fadeImage; 
    [SerializeField] private float fadeDuration = 3f; 

    private void Awake()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));

            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}

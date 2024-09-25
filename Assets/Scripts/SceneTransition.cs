using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 3f;

    private bool isFading = false;

    public void TriggerSceneTransition()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToBlackAndLoadScene("NextSceneName"));
        }
    }

    private IEnumerator FadeToBlackAndLoadScene(string sceneName)
    {
        isFading = true;

        Color color = fadeImage.color;
        float startAlpha = 0f;
        float endAlpha = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private StateController stateController;

    private bool isFading = false;

    void Start(){
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }

    void Update(){
        if(stateController.GetIsAgentDone()){
            if (!isFading)
            {
                StartCoroutine(FadeToBlack());
            }
        }
    }


    private IEnumerator FadeToBlack()
    {
        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = fadeImage.color;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("LVL2 grayboxing");
    }
}

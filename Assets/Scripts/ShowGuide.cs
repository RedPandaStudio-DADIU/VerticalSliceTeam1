using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ShowGuide  : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;   
    [SerializeField] private GameObject guide;          
    [SerializeField] private float guideDisplayTime = 10f;  

    private void Start()
    {
        if (videoPlayer != null)
        {
            
            videoPlayer.loopPointReached += OnVideoEnd;
             Time.timeScale = 0f;
             videoPlayer.Play(); 
        }

        if (guide != null)
        {
            guide.SetActive(false);  
        }
    }

    
    private void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(ShowGuideCoroutine());
        Time.timeScale = 1f; 
    }

    
    private IEnumerator ShowGuideCoroutine()
    {
        if (guide != null)
        {
            videoPlayer.enabled = false;
            guide.SetActive(true);  
            yield return new WaitForSeconds(guideDisplayTime); 
            guide.SetActive(false);  

            
            
        }
    }
}
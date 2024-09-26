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
        }

        if (guide != null)
        {
            guide.SetActive(false);  
        }
    }

    
    private void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(ShowGuideCoroutine());
    }

    
    private IEnumerator ShowGuideCoroutine()
    {
        if (guide != null)
        {
            videoPlayer.enabled = false;
            guide.SetActive(true);  
            yield return new WaitForSeconds(guideDisplayTime);  // 等待 10 秒
            guide.SetActive(false);  

            
            
        }
    }
}
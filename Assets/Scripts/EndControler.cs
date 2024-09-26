using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements;

public class EndControler : MonoBehaviour
{
     [SerializeField] private VideoPlayer videoPlayer;   // 视频播放器
    [SerializeField] private UIDocument uiDocument;     // 引导信息的 UI Document

    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayer != null)
        {
            
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        
        if (uiDocument != null)
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();  
            Debug.Log("Game is quitting...");

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (uiDocument != null)
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;  // 显示 UI
        }
    }
}

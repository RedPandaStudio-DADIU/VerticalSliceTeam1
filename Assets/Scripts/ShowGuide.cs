using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements; 

public class ShowGuide  : MonoBehaviour
{
     
    [SerializeField] private UIDocument guideDocument;  // Reference to the UIDocument
     [SerializeField] private float guideDisplayTime = 5f;  
    

    private void Start()
    {

       if (guideDocument != null)
        {
            guideDocument.enabled = false;  // Hide the UI document initially
        }
        StartCoroutine(ShowGuideCoroutine());
    }

    
  
    
     private IEnumerator ShowGuideCoroutine()
    {
        guideDocument.enabled = true;  // Show the UI document
            yield return new WaitForSeconds(guideDisplayTime);  // Wait for the specified time
            guideDocument.enabled = false;  // Hide the UI document after the time elapses
       
    }
}
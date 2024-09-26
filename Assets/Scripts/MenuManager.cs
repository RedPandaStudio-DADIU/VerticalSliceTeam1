using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements; 
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private VisualElement controlPicture;  // UI Toolkit VisualElement
    [SerializeField] private VisualElement creditsPicture;
     [SerializeField] private VideoPlayer videoPlayer; 
      [SerializeField] private UIDocument uiDocument;  
   
    // Start is called before the first frame update
    void Start()
    {
        // Get the root VisualElement from the UI Document
        var root = GetComponent<UIDocument>().rootVisualElement;

        controlPicture = root.Q<VisualElement>("controlP");  
        creditsPicture = root.Q<VisualElement>("creditP");   

        // Initially, hide the control and credits panels
        controlPicture.style.display = DisplayStyle.None;
        creditsPicture.style.display = DisplayStyle.None;

        // Assign button functionality if buttons are also in the UI Builder
        Button startButton = root.Q<Button>("start");
        Button quitButton = root.Q<Button>("Quit");
        Button controlsButton = root.Q<Button>("control");
        Button creditsButton = root.Q<Button>("credits");

        // Link buttons to their respective methods
        startButton.clicked += OnStartButtonPressed;
        quitButton.clicked += OnQuitButtonPressed;
        controlsButton.clicked += OnControlsButtonPressed;
        creditsButton.clicked += OnCreditsButtonPressed;

        videoPlayer.gameObject.SetActive(false);
    }

    // Start the game by loading another scene
    public void OnStartButtonPressed()
    {
        StartCoroutine(PlayVideoAndLoadScene());// Load the game scene after the video finishes
        
    }

    private IEnumerator PlayVideoAndLoadScene()
    {
        // Enable and start the video player
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        if (uiDocument != null)
        {
            uiDocument.enabled = false;  
        }

        // Give it a brief moment to start playing
        yield return new WaitForSeconds(0.1f);

        // Wait until the video finishes
        while (videoPlayer.isPlaying == false)
        {
            yield return null; // Wait for the video to start playing
        }

        // Wait until the video finishes playing
        while (videoPlayer.isPlaying)
        {
            yield return null; // Wait for the video to finish
        }
        // Load the game scene after the video finishes
        SceneManager.LoadScene("Game Scene");  // Replace with your actual scene name
   
    }


    // Quit the game (this works in the built version of the game, not in the editor)
    public void OnQuitButtonPressed()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        
        Application.Quit();
        
        #endif

        Debug.Log("Game is quitting...");
     }

    // Show the control picture by enabling the control panel
    public void OnControlsButtonPressed()
    {
        controlPicture.style.display = DisplayStyle.Flex;  // Show the control panel
    }

    // Show the credits picture by enabling the credits panel
    public void OnCreditsButtonPressed()
    {
        creditsPicture.style.display = DisplayStyle.Flex;  // Show the credits panel
    }

    // Update is called once per frame
    private void Update()
    {
        if ((creditsPicture.style.display == DisplayStyle.Flex || controlPicture.style.display == DisplayStyle.Flex))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                creditsPicture.style.display = DisplayStyle.None;
                controlPicture.style.display = DisplayStyle.None;
            }
        }
    }
}
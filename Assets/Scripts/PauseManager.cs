using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
     [Header("UI Elements")]
    [SerializeField] private UIDocument uiDocument;  // Reference to the UI Document (for UI Toolkit)
    private VisualElement pausePanel;
    private VisualElement controlPanel;

    private bool isPaused = false;  // To track game pause state
     private bool isControlOpen = false;  // To track if the control panel is open

    
    // Start is called before the first frame update
    void Start()
    {// Access the root VisualElement of the UI Document
        pausePanel = uiDocument.rootVisualElement.Q<VisualElement>("pausePanel");
         controlPanel = uiDocument.rootVisualElement.Q<VisualElement>("controlP");

        Debug.Log(pausePanel != null ? "Pause panel loaded" : "Pause panel missing");
        Debug.Log(controlPanel != null ? "Control panel loaded" : "Control panel missing");

        // Hide pause menu and control panel at the start
        pausePanel.style.display = DisplayStyle.None;
        controlPanel.style.display = DisplayStyle.None;

        // Access the buttons directly
        Button continueButton = pausePanel.Q<Button>("continue");
        Button controlButton = pausePanel.Q<Button>("control");
        Button quitButton = pausePanel.Q<Button>("quit");

        // Assign button actions
        continueButton.clicked += ResumeGame;
        controlButton.clicked += ShowControls;
        quitButton.clicked += QuitGame;
    }

    private void Update()
    {
        // Toggle pause menu when Esc is pressed
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isControlOpen)
            {
                // If the control panel is open, close it and return to the pause menu
                CloseControls();
            }
            else if (isPaused)
            {
                // If the game is paused, resume the game when pressing Escape again
                ResumeGame();
            }
            else
            {
                // If not paused, pause the game and show the pause menu
                PauseGame();
            }
        }
    }

    // Pause the game and show the pause menu
    private void PauseGame()
    {
        isPaused = true;
        pausePanel.style.display = DisplayStyle.Flex;  // Show pause menu
        Time.timeScale = 0f;  // Pause game
        UnityEngine.Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        UnityEngine.Cursor.visible = true;  // Show cursor
    }

    // Resume the game and hide the pause menu
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.style.display = DisplayStyle.None;  // Hide pause menu
        controlPanel.style.display = DisplayStyle.None;  // Hide control panel if it was visible
        Time.timeScale = 1f;  // Resume game
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  // Lock cursor
        UnityEngine.Cursor.visible = false;  // Hide cursor
    }

    // Show the control panel
    public void ShowControls()
    {
        isControlOpen = true;
        controlPanel.style.display = DisplayStyle.Flex;  // Show control panel
        pausePanel.style.display = DisplayStyle.None;  // Hide pause menu
     }

    // Close the control panel and return to the pause menu
    public void CloseControls()
    {
        isControlOpen = false;
        controlPanel.style.display = DisplayStyle.None;  // Hide control panel
        pausePanel.style.display = DisplayStyle.Flex;  // Show pause menu again
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Exit play mode in the editor
        #else
        Application.Quit();  // Quit the game in a build
        #endif
    }
}
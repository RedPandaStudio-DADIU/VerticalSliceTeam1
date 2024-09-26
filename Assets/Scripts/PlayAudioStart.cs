using UnityEngine;
using AK.Wwise; 

public class PlayAudioStart : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event introMusicEvent; 
    [SerializeField] private string soundBank = "soundbank_MAIN";

    void Start()
    {
        AkSoundEngine.LoadBank(soundBank, out uint bankID);

        PauseGame();
        PlayIntroMusic();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    private void PlayIntroMusic()
    {
        introMusicEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, OnMusicEnd);
    }

    private void OnMusicEnd(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        ResumeGame();
    }
}

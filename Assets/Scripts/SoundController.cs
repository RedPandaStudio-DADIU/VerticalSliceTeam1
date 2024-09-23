using UnityEngine;
using AK.Wwise;

public class SoundController : MonoBehaviour
{
    public string soundBank = "Soundbank_Spirit";
    public string soundEvent = "spirit_Laugh_play";

    // void Start()
    // {
    //     AkSoundEngine.LoadBank(soundBank, out uint bankID);

    // }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("Playing Sound?");
    //         PlaySound(soundEvent);
    //     }
    // }

    // public void PlaySound(string eventName)
    // {
    //     AkSoundEngine.PostEvent(eventName, gameObject);
    // }
}

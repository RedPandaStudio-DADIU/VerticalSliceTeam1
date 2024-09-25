using UnityEngine;
using AK.Wwise;

public class SoundController : MonoBehaviour
{
    public string soundBank = "soundbank_MAIN";
    public string soundEvent = "Footsteps_spirit_SW";

    void Start()
    {
        AkSoundEngine.LoadBank(soundBank, out uint bankID);
        Debug.Log("Bank: " + bankID);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Playing Sound?");
            PlaySound(soundEvent);
        }
    }

    public void PlaySound(string eventName)
    {
         AkSoundEngine.PostEvent(eventName, gameObject);
    }
}
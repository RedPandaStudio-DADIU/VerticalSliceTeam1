// using UnityEngine;
// using AK.Wwise;

// public class SoundController : MonoBehaviour
// {
//     [SerializeField] private string soundBank = "soundbank_MAIN";
//     [SerializeField] private AK.Wwise.Event ambienceEvent;


//     void Start()
//     {
//         AkSoundEngine.LoadBank(soundBank, out uint bankID);
//         Debug.Log("Bank: " + bankID);
//         ambienceEvent.Post(gameObject);

//     }
// }
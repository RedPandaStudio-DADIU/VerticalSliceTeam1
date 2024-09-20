using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakState : NPCBaseState
{
    private bool startedPlaying = false;
    public override void OnEnter(StateController controller){
        AudioSource voice = controller.GetNpcVoice();
        if(!voice.isPlaying){
            voice.Play();
            voice.volume = 1.0f;
            Debug.Log("Playing"+ voice.clip.name);
            startedPlaying = true;
        }
    }
    public override void OnUpdate(StateController controller){
        AudioSource voice = controller.GetNpcVoice();
        if (startedPlaying && !voice.isPlaying){
            controller.ChangeState(new IdleState());
        }
    }

    public override void OnExit(StateController controller){
        Debug.Log("Existng Speak");
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }

}

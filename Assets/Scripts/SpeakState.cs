using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakState : NPCBaseState
{
    public override void OnEnter(StateController controller){
        AudioSource voice = controller.GetNpcVoice();
        if(!voice.isPlaying){
            voice.Play();
        }
    }
    public override void OnUpdate(StateController controller){
        AudioSource voice = controller.GetNpcVoice();
        if (!voice.isPlaying){
            controller.ChangeState(new IdleState());
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Existng Speak");
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }

    public override void OnCollisionExit(StateController controller, Collision other){
        Debug.Log("Collision exit");
    }
}

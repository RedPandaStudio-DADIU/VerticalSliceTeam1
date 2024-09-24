using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakState : NPCBaseState
{
    private bool startedPlaying = false;
    public override void OnEnter(StateController controller){
        controller.GetNpcAnimator().SetBool("isSpeaking", true);

        // AudioSource voice = controller.GetNpcVoice();
        // if(!voice.isPlaying){
        //     voice.Play();
        //     voice.volume = 1.0f;
        //     Debug.Log("Playing"+ voice.clip.name);
        startedPlaying = true;
        // }
    }
    public override void OnUpdate(StateController controller){
        // AudioSource voice = controller.GetNpcVoice();
        // if (startedPlaying && !voice.isPlaying){
        if (startedPlaying){
            // controller.GetNpcAnimator().SetBool("isSpeaking", false);
            controller.ChangeState(new IdleState());
        }
    }

    public override void OnExit(StateController controller){
        Debug.Log("Existng Speak");
        controller.GetNpcAnimator().SetBool("isSpeaking", false);

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }

}

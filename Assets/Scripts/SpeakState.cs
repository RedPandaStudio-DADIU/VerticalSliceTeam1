using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakState : NPCBaseState
{
    private uint playingID;
    public override void OnEnter(StateController controller){
        controller.GetNpcAnimator().SetBool("isSpeaking", true);
        controller.GetNpcAnimator().SetBool("isFleeing", false);
        controller.GetNpcAnimator().SetBool("isScared", false);
        controller.GetNpcAnimator().SetBool("isMoving", false);
        
        playingID = AkSoundEngine.PostEvent("npc_dialogue", controller.GetNpcGameObject(), (uint)AkCallbackType.AK_EndOfEvent, SoundEndCallback, controller);
    }

    private void SoundEndCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if (in_type == AkCallbackType.AK_EndOfEvent)
        {
            StateController controller = in_cookie as StateController;
            if (controller != null)
            {
                controller.ChangeState(new IdleState());
            }
        }
    }

    public override void OnUpdate(StateController controller){
        // controller.ChangeState(new IdleState());
    }

    public override void OnExit(StateController controller){
        Debug.Log("Existng Speak");
        controller.GetNpcAnimator().SetBool("isSpeaking", false);

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AK.Wwise;



public class MoveState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    // private bool isPlaying = false;
    private uint playingId;

    public override void OnEnter(StateController controller){
        Debug.Log("Move state here!");
        // Manage animations
        controller.GetNpcAnimator().SetBool("isMoving", true);
        controller.GetNpcAnimator().SetBool("isFleeing", false);
        controller.GetNpcAnimator().SetBool("isScared", false);
        controller.GetNpcAnimator().SetBool("isSpeaking", false);

        //Manage sounds

        playingId = controller.GetNpcWalkEvent().Post(controller.GetNpcGameObject());



        movingNpc = controller.GetNpc();
        if(!movingNpc.enabled){
            controller.EnableNavMeshAgent();
        }
        movingNpc.destination = controller.GetEndTransform().position;
        Debug.Log("Moving destination" + movingNpc.destination);
        movingNpc.angularSpeed = 0.0f;
        movingNpc.updateRotation = true;

    }

    public override void OnUpdate(StateController controller){
        // Vector3 direction = movingNpc.velocity.normalized;

        // if (direction != Vector3.zero){
        //     controller.transform.LookAt(movingNpc.velocity.normalized);
        // }
        Debug.Log("Destination: " + movingNpc.destination);
        if (movingNpc.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.LogError("Invalid path to destination.");
        }
        if (movingNpc.remainingDistance <= movingNpc.stoppingDistance)
        {
            if (!movingNpc.pathPending)
            {

                if(controller.RemoveEndTransform() != null){
                    movingNpc.destination = controller.GetEndTransform().position;
                    controller.RecalculatePathForNPC();
                } else {
                    controller.ChangeState(new IdleState());
                    Debug.Log("Found the end");
                    controller.SetIsAgentDone(true);
                    // controller.RecalculatePathForNPC();
                }

            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting the Move State");
        controller.DisableNavMeshAgent();
        controller.GetNpcAnimator().SetBool("isMoving", false);
        AkSoundEngine.StopPlayingID(playingId);
        // isPlaying = false;
        

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("FreeRock") || other.gameObject.CompareTag("Water")){
            AkSoundEngine.StopPlayingID(playingId);
            // isPlaying = false;
            controller.ChangeState(new SpeakState());
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    public override void OnEnter(StateController controller){
        Debug.Log("Move state here!");
        controller.GetNpcAnimator().SetBool("isMoving", true);
        controller.GetNpcAnimator().SetBool("isFleeing", false);
        controller.GetNpcAnimator().SetBool("isScared", false);
        controller.GetNpcAnimator().SetBool("isSpeaking", false);

        movingNpc = controller.GetNpc();
        if(!movingNpc.enabled){
            controller.EnableNavMeshAgent();
        }
        movingNpc.destination = controller.GetEndTransform().position;
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
                controller.ChangeState(new IdleState());
                // Debug.Log("Found the end");
                controller.RecalculatePathForNPC();

            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting the Move State");
        controller.DisableNavMeshAgent();
        controller.GetNpcAnimator().SetBool("isMoving", false);

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("FreeRock") || other.gameObject.CompareTag("Water")){
            controller.ChangeState(new SpeakState());
        }
    }

}

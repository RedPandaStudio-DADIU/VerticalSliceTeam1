using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FleeState : NPCBaseState
{
    private NavMeshAgent movingNpc;

    public override void OnEnter(StateController controller){  
        Debug.Log("Entering Flee State");
        controller.GetNpcAnimator().SetBool("isFleeing", true);
        controller.GetNpcAnimator().SetBool("isScared", false);
        controller.GetNpcAnimator().SetBool("isSpeaking", false);
        controller.GetNpcAnimator().SetBool("isMoving", false);

        movingNpc = controller.GetNpc();
        if(!movingNpc.enabled){
            controller.EnableNavMeshAgent();
        }
        movingNpc.destination = controller.GetStartTransform().position;
        movingNpc.speed = 5f;

        movingNpc.angularSpeed = 0.0f;
        movingNpc.updateRotation = true;

    }
    public override void OnUpdate(StateController controller){
        // Vector3 direction = (controller.GetStartTransform().position - movingNpc.transform.position).normalized;

        // if (direction != Vector3.zero){
        //     controller.transform.LookAt(movingNpc.velocity.normalized);
        // }

        if (movingNpc.remainingDistance <= movingNpc.stoppingDistance)
        {
            if (!movingNpc.pathPending)
            {
                controller.ChangeState(new IdleState());
                Debug.Log("Found the start");
            }
        }
    }
    public override void OnExit(StateController controller){
        controller.DisableNavMeshAgent();
        controller.GetNpcAnimator().SetBool("isFleeing", false);

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
    }

}

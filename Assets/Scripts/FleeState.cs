using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FleeState : NPCBaseState
{
    private NavMeshAgent movingNpc;

    public override void OnEnter(StateController controller){  
        Debug.Log("Entering Flee State");
        movingNpc = controller.GetNpc();
        movingNpc.destination = controller.GetStartTransform().position;
        movingNpc.speed = 5f;

        movingNpc.angularSpeed = 0.0f;
        movingNpc.updateRotation = true;

    }
    public override void OnUpdate(StateController controller){
        Vector3 direction = (controller.GetStartTransform().position - movingNpc.transform.position).normalized;

        if (direction != Vector3.zero){
            controller.transform.LookAt(movingNpc.velocity.normalized);
            controller.transform.rotation *=  Quaternion.Euler(controller.GetXRotation(), 0f, 0f) ;

            foreach (Transform child in controller.transform)
            {
                child.rotation = controller.transform.rotation;
            }
        }

        if (movingNpc.remainingDistance <= movingNpc.stoppingDistance)
        {
            if (!movingNpc.pathPending)
            {
                controller.ChangeState(new IdleState());
            }
        }
    }
    public override void OnExit(StateController controller){
        controller.DisableNavMeshAgent();

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
    }

    public override void OnCollisionExit(StateController controller, Collision other){
    }
}

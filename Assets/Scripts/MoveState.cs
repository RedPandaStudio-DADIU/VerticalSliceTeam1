using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    public override void OnEnter(StateController controller){
        movingNpc = controller.getNpc();
        movingNpc.destination = controller.getEndTransform().position;
        movingNpc.angularSpeed = 0.0f;
        movingNpc.updateRotation = true;
    }
    public override void OnUpdate(StateController controller){
        Vector3 direction = movingNpc.velocity.normalized;

        if (direction != Vector3.zero){
            controller.transform.LookAt(movingNpc.velocity.normalized);
            controller.transform.rotation *=  Quaternion.Euler(controller.getXRotation(), 0f, 0f) ;

            foreach (Transform child in controller.transform)
            {
                child.rotation = controller.transform.rotation;
            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting the Move State");
        controller.disableNavMeshAgent();
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        if(other.gameObject.CompareTag("Obstacle")){
            controller.ChangeState(new IdleState());
        }
    }

}

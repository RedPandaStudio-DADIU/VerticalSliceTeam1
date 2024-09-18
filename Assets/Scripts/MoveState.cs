using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    public override void OnEnter(StateController controller){
        movingNpc = controller.GetNpc();
        movingNpc.destination = controller.GetEndTransform().position;
        movingNpc.angularSpeed = 0.0f;
        movingNpc.updateRotation = true;
    }
    public override void OnUpdate(StateController controller){
        Vector3 direction = movingNpc.velocity.normalized;

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
                Debug.Log("Found the end");
            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting the Move State");
        controller.DisableNavMeshAgent();

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("Obstacle")){
            controller.ChangeState(new SpeakState());
        }
    }

    public override void OnCollisionExit(StateController controller, Collision other){
        Debug.Log("Collision exit");
    }

}

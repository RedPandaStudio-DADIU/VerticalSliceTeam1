using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCBaseState
{

    public override void OnEnter(StateController controller){
        Debug.Log("Enter");
    }
    public override void OnUpdate(StateController controller){
        Debug.Log("Update");
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exit");
        controller.EnableNavMeshAgent();
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }

    public override void OnCollisionExit(StateController controller, Collision other){
        Debug.Log("Collision exit");
        if(other.gameObject.CompareTag("Obstacle")){
            controller.ChangeState(new MoveState());
        }
    }

}

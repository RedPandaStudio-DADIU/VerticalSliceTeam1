using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IdleState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    public override void OnEnter(StateController controller){
        Debug.Log("Enter");
    }
    public override void OnUpdate(StateController controller){
        Debug.Log("Update");
        movingNpc = controller.GetNpc();
        if ((movingNpc.pathStatus == NavMeshPathStatus.PathComplete) || (movingNpc.pathStatus == NavMeshPathStatus.PathPartial) && !movingNpc.pathPending)
        {
            controller.ChangeState(new MoveState());
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exit");
        // controller.EnableNavMeshAgent();
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }


}

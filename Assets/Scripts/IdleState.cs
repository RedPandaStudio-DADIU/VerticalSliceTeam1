using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IdleState : NPCBaseState
{
    private NavMeshAgent movingNpc;
    public override void OnEnter(StateController controller){
        Debug.Log("Enter");
        controller.GetNpcAnimator().SetBool("isMoving", false);

    }
    public override void OnUpdate(StateController controller){
        Debug.Log("Update");
        movingNpc = controller.GetNpc();
        if ((movingNpc.pathStatus == NavMeshPathStatus.PathComplete) && !movingNpc.pathPending)
        {
            // controller.GetNpcAnimator().SetBool("isMoving", true);
            controller.ChangeState(new MoveState());
            
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exit");
        controller.GetNpcAnimator().SetBool("isMoving", true);

        // controller.EnableNavMeshAgent();
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }


}

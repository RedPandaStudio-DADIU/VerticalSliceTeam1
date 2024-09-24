using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScaredState : NPCBaseState
{

    // placeholder for future animation
    private float animationDuration = 5f;
    private float animationStartTime;
    private NavMeshAgent movingNpc;

    public override void OnEnter(StateController controller){  
        Debug.Log("Entering Scared State");
        movingNpc = controller.GetNpc();
        if(movingNpc.enabled && !movingNpc.isStopped){
            movingNpc.isStopped = true;
        }
         

        animationStartTime = Time.time;
    
    }
    public override void OnUpdate(StateController controller){
        // placeholder for future animation
        if ((Time.time - animationStartTime) >= animationDuration)
        {
            if(controller.GetPreviousState() is MoveState){
                controller.ChangeState(new FleeState());
            } else if(controller.GetPreviousState() is FleeState){
                controller.ChangeState(new MoveState());
            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting Scared State");
        if(movingNpc.enabled && movingNpc.isStopped){
            movingNpc.isStopped = false;
        }

    }

    public override void OnCollisionEnter(StateController controller, Collision other){
    }

}

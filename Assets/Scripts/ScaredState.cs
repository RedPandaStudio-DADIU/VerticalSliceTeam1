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
        controller.GetNpcAnimator().SetBool("isScared", true);

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
                // controller.GetNpcAnimator().SetBool("isScared", false);
                // controller.GetNpcAnimator().SetBool("isFleeing", true);
                controller.ChangeState(new FleeState());
            } else if(controller.GetPreviousState() is FleeState){
                // controller.GetNpcAnimator().SetBool("isScared", false);
                // controller.GetNpcAnimator().SetBool("isMoving", true);
                controller.ChangeState(new MoveState());
            }
        }
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exiting Scared State");
        if(movingNpc.enabled && movingNpc.isStopped){
            movingNpc.isStopped = false;
        }
        controller.GetNpcAnimator().SetBool("isScared", false);


    }

    public override void OnCollisionEnter(StateController controller, Collision other){
    }

}

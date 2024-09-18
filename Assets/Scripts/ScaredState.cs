using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScaredState : NPCBaseState
{

    // placeholder for future animation
    private float animationDuration = 5f;
    private float animationStartTime;


    public override void OnEnter(StateController controller){  
        Debug.Log("Entering Scared State");
        controller.transform.rotation =  Quaternion.Euler(controller.GetXRotation(), 0f, 0f) ;
        animationStartTime = Time.time;
    
    }
    public override void OnUpdate(StateController controller){
        // placeholder for future animation
        if (Time.time - animationStartTime >= animationDuration)
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
        // controller.transform.rotation =  Quaternion.Euler(controller.GetXRotation(), 0f, 180f) ;
        controller.EnableNavMeshAgent();


    }

    public override void OnCollisionEnter(StateController controller, Collision other){
    }

    public override void OnCollisionExit(StateController controller, Collision other){
    }
}

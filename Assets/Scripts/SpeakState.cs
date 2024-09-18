using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakState : NPCBaseState
{
    public override void OnEnter(StateController controller){
        Debug.Log("Enter");
    }
    public override void OnUpdate(StateController controller){
        Debug.Log("Update");
    }
    public override void OnExit(StateController controller){
        Debug.Log("Exit");
    }

    public override void OnCollisionEnter(StateController controller, Collision other){
        Debug.Log("Collision");
    }
}

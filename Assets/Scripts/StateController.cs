using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    [Header("NPC AI settings")]
    [SerializeField] private Transform end;
    private NPCBaseState currentState;
    private NavMeshAgent npc;
    private float xRotation = -90f;

    void Start(){
        npc = GetComponent<NavMeshAgent>();
        currentState = new MoveState();
        currentState.OnEnter(this);
    }

    void Update(){
        currentState.OnUpdate(this);
    }

    public void ChangeState(NPCBaseState newState){
        currentState.OnExit(this);

        currentState = newState;
        currentState.OnEnter(this);
    }

    void OnCollisionEnter(Collision other){
        currentState.OnCollisionEnter(this, other); 
    }

    public NavMeshAgent getNpc(){
        return npc;
    }

    public float getXRotation(){
        return xRotation;
    }

    public Transform getEndTransform(){
        return end;
    }

    public void disableNavMeshAgent(){
        npc.enabled = false;
    }
}



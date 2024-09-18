using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    [Header("NPC AI settings")]
    [SerializeField] private Transform end;
    [SerializeField] private Transform start;

    private AudioSource npcVoice;
    private NPCBaseState currentState;
    private NPCBaseState previousState;

    private NavMeshAgent npc;
    private float xRotation = -90f;


    void Start(){
        npc = GetComponent<NavMeshAgent>();
        npcVoice = GetComponent<AudioSource>();
        currentState = new MoveState();
        previousState = currentState;
        currentState.OnEnter(this);
    }

    void Update(){
        currentState.OnUpdate(this);
    }

    public void ChangeState(NPCBaseState newState){
        currentState.OnExit(this);
        previousState = currentState;
        currentState = newState;
        currentState.OnEnter(this);
    }

    void OnCollisionEnter(Collision other){
        currentState.OnCollisionEnter(this, other); 
    }

    void OnCollisionExit(Collision other){
        currentState.OnCollisionExit(this, other); 
        Debug.Log("Collision exit");
    }

    public NavMeshAgent GetNpc(){
        return npc;
    }

    public float GetXRotation(){
        return xRotation;
    }

    public Transform GetEndTransform(){
        return end;
    }
    public Transform GetStartTransform(){
        return start;
    }

    public void DisableNavMeshAgent(){
        npc.enabled = false;
    }

    public void EnableNavMeshAgent(){
        npc.enabled = true;
    }

    public AudioSource GetNpcVoice(){
        return npcVoice; 
    }

    public NPCBaseState GetCurrentState(){
        return currentState; 
    }

    public NPCBaseState GetPreviousState(){
        return previousState; 
    }
}



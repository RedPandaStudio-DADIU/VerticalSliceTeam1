using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AK.Wwise;


public class StateController : MonoBehaviour
{

    [Header("NPC AI settings")]
    [SerializeField] private Transform end;
    [SerializeField] private Transform start;
    [SerializeField] private Animator NpcAnimator;
    [SerializeField] private GameObject npcGameObject;
    [SerializeField] private AK.Wwise.Event npcWalkEvent;

    // private AudioSource npcVoice;
    private NPCBaseState currentState;
    private NPCBaseState previousMoveState;
    private string soundBank = "soundbank_MAIN";

    private NavMeshAgent npc;
    private float xRotation = -90f;

    void Start(){
        npc = GetComponent<NavMeshAgent>();
        // npcVoice = GetComponent<AudioSource>();
        currentState = new MoveState();
        previousMoveState = currentState;
        currentState.OnEnter(this);
        AkSoundEngine.LoadBank(soundBank, out uint bankID);

    }

    void Update(){
        currentState.OnUpdate(this);
        Debug.Log("Current State: " + currentState);
    }

    public void ChangeState(NPCBaseState newState){
        currentState.OnExit(this);
        if(currentState is MoveState || currentState is FleeState){
            previousMoveState = currentState;
        }
        currentState = newState;
        currentState.OnEnter(this);
    }

    void OnCollisionEnter(Collision other){
        currentState.OnCollisionEnter(this, other); 
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
    public bool isEnabled(){
        return npc.enabled;
    }
    public void DisableNavMeshAgent(){
        npc.enabled = false;
    }

    public void EnableNavMeshAgent(){
        npc.enabled = true;
    }
    public NPCBaseState GetCurrentState(){
        return currentState; 
    }

    public NPCBaseState GetPreviousState(){
        return previousMoveState; 
    }

    public void RecalculatePathForNPC()
    {
        if (!npc.enabled)
        {
            npc.enabled = true;
            Vector3 currentDestination = npc.destination;
            npc.ResetPath(); 
            npc.SetDestination(currentDestination); 
        }
        
    }

    public Animator GetNpcAnimator(){
        return NpcAnimator;
    }

    public AK.Wwise.Event GetNpcWalkEvent(){
        return npcWalkEvent;
    }

    public GameObject GetNpcGameObject(){
        return npcGameObject;
    }
}



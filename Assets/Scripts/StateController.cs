using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    private NPCBaseState currentState;

    void Start()
    {
        currentState = new IdleState();
        currentState.OnEnter(this);
    }

    void Update()
    {
        currentState.OnUpdate(this);
    }

    public void ChangeState(NPCBaseState newState)
    {
        currentState.OnExit(this);

        currentState = newState;
        currentState.OnEnter(this);
    }
}



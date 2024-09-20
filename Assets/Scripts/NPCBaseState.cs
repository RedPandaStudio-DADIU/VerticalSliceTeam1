using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBaseState
{
    public abstract void OnEnter(StateController controller);
    public abstract void OnUpdate(StateController controller);
    public abstract void OnExit(StateController controller);
    public abstract void OnCollisionEnter(StateController controller, Collision other);
}

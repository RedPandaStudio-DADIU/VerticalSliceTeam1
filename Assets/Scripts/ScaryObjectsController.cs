using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryObjectsController : MonoBehaviour
{
    [SerializeField] private GameObject npc;

    private float collisionRadius = 3f;
    private StateController stateController;

    void Start()
    {
        stateController = npc.GetComponent<StateController>();
    }

    void Update()
    {
        // Debug.Log("It's executing");
        Collider[] colliders = Physics.OverlapSphere(stateController.transform.position, collisionRadius);
        
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("ScaryObject") && !(stateController.GetCurrentState() is ScaredState)) 
            {
                stateController.ChangeState(new ScaredState()); 
                // For now - put the object far away
                col.transform.position = new Vector3(300f, 300f, 300f);
            }
        }
    }

}

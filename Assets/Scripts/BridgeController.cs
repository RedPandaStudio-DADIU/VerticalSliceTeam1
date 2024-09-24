using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BridgeController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent movingNPC;
    private int noOfCollidingRockSets = 0;
    private int noOfCollidingRocks = 0;
    private StateController stateController;
    private bool pathRecalculated = false;

    void Start(){
        stateController = FindObjectOfType<StateController>();
    }

    void Update()
    {
        if (noOfCollidingRocks == noOfCollidingRockSets && !pathRecalculated){
            RecalculatePathForNPC();
            pathRecalculated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            noOfCollidingRocks++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            noOfCollidingRocks++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RockSet"))
        {
            noOfCollidingRockSets++;
        }
    }

    void RecalculatePathForNPC()
    {
        if (!movingNPC.enabled){
            movingNPC.enabled = true;
            movingNPC.autoBraking = true; 
            movingNPC.SetDestination(stateController.GetEndTransform().position);
            stateController.ChangeState(new MoveState());
        }
    }
}

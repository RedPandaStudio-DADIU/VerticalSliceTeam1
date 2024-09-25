using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BridgeController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent movingNPC;
    private int noOfCollidingRockSets = 0;
    private int noOfCollidingRocks = 0;
    private int noOfOccupiedRockSets = 0;

    private StateController stateController;
    private bool pathRecalculated = false;

    private List<GameObject> collidingRockSets = new List<GameObject>();
    private RockManager rockManager;


    void Start(){
        stateController = FindObjectOfType<StateController>();
        rockManager = FindObjectOfType<RockManager>();

    }

    void Update()
    {
        Debug.Log("Num of colliding rockSets: " + noOfCollidingRockSets + ", Num of colliding rocks: " + noOfCollidingRocks);
        if (noOfCollidingRocks == noOfCollidingRockSets && !pathRecalculated && CheckIfOccupied()){
            Debug.LogWarning("Recalculating path");
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
            noOfCollidingRocks--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RockSet"))
        {
            if (!collidingRockSets.Contains(collision.gameObject)){
                collidingRockSets.Add(collision.gameObject);
            }
            noOfCollidingRockSets++;
        }
    }

    private bool CheckIfOccupied(){
        foreach (GameObject rockSet in collidingRockSets){
            if (!rockManager.rockSetsDict[rockSet]) {
                return false;
            }
        }
        return true;
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

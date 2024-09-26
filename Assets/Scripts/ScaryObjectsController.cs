using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryObjectsController : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private float spawnHeight = 20f;
    [SerializeField] private GameObject scaryObjectPrefab;

    private float waitTimeMin =15f;
    private float waitTimeMax = 30f;
    private float collisionRadius = 10f;
    private StateController stateController;
    

    void Start()
    {
        stateController = npc.GetComponent<StateController>();
        StartCoroutine(SpawnScaryObject());
        collisionRadius = 30f;
        spawnHeight = 20f;
        waitTimeMin = 15f;
        waitTimeMax = 30f;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(stateController.transform.position, collisionRadius);
        
        foreach (Collider col in colliders)
        {
            // if (col.CompareTag("ScaryObject") && !(stateController.GetCurrentState() is ScaredState)) 
            if (col.CompareTag("ScaryObject") && ((stateController.GetCurrentState() is MoveState) || (stateController.GetCurrentState() is IdleState && stateController.GetPreviousState() is MoveState))) 
            {
                Debug.Log("ChangingToScared");
                stateController.ChangeState(new ScaredState()); 
            }
        }
    }

    private IEnumerator SpawnScaryObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(waitTimeMax, waitTimeMax)); 
            Vector3 spawnPosition = npc.transform.position + npc.transform.forward * 10f;
            spawnPosition.y = spawnHeight; 
            if (((stateController.GetCurrentState() is MoveState) || (stateController.GetCurrentState() is IdleState))){
                GameObject scaryObject = Instantiate(scaryObjectPrefab, spawnPosition, Quaternion.identity);

                Destroy(scaryObject, 7f);
            
            } 

        }
    }

}

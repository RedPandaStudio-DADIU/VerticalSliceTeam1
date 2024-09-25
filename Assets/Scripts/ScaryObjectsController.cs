using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryObjectsController : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private GameObject scaryObjectPrefab;

    private float waitTimeMin =7f;
    private float waitTimeMax = 12f;
    private float collisionRadius = 10f;
    private StateController stateController;
    

    void Start()
    {
        stateController = npc.GetComponent<StateController>();
        StartCoroutine(SpawnScaryObject());
        collisionRadius = 30f;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(stateController.transform.position, collisionRadius);
        
        foreach (Collider col in colliders)
        {
            // if (col.CompareTag("ScaryObject") && !(stateController.GetCurrentState() is ScaredState)) 
            if (col.CompareTag("ScaryObject") && (stateController.GetCurrentState() is MoveState)) 
            {
                Debug.Log("ChangingToScared");
                stateController.ChangeState(new ScaredState()); 
                // For now - put the object far away
                col.transform.position = new Vector3(300f, 300f, 300f);
            }
        }
    }

    private IEnumerator SpawnScaryObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(waitTimeMax, waitTimeMax)); 
            Vector3 spawnPosition = npc.transform.position + npc.transform.forward * 15f;
            spawnPosition.y = spawnHeight; 
            GameObject scaryObject = Instantiate(scaryObjectPrefab, spawnPosition, Quaternion.identity);
        }
    }

}

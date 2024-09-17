using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [Header("NPC AI settings")]
    [SerializeField] private Transform end;
    private NavMeshAgent npc;
    private float xRotation = -90f;
    private Quaternion initialRotationOffset;

    void Start()
    {
        npc = GetComponent<NavMeshAgent>();
        npc.destination = end.position;
        npc.angularSpeed = 0.0f;
        npc.updateRotation = true;

    }

    void Update()
    {
        Vector3 direction = npc.velocity.normalized;

        if (direction != Vector3.zero){
            transform.LookAt(npc.velocity.normalized);
            transform.rotation *=  Quaternion.Euler(xRotation, 0f, 0f) ;

            foreach (Transform child in transform)
            {
                child.rotation = transform.rotation;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CirclesManager : MonoBehaviour
{
    
    [Header("Circle and Obstacle Configuration")]
    [SerializeField] private GameObject[] circles;  // Array to hold circle objects
    [SerializeField] private GameObject[] obstacles;  // Array to hold obstacle objects
    [SerializeField] private Material defaultMaterial;  // The default material for circles
    [SerializeField] private Material disabledMaterial;  // The disabled material for circles
    [SerializeField] private NavMeshAgent movingNPC;
    [SerializeField] private float radiusOverlap = 5f;
    private StateController stateController;

    void Start(){
        stateController = FindObjectOfType<StateController>();
    }

    // Method to remove the obstacle based on the circle index
    public void RemoveObstacle(int circleIndex)
    {
        if (circleIndex >= 0 && circleIndex < obstacles.Length)
        {
            if (obstacles[circleIndex] != null)
            {
                // check whether NPC is within specified distance of the obstacle 
                Collider[] colliders = Physics.OverlapSphere(obstacles[circleIndex].transform.position, radiusOverlap);

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("NPC"))
                    {
                        obstacles[circleIndex].SetActive(false);  // Disable the obstacle
                        Debug.Log("Obstacle " + obstacles[circleIndex].name + " removed.");
                        stateController.RecalculatePathForNPC();
                    }
                }

                
            }
        }
    }




    // Method to get the index of the entered circle
    public int GetCircleIndex(GameObject circle)
    {
        for (int i = 0; i < circles.Length; i++)
        {
            if (circles[i] == circle)
            {
                return i;  // Return the index of the circle
            }
        }
        return -1;  // Return -1 if circle is not found
    }

    // Method to enable/disable circle interaction and change material
    public void SetCircleInteraction(bool isEnabled)
    {
        foreach (GameObject circle in circles)
        {
            Renderer circleRenderer = circle.GetComponent<Renderer>();
            if (circleRenderer != null)
            {
                circleRenderer.material = isEnabled ? defaultMaterial : disabledMaterial;
            }

            // Disable/Enable the circle's collider to prevent interaction
            Collider circleCollider = circle.GetComponent<Collider>();
            if (circleCollider != null)
            {
                circleCollider.enabled = isEnabled;
            }
        }

        //Debug.Log("Circles interaction set to: " + isEnabled);
    }
}
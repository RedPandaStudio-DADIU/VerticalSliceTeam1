using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CirclesManager : MonoBehaviour
{
    
    [Header("Circle and Obstacle Configuration")]
    [SerializeField] private GameObject[] circles;  // Array to hold circle objects
    [SerializeField] private GameObject[] obstacles;  // Array to hold obstacle objects
    [SerializeField] private NavMeshAgent movingNPC;
    [SerializeField] private GameObject npc;
    [SerializeField] private float radiusOverlap = 5f;
    [SerializeField] private GameObject fireflyEffect; 
    [SerializeField] private PlayerController playerController; 
    [SerializeField] private Animator npcAnimator;  
    [SerializeField] private string idleStateName = "Idle";  

    private StateController stateController;
    private float idleTime = 0f;
    private bool firefliesTriggered = false;
    public float timeToTriggerFireflies = 3;
    void Start(){
        
        SetCircleInteraction(false); 

        fireflyEffect.SetActive(false);

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

    private void Update()
    {
        
        if (npcAnimator != null && npcAnimator.GetCurrentAnimatorStateInfo(0).IsName(idleStateName))
        {
            int currentCircleIndex = playerController.currentCircleIndex;  // 从PlayerController获取currentCircleIndex

            idleTime += Time.deltaTime;

            if (idleTime >= timeToTriggerFireflies && !firefliesTriggered)
            {
                
                ShowFirefliesAtCircle(currentCircleIndex);  
                //firefliesTriggered = true;
                //SetCircleInteraction(true);  
            }
        }
        else
        {
            idleTime = 0f;  
            fireflyEffect.SetActive(false);
            //firefliesTriggered = false;  
            //SetCircleInteraction(false);  
        }
    }

   
    public void ShowFirefliesAtCircle(int circleIndex)
    {
        foreach (GameObject circle in circles)
        {
            fireflyEffect.transform.position = circle.transform.position;
            fireflyEffect.SetActive(true); 
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
                circleRenderer.enabled = false;  // Keep the object invisible
            }

            // Keep the collider active for interaction
            Collider circleCollider = circle.GetComponent<Collider>();
            if (circleCollider != null)
            {
                circleCollider.enabled = isEnabled;  // Only disable collider when explicitly needed
            }
        }

        //Debug.Log("Circles interaction set to: " + isEnabled);
    }
}
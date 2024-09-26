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
    public GameObject fireflyPrefab; 
    //[SerializeField] private GameObject fireflyEffect; 
    [SerializeField] private PlayerController playerController; 
    [SerializeField] private Animator npcAnimator;  
    [SerializeField] private string idleStateName = "IdleState";  

    private StateController stateController;
    private float idleTime = 0f;
    private bool firefliesTriggered = false;
    public float timeToTriggerFireflies = 8f;

    private AnimatorStateInfo currentAnimatorStateInfo;
    private AnimatorStateInfo previousAnimatorStateInfo;
    
    void Start(){
        
        SetCircleInteraction(false); 

        //fireflyEffect.SetActive(false);

        stateController = FindObjectOfType<StateController>();
    }

    // Method to remove the obstacle based on the circle index
    public void RemoveObstacle(int circleIndex)
    {
        /*if (circleIndex >= 0 && circleIndex < obstacles.Length)
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
        */


         if (circleIndex >= 0 && circleIndex < obstacles.Length && obstacles[circleIndex] != null)
        {
            
            obstacles[circleIndex].SetActive(false);  // Disable the obstacle
            Debug.Log("Obstacle " + obstacles[circleIndex].name + " removed.");
            
            // Recalculate the path for the NPC
            if (stateController != null)
            {
                stateController.RecalculatePathForNPC();
            }
        }
        else
        {
            Debug.Log("Invalid circle index or obstacle is null.");
        }

    }

    private void Update()
    {
        
        currentAnimatorStateInfo = npcAnimator.GetCurrentAnimatorStateInfo(0);
        if((stateController.GetCurrentState() is IdleState || stateController.GetCurrentState() is SpeakState) && (stateController.GetEarlierState() is not IdleState && stateController.GetEarlierState() is not SpeakState))
        {
            
            Debug.Log("NPC into IdleState");
            idleTime = 0f;  
            firefliesTriggered = false; 
             StartCoroutine(ShowFirefliesAfterDelay(0, timeToTriggerFireflies, firefliesTriggered));
  
            //ShowFirefliesAtCircle(0, firefliesTriggered); 
        }
        else
        {
            idleTime = 0f;  
            firefliesTriggered = false;
        }

        
        // if (currentAnimatorStateInfo.IsName(idleStateName))
        // if ((stateController.GetCurrentState() is IdleState) || (stateController.GetCurrentState() is SpeakState))

        // {
        //     // idleTime += Time.deltaTime;

        //     // if (idleTime >= timeToTriggerFireflies && !firefliesTriggered)
        //     // {
        //     //     Debug.Log("time to show fireflies");

        //     firefliesTriggered = false;
        //     ShowFirefliesAtCircle(0, firefliesTriggered);  
        //     //     firefliesTriggered = true;  
        //     // }
        // }
        

        
        previousAnimatorStateInfo = currentAnimatorStateInfo;
    }

    private IEnumerator ShowFirefliesAfterDelay(int circleIndex, float delay, bool firefliesTriggered)
    {
        
        yield return new WaitForSeconds(delay);

        
        
            ShowFirefliesAtCircle(circleIndex,firefliesTriggered);  
          
    }
   
    public void ShowFirefliesAtCircle(int circleIndex, bool firefliesTriggered)
    {
        if(!firefliesTriggered){
            for (int i = 0; i < circles.Length; i++)
            {
                
                GameObject newFireflyEffect = Instantiate(fireflyPrefab, circles[i].transform.position, fireflyPrefab.transform.rotation);

                
                ParticleSystem fireflyParticleSystem = newFireflyEffect.GetComponent<ParticleSystem>();
                
                if (fireflyParticleSystem != null)
                {
                    
                    fireflyParticleSystem.Play();
                }
                
                Destroy(newFireflyEffect, 10f);
            }
            firefliesTriggered = true;
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
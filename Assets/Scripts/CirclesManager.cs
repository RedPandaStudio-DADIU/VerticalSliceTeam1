using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclesManager : MonoBehaviour
{
    
    [Header("Circle and Obstacle Configuration")]
    [SerializeField] private GameObject[] circles;  // Array to hold circle objects
    [SerializeField] private GameObject[] obstacles;  // Array to hold obstacle objects

    // Method to remove the obstacle based on the circle index
    public void RemoveObstacle(int circleIndex)
    {
        if (circleIndex >= 0 && circleIndex < obstacles.Length)
        {
            if (obstacles[circleIndex] != null)
            {
                obstacles[circleIndex].SetActive(false);  // Disable the obstacle
                Debug.Log("Obstacle " + obstacles[circleIndex].name + " removed.");
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
}
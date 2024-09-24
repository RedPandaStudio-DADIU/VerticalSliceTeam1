using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> rocks;
    [SerializeField] private List<GameObject> rockSets;
    [SerializeField] private List<GameObject> freeRocks; 

    public int GetRockIndex(GameObject rock)
    {
        return rocks.IndexOf(rock);
    }

    public GameObject GetRock(int index)
    {
        if (index >= 0 && index < rocks.Count)
        {
            return rocks[index];
        }
        return null;
    }

    public int GetFreeRockIndex(GameObject freeRock)
    {
        return freeRocks.IndexOf(freeRock);
    }

    public GameObject GetFreeRock(int index)
    {
        if (index >= 0 && index < freeRocks.Count)
        {
            return freeRocks[index];
        }
        return null;
    }

    // Get the index of the rock set
    public int GetRockSetIndex(GameObject rockSet)
    {
        return rockSets.IndexOf(rockSet);
    }

    
    public GameObject GetRockSet(int index)
    {
        Debug.Log("Requesting RockSet with index: " + index);  // Log the index being requested

        if (index >= 0 && index < rockSets.Count)
        {
            return rockSets[index];
        }
        Debug.Log("RockSet index out of bounds or not found.");
   
        return null;
    }

    // Ensure that the rock matches the correct rock set
    public bool IsMatchingRockAndSet(int rockIndex, int rockSetIndex)
    {
        /*
        if (rockIndex >= 0 && rockIndex < rocks.Count && rockSetIndex >= 0 && rockSetIndex < rockSets.Count)
        {
            // Logic to ensure the rock matches the correct rock set (based on their indices)
            // If rock and rockSet indices match, return true
            return rockIndex == rockSetIndex;
        }
        return false;
        */
        if (rockIndex >= 0 && rockIndex < rocks.Count && rockSetIndex >= 0 && rockSetIndex < rockSets.Count)
        {
            // Logic to ensure the rock matches the correct rock set (based on their indices)
            // If rock and rockSet indices match, return true
            return true; 
        }
        return false;
        // Placeholder logic, replace with actual matching logic
    }
}

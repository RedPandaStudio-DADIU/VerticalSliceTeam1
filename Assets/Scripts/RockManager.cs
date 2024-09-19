using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> rocks;
    [SerializeField] private List<GameObject> rockSets;

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

    // Get the index of the rock set
    public int GetRockSetIndex(GameObject rockSet)
    {
        return rockSets.IndexOf(rockSet);
    }

    public GameObject GetRockSet(int index)
    {
        if (index >= 0 && index < rockSets.Count)
        {
            return rockSets[index];
        }
        return null;
    }

    // Ensure that the rock matches the correct rock set
    public bool IsMatchingRockAndSet(int rockIndex, int rockSetIndex)
    {
        if (rockIndex >= 0 && rockIndex < rocks.Count && rockSetIndex >= 0 && rockSetIndex < rockSets.Count)
        {
            // Logic to ensure the rock matches the correct rock set (based on their indices)
            // If rock and rockSet indices match, return true
            return rockIndex == rockSetIndex;
        }
        return false;
    }
}

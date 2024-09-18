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

    public GameObject GetRockSet(int index)
    {
        if (index >= 0 && index < rockSets.Count)
        {
            return rockSets[index];
        }
        return null;
    }
}

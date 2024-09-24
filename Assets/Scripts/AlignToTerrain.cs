using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class AlignToTerrain : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private GameObject[] roads; 
    [SerializeField] private float offsetY = 0.1f;

    [SerializeField] private NavMeshSurface navMeshSurface;

    void Awake()
    {
        AdjustAllRoads();
        navMeshSurface.BuildNavMesh();
    }

    void AdjustAllRoads()
    {
        foreach (GameObject road in roads)
        {
            AdjustRoadHeight(road);
        }
    }

    void AdjustRoadHeight(GameObject road)
    {
        Mesh mesh = road.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = road.transform.TransformPoint(vertices[i]);

            float terrainHeight = terrain.SampleHeight(worldPos);
            worldPos.y = terrainHeight + offsetY;
            vertices[i] = road.transform.InverseTransformPoint(worldPos);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}

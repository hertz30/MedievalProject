using UnityEngine;

public class WorldSetup : MonoBehaviour
{
    [ContextMenu("Create World Boundary")]
    void CreateWorldBoundary()
    {
        float mapSize = 800f;
        float boundaryThickness = 1f; // Adjust as needed
        int barrierHeight = 100; // Adjust the height

        // Create top boundary
        CreateBoxCollider(new Vector3(0f, boundaryThickness / 2f, mapSize / 2f), new Vector3(mapSize, boundaryThickness, barrierHeight));

        // Create bottom boundary
        CreateBoxCollider(new Vector3(0f, boundaryThickness / 2f, -mapSize / 2f), new Vector3(mapSize, boundaryThickness, barrierHeight));

        // Create left boundary
        CreateBoxCollider(new Vector3(-mapSize / 2f, boundaryThickness / 2f, 0f), new Vector3(boundaryThickness, barrierHeight, mapSize));

        // Create right boundary
        CreateBoxCollider(new Vector3(mapSize / 2f, boundaryThickness / 2f, 0f), new Vector3(boundaryThickness, barrierHeight, mapSize));
    }

    void CreateBoxCollider(Vector3 position, Vector3 size)
    {
        GameObject boundary = new GameObject("Boundary");
        boundary.transform.position = position;
        BoxCollider boxCollider = boundary.AddComponent<BoxCollider>();
        boxCollider.size = size;
    }
}
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

public class WorldSetup : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("Remove NavMeshObstacle Recursively")]
    void RemoveNavMeshObstacleContext()
    {
        RemoveNavMeshObstacleRecursive(transform);
    }
#endif

    void RemoveNavMeshObstacleRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Remove or disable the NavMeshObstacle component
            NavMeshObstacle navMeshObstacle = child.GetComponent<NavMeshObstacle>();
            if (navMeshObstacle != null)
            {
                // You can either remove the NavMeshObstacle component
                DestroyImmediate(navMeshObstacle);
                // OR disable it if you might want to enable it later
                // navMeshObstacle.enabled = false;
            }

            RemoveNavMeshObstacleRecursive(child);
        }
    }
}
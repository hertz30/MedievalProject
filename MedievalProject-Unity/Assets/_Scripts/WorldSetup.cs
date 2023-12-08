using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSetup : MonoBehaviour
{
    void Start()
    {
        // Recursively set tag to "ground" and add MeshColliders to all child objects
        SetTagAndAddMeshCollidersRecursivelyToChildren(transform);
    }

    void SetTagAndAddMeshCollidersRecursivelyToChildren(Transform parent)
    {
        // Set tag to "ground" for the current object if it has a MeshFilter component
        MeshFilter meshFilter = parent.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            parent.gameObject.tag = "Ground";
            MeshCollider meshCollider = parent.gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.sharedMesh;
        }

        // Recursively process child objects
        foreach (Transform child in parent)
        {
            SetTagAndAddMeshCollidersRecursivelyToChildren(child);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeIncrease : MonoBehaviour
{
    public GameObject tracks;
    private Mesh originalMesh;
    private List<Vector3> expandedPositions;
    void Start()
    {
        originalMesh = tracks.GetComponent<MeshFilter>().mesh;
        //List<string> triangles = from triangle in originalMesh.triangles select triangle.ToString();
        foreach (var v in originalMesh.vertices)
        {
            Vector3 newVector1 = v;
            newVector1.y = newVector1.y + 0.1f;

            Vector3 newVector2 = v;
            newVector2.y = newVector2.y - 0.1f;
            expandedPositions.Add(newVector1);
            expandedPositions.Add(newVector2);

        }
    }
}

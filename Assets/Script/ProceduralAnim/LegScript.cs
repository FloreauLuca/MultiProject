using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegScript : MonoBehaviour
{
    [SerializeField] private Transform foot;
    private MeshFilter meshFilter;
    [SerializeField] private Mesh startMesh;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("--------------------");
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = startMesh.vertices;
        int[] triangles = mesh.triangles;
        Vector2[] uvs = mesh.uv;
        mesh.Clear();
        Vector3 newPos = new Vector3((foot.position.x - transform.position.x) / transform.localScale.x,
            (foot.position.y - transform.position.y) / transform.localScale.y,
            (foot.position.z - transform.position.z) / transform.localScale.z);
        newPos -= (transform.localScale.y)/2 * Vector3.down;
        Debug.Log(newPos);
        for(int i = 0; i < vertices.Length; i++) {
            if (vertices[i].y == -0.5f)
            {
                vertices[i] += newPos;
                Debug.Log(vertices[i]);
            }
        }
        //Debug.Log("---");
        //foreach (int triangle in triangles)
        //{
        //    Debug.Log(triangle);
        //}
        //Debug.Log("---");
        //foreach (Vector2 uv in uvs)
        //{
        //    Debug.Log(uv);
        //}
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
}

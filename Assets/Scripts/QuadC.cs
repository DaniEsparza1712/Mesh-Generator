using UnityEngine;
using UnityEngine.Serialization;

//Consulted in https://docs.unity3d.com/Manual/Example-CreatingaBillboardPlane.html
public class QuadC : MonoBehaviour
{
    private Mesh _mesh;
    public Material mat;

    public void CreateQuad(Vector3[] vertices)
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(mat);
        
        _mesh = new Mesh();
        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        
        SetVertices(vertices);
        SetTriangles();
        SetNormals();
        SetUVs();

        meshFilter.mesh = _mesh;
    }

    public void SetVertices(Vector3[] newVertices)
    {
        _mesh.vertices = newVertices;
    }

    public void SetTriangles()
    {
        int[] tris = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };
        _mesh.triangles = tris;
    }

    public void SetNormals()
    {
        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        _mesh.normals = normals;
    }

    public void SetUVs()
    {
        Vector2[] uvs = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        _mesh.uv = uvs;
    }
}

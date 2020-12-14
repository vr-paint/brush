using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
   

public class operation6 : MonoBehaviour
{
    // Start is called before the first frame update
     Mesh mesh;
     Vector3[] vertices;
     int[] triangles;
     public float cellsize;
     public Vector3 gridoffset;
     int gridSize;
    void Awake()
    {
        mesh=GetComponent<MeshFilter>().mesh;
    }
    void start(){
        MakeProceduralGrid ();
        UpdateMesh ();
    }
    void MakeProceduralGrid(){
        vertices=new Vector3[4];
        triangles=new int [6];
        float vertexoffset=cellsize*0.5f;
        vertices[0]=new Vector3(-vertexoffset,0,-vertexoffset)+gridoffset;
        vertices[1]=new Vector3(-vertexoffset,0,vertexoffset)+gridoffset;
        vertices[2]=new Vector3(vertexoffset,0,-vertexoffset)+gridoffset;
        vertices[3]=new Vector3(vertexoffset,0,vertexoffset)+gridoffset;
        triangles[0]=0;
        triangles[1]= triangles[4]=1;
        triangles[2]= triangles[3]=2;
        triangles[5]=3;
        
    }
    // Update is called once per frame
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        mesh.RecalculateNormals();
    }
}

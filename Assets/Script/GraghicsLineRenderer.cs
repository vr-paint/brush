using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class GraphicsLineRenderer : MonoBehaviour
{
    public Material Imat;
    private Mesh dMesh;
    private Vector3 oldPos;
    private float lineWidth = .1f;
    private float lineWidthInSqueeze = 1f;
    private bool firstQuad = true;
    public Vector3[] vertices;
    //public int[] tris;
    public Vector3 c;
    public bool ErrorCheck=false;
    //public Vector3[] normals = new Vector3[0];
    public Vector3 magicNormal;

    void Start()
    {
        dMesh = GetComponent<MeshFilter>().mesh; //dMesh為mesh的值
        GetComponent<MeshRenderer>().material = Imat;
        
    }

    public bool Check() {
        if (ErrorCheck == false)
        {
            
            Destroy(gameObject);
      
            return true;
        }
        return false;
    }
    
    public void setwidth(float ws)
    {
        lineWidth = ws;
    }
    public void setwidth2(float ws)
    {
        lineWidthInSqueeze = ws;
        /*
        if (ws > 0.9f) lineWidthInSqueeze = 1.0f;
        else if (ws > 0.8f) lineWidthInSqueeze = 0.9f;
        else if (ws > 0.7f) lineWidthInSqueeze = 0.8f;
        else if (ws > 0.6f) lineWidthInSqueeze = 0.7f;
        else if (ws > 0.5f) lineWidthInSqueeze = 0.6f;
        else if (ws > 0.4f) lineWidthInSqueeze = 0.5f;
        else if (ws > 0.3f) lineWidthInSqueeze = 0.4f;
        else if (ws > 0.2f) lineWidthInSqueeze = 0.3f;
        else if (ws > 0.1f) lineWidthInSqueeze = 0.2f;
        else lineWidthInSqueeze = 0.1f;*/

    }
    public void setVectorCross(Vector3 a, Vector3 b)
    {
        c = Vector3.Cross(a, b);
        c.Normalize();
    }
    public void AddPoint(Vector3 point)
    {
        if (oldPos != Vector3.zero)
        {//Vector3.zero 是Vector3(0,0,0)
            Vector3[] Quads = MakeQuad(oldPos, point, lineWidth, firstQuad);
            AddLine(dMesh, Quads);
            firstQuad = false;//firstQuad為假
        }
     
        oldPos = point;
    }
    Vector3[] MakeQuad(Vector3 o, Vector3 n, float w, bool all)
    { //作四邊形(o=oldPos, n=point, w=lineWidth, all=firstQuad)
        Vector3[] qd;
        if (all) qd = new Vector3[6];
        else qd = new Vector3[3];
    // GameObject Change;
        if (all)
        {
            qd[0] = transform.InverseTransformPoint(o);
            qd[3] = transform.InverseTransformPoint(n);

            qd[1] = transform.InverseTransformPoint(o + c * w * lineWidthInSqueeze);
            qd[2] = transform.InverseTransformPoint(o - c * w * lineWidthInSqueeze);
            qd[4] = transform.InverseTransformPoint(n + c * w * lineWidthInSqueeze);
            qd[5] = transform.InverseTransformPoint(n - c * w * lineWidthInSqueeze);
       
        }
        else
        {
            qd[0] = transform.InverseTransformPoint(n);//n;
            qd[1] = transform.InverseTransformPoint(n + c * w * lineWidthInSqueeze);//n+c*w;
            qd[2] = transform.InverseTransformPoint(n - c * w * lineWidthInSqueeze);
    
        }
        return qd;
    }
    public KDtreeBlender Blender;
    void AddLine(Mesh m, Vector3[] quad)
    { //加線(m=dMesh, quad=Quads) 
        int vl = m.vertices.Length; //v1為Mesh m的頂點長度


        vertices = m.vertices;
        vertices = resizeVertices(vl, vertices, quad);
        m.vertices = vertices;

        int tl = m.triangles.Length; //t1為m三角形的長度
        int[] tris = m.triangles;
        tris = resizeTriangles(tl, tris, quad.Length);
        m.triangles = tris;

        magicNormal = Normal(quad);

        Vector3[] normals = m.normals;
        normals = resizeNormals(vl, normals, quad, vertices.Length);
        m.normals = normals;//jsyeh

        m.RecalculateBounds();
        //m.RecalculateNormals();

       // Debug.Log(vertices[vertices.Length-1]);
      //  Debug.Log(vertices[vertices.Length-2]);
        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().LineVertices(vertices[vertices.Length - 2], vertices[vertices.Length - 1]);


     
    }

    private Vector3[] qd1 = new Vector3[2];
    Vector3 Normal(Vector3[] quad)
    {
        Vector3 z;
        if (quad.Length == 6)
        {
            //x = quad[4] - quad[2]; y = quad[5] - quad[1];
            z = Vector3.Cross(quad[4] - quad[2], quad[5] - quad[1]);
            qd1[0] = quad[4]; qd1[1] = quad[5];
        }
        else
        {
            //x = quad[1] - qd1[1]; y = quad[2] - qd1[0];
            z = Vector3.Cross(quad[1] - qd1[1], quad[2] - qd1[0]);
            qd1[0] = quad[1]; qd1[1] = quad[2];
        }
        z.Normalize();
        return z;
    }

    /////resize區域:陣列擴增
    Vector3[] resizeNormals(int vl, Vector3[] oldNormals, Vector3[] nowQ, int magicLength)
    {
        Vector3[] nowNormals = new Vector3[magicLength];
        //for (int i=0; i < oldNormals.Length; i++) { nowNormals[i] = new Vector3(oldNormals[i].x, oldNormals[i].y, oldNormals[i].z); }
        for (int i = 0; i < oldNormals.Length; i++){nowNormals[i] = oldNormals[i];}
        //for (int i = 0; i < nowQ.Length; i++) { nowNormals[vl + i] = new Vector3( magicNormal.x, magicNormal.y, magicNormal.z) ; }
        for (int i = 0; i < nowQ.Length; i++){nowNormals[vl + i] = magicNormal;}
        return nowNormals;
    }

    Vector3[] resizeVertices(int vl, Vector3[] oldVs, Vector3[] nowQ)
    {//調頂點大小
        Vector3[] nowVs = new Vector3[oldVs.Length + nowQ.Length];
        for (int i = 0; i < oldVs.Length; i++) { nowVs[i] = oldVs[i]; }
        for (int i = 0; i < nowQ.Length; i++) { nowVs[vl + i] = nowQ[i]; }
        return nowVs;
    }
    int[] resizeTriangles(int tl, int[] oTris, int qs)
    { //調三角形的大小
        int[] nowTris = new int[oTris.Length + 24];
        for (int i = 0; i < oTris.Length; i++) { nowTris[i] = oTris[i]; }
        if (qs == 6)
        {
            //nowTris = new int[] { 0, 1, 4, 4, 3, 0,  0, 2, 5, 5, 3, 0,};
            nowTris = new int[] {
                0, 1, 4, 4, 3, 0,  0, 3, 5, 5, 2, 0,
                0,3,4,4,1,0, 0,2,5,5,3,0,
             };
        }
        else
        {
            for (int i = 0; i < 24; i++) { nowTris[tl + i] = nowTris[tl + i - 24] + 3; }
        }
        return nowTris;
    }

}
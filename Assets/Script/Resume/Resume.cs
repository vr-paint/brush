using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEditor;

public class Resume : MonoBehaviour
{
    public Vector3[] ResumeVector;
    public int[] ResumeI;
    public int[] ResumeJ;
    public int RegisterCounter = 0;
    public int ResumeLength;
    public int[] Split;

    public void get() {
         ResumeLength = GameObject.Find("KDTree").transform.childCount;

        Array.Resize(ref ResumeVector, 0);
        Array.Resize(ref ResumeI, 0);
        Array.Resize(ref ResumeJ, 0);
        RegisterCounter = 0;
        Array.Resize(ref Split, 0);
        for (int i = 0; i < ResumeLength; i++)
        {
            GameObject CopyTransformObject = GameObject.Find("TEST" + i);
            Array.Resize(ref Split, Split.Length + 1);
            for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {
                Array.Resize(ref ResumeVector, ResumeVector.Length + 1);
                Array.Resize(ref ResumeI, ResumeI.Length + 1);
                Array.Resize(ref ResumeJ, ResumeJ.Length + 1);
            }
        }
        
        for (int i = 0; i < ResumeLength; i++) {
            GameObject CopyTransformObject = GameObject.Find("TEST" + i);
            for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++) {

                ResumeVector[RegisterCounter] = CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices[j];
                ResumeI[RegisterCounter]=i;
                ResumeJ[RegisterCounter]=j;
                RegisterCounter++;
            }
            Split[i] = RegisterCounter;
        }
        
    

    }
    void Resize() { }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject RecoverLine;
    public Mesh dMesh;
    Vector3[] Register;
    public void RecoverLineRender()
    {
       
        int Counter = 0;
        for (int i = 0; i < ResumeLength; i++)
        {
            
            RecoverLine = new GameObject();
            RecoverLine.AddComponent<MeshFilter>();
            RecoverLine.AddComponent<MeshRenderer>();
            dMesh =RecoverLine.GetComponent<MeshFilter>().mesh;
            Debug.Log(Split[i]);
            for (int j = Counter; j < Split[i]; j++)
            {
                Register = new Vector3[Split[i]];
                //  dMesh[j] =;
                Register[j] = ResumeVector[j];
                Counter++;
            }
            dMesh.vertices = Register;
            int[] nowTris = new int[] {
                0, 1, 4, 4, 3, 0,  0, 3, 5, 5, 2, 0,
                0,3,4,4,1,0, 0,2,5,5,3,0,
             };
            dMesh.triangles = nowTris;
            RecoverLine.name = "TEST" +"_"+i;
        }


    }
        
}


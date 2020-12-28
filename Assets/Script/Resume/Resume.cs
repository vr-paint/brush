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
    public Material[] ResumeMaterial;
    public void DeleteLastLineRecover(int A) {//預計，這是刪除最後一條的還原功能

        ResumeLength = 1;
        RegisterCounter = 0;
        Array.Resize(ref Split, 1);
        Array.Resize(ref ResumeMaterial, 1);
        Array.Resize(ref ResumeVector, 0);
        Array.Resize(ref ResumeI, 0);
        Array.Resize(ref ResumeJ, 0);

        GameObject CopyTransformObject = GameObject.Find("TEST" + A);
        for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
        {
            Array.Resize(ref ResumeVector, ResumeVector.Length + 1);
            Array.Resize(ref ResumeI, ResumeI.Length + 1);
            Array.Resize(ref ResumeJ, ResumeJ.Length + 1);
        }
        for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
        {
            ResumeVector[RegisterCounter] = CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices[j];
            ResumeI[RegisterCounter] = A;
            ResumeJ[RegisterCounter] = j;
            RegisterCounter++;
        }
        Split[0] = RegisterCounter;
        ResumeMaterial[0] = CopyTransformObject.GetComponent<GraphicsLineRenderer>().Imat;
    }
    public void NotTouchGet() {
        GameObject.Find("LeftHand").GetComponent<UIIcon>().onlyOneTime=false;
    }
    public void get()
    {
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
            Array.Resize(ref ResumeMaterial, ResumeMaterial.Length + 1);
            for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {
                Array.Resize(ref ResumeVector, ResumeVector.Length + 1);
                Array.Resize(ref ResumeI, ResumeI.Length + 1);
                Array.Resize(ref ResumeJ, ResumeJ.Length + 1);
            }

        }

        for (int i = 0; i < ResumeLength; i++)
        {
            GameObject CopyTransformObject = GameObject.Find("TEST" + i);
            for (int j = 0; j < CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {

                ResumeVector[RegisterCounter] = CopyTransformObject.GetComponent<GraphicsLineRenderer>().vertices[j];
                ResumeI[RegisterCounter] = i;
                ResumeJ[RegisterCounter] = j;
                RegisterCounter++;
            }
            Split[i] = RegisterCounter;
            ResumeMaterial[i] = CopyTransformObject.GetComponent<GraphicsLineRenderer>().Imat;
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
        int[] nowTris = new int[] {
                0,1,4, 4,3,0, 0,3,5, 5,2,0,
                0,3,4, 4,1,0, 0,2,5, 5,3,0,
             };

        int Counter = 0;
        for (int i = 0; i < ResumeLength; i++)
        {
            RecoverLine = new GameObject();
            RecoverLine.AddComponent<GraphicsLineRenderer>();
            //RecoverLine.AddComponent<MeshFilter>();
            //RecoverLine.AddComponent<MeshRenderer>();
            RecoverLine.GetComponent<MeshRenderer>().material = ResumeMaterial[i];
            RecoverLine.GetComponent <GraphicsLineRenderer>().Imat = ResumeMaterial[i];
         
            RecoverLine.AddComponent<Rigidbody>().useGravity = false;
            RecoverLine.AddComponent<MeshCollider>().convex = true;
            RecoverLine.AddComponent<MeshColiderAndSphere>();
            dMesh = RecoverLine.GetComponent<MeshFilter>().mesh;
            int Counter2 = Counter;
            if (i == 0)
            {
                Register = new Vector3[Split[i]];
                for (int j = Counter; j < Split[i]; j++)
                {
                    Register[j - Counter2] = ResumeVector[j];
                    Counter++;
                }
            }
            else
            {
                int Test = Split[i] - Split[i - 1];
                Register = new Vector3[Test];
                for (int j = Counter; j < Split[i]; j++)
                {
                    Register[j - Counter2] = ResumeVector[j];
                    Counter++;
                }
            }
            dMesh.vertices = Register;
            RecoverLine.GetComponent<GraphicsLineRenderer>().vertices = dMesh.vertices;

            int[] nowTris_2;
            if (i == 0) { nowTris_2 = new int[(Split[i] / 3 - 1) * 24]; }
            else { nowTris_2 = new int[((Split[i] - Split[i - 1]) / 3 - 1) * 24]; }
            //(/3-1)*24 //TRIS的個數
            for (int j = 0; j < 24; j++)
            {
                nowTris_2[j] = nowTris[j];
            }
            for (int j = 24; j < nowTris_2.Length; j++)
            {
                nowTris_2[j] = nowTris_2[j - 24] + 3;
            }
            //nowTris[tl + i] = nowTris[tl + i - 24] + 3; //TRIS的邏輯
            dMesh.triangles = nowTris_2;

            RecoverLine.name = "TEST" + i;

        }
        ResetDrawLine(ResumeLength);
        ResetGameObject(ResumeLength);
        ResetKDtree(ResumeLength);
    }
    public void ResetDrawLine(int i)
    {
        GameObject.Find("RightHand").GetComponent<DrawLine>().i = i;//最後要重設的東西之一
        for (int j = 0; j < i; j++)
        {
            Array.Resize(ref GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine, GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine.Length + 1);
        }
        for (int j = 0; j < i; j++)
        {
            GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine[j] = GameObject.Find("TEST" + j);
        }
    }
    public void ResetGameObject(int i)
    {

        for (int j = 0; j < i; j++)
        {
            GameObject KDTreeInstance = new GameObject();
            KDTreeInstance.name = "KDtree" + j;//這是第i條的彩帶
            KDTreeInstance.transform.parent = GameObject.Find("KDTree").transform;
            GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeAdd(j);
        }

        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().Closet = GameObject.Find("TEST" + (i-1));
        Array.Resize(ref GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().MyDrawLine,i);

    }
    public void ResetKDtree(int i)
    {
        if (i == 1) {
            GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeReSetResume();
        }
        else
        {
            GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeReSet();
        }
    }
}


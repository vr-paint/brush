using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDtreeBlender : MonoBehaviour
{
   
    private Vector3 LineVerticesPos1, LineVerticesPos2;
    
    public void LineVertices(Vector3 EndPos1, Vector3 EndPos2)
    {
        Closet = GameObject.Find("RightHand").GetComponent<DrawLine>().DrawLineTest;

        LineVerticesPos1 = EndPos1;
        LineVerticesPos2 = EndPos2;
        if (GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine.Length == 1) { }
        else
        {
            find(LineVerticesPos1, LineVerticesPos2);
        }
    }
    public GameObject Closet;

    public void find(Vector3 LVPOS1, Vector3 LVPOS2)
    {
        //Debug.Log("Test");
        if (Vector3.Distance(LVPOS1, BlenderKdtree.FindClosest(LVPOS1).transform.position) < 0.05)
        {
            //Closet.GetComponent<MeshFilter>().mesh.vertices;
            Vector3[] vertices = Closet.GetComponent<MeshFilter>().mesh.vertices;
            vertices[vertices.Length - 2] = BlenderKdtree.FindClosest(LVPOS1).transform.position;
            Closet.GetComponent<MeshFilter>().mesh.vertices = vertices;
            //LineInBox[oldLine].gameObject.GetComponent<MeshFilter>().mesh.vertices = LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices;
        }
        else {// Debug.Log("GOOD"); 
        }
        if (Vector3.Distance(LVPOS2, BlenderKdtree.FindClosest(LVPOS2).transform.position) < 0.05)
        {
            Vector3[] vertices = Closet.GetComponent<MeshFilter>().mesh.vertices;
            vertices[vertices.Length - 1] = BlenderKdtree.FindClosest(LVPOS2).transform.position;
            Closet.GetComponent<MeshFilter>().mesh.vertices = vertices;
        }
        else {// Debug.Log("Bad"); 
            }
    }



    public void Kdtree()
    {
        LineInBox = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;

        List<Transform> RegisterList = new List<Transform>();
        //建樹過程
        for (int i = LineInBox.Length - 1; i < LineInBox.Length; i++)
        {
            int setJ = 0;
            for (int j = 0; j < LineInBox[i].GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {
                if (j % 3 == 0) continue;

                GameObject KdtreeGameObject = new GameObject();
                KdtreeGameObject.transform.parent = GameObject.Find("KDtree" + i).transform;
                KdtreeGameObject.name = ("KDtree" + i + "_" + setJ);
                KdtreeGameObject.AddComponent<SetArray>();
                KdtreeGameObject.GetComponent<SetArray>().SetI(i);
                KdtreeGameObject.GetComponent<SetArray>().SetJ(j);
                float _x = LineInBox[i].GetComponent<GraphicsLineRenderer>().vertices[j].x;
                float _y = LineInBox[i].GetComponent<GraphicsLineRenderer>().vertices[j].y;
                float _z = LineInBox[i].GetComponent<GraphicsLineRenderer>().vertices[j].z;
                KdtreeGameObject.transform.position = new Vector3(_x, _y, _z);
                RegisterList.Add(KdtreeGameObject.transform);
                setJ++;
            }
        }
        BlenderKdtree = new KdTree<Transform>();
        BlenderKdtree.AddAll(RegisterList);
        //以上是建樹
    }

    public GameObject[] LineInBox;
    public KdTree<Transform> BlenderKdtree;
    public void Blender()
    {
        Debug.Log("開始");


        //建樹
        Kdtree();
        //建樹
        //以下是查詢
        LineInBox = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;

        Vector3[] NewLine = LineInBox[LineInBox.Length - 1].gameObject.GetComponent<GraphicsLineRenderer>().vertices;
        //GameObject now = BlenderKdtree.FindClosest(new Vector3(0,0,0)).gameObject;
        float check = 999;
        int Register_i = 0;
        for (int i = 0; i < NewLine.Length; i++)
        {
            GameObject now1 = BlenderKdtree.FindClosest(NewLine[i]).gameObject;
            int oldLine_I = now1.GetComponent<SetArray>().I;
            int oldLine_J = now1.GetComponent<SetArray>().J;
            float BoxDist = Vector3.Distance(now1.transform.position, NewLine[i]);
            if (BoxDist > 0.05) { continue; }
            else
            {
                if (check > BoxDist)
                {
                    check = BoxDist;
                    Register_i = i;
                }
                if (check == 999)
                {
                    continue;
                }
            }
        }
        //以上是查詢
        //        print(now.transform.position);


    }
    public GameObject FindClose(Vector3 FindPos)
    {
        GameObject EndPos = BlenderKdtree.FindClosest(FindPos).gameObject;
        return EndPos;
    }

    public int AAA = 0;
}

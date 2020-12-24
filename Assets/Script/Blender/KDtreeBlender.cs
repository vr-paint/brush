using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class KDtreeBlender : MonoBehaviour
{

    private Vector3 LineVerticesPos1, LineVerticesPos2;
    KDtreeBlender()
    {
        BlenderKdtree = new KdTree<Transform>();
    }
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

    public void ResetMydrawLine() {
        MyDrawLine = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;
        Array.Resize(ref MyDrawLine, MyDrawLine.Length);
    }
    public void KdtreeReSet()
    {
        BlenderKdtree = new KdTree<Transform>();
        List<Transform> RegisterList = new List<Transform>();
        for (int i = 0;i<GameObject.Find("KDTree").transform.childCount-1 ; i++) {
            for (int j = 0;j<GameObject.Find("KDtree"+i).transform.childCount ; j++) {
                RegisterList.Add(GameObject.Find("KDtree" + i).transform.GetChild(j));
            }
        }
        BlenderKdtree.AddAll(RegisterList);
    }

    public void KdtreeReBuildAll()
    {
        MyDrawLine = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;//右邊是別的檔案裡定義的
        List<Transform> RegisterList = new List<Transform>();
        for (int i=0; i<MyDrawLine.Length-1; i++)
        {
            for (int j = 0; j < MyDrawLine[i].GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {   
                if (j % 3 == 0) continue;
                GameObject KdtreeGameObject = GameObject.Find("KDtree" + i + "_" + j);
                RegisterList.Add(KdtreeGameObject.transform);
            }
        }
        BlenderKdtree = new KdTree<Transform>();
        BlenderKdtree.AddAll(RegisterList);
    }
    public void KdtreeAdd(int i)
    {//左邊是這個檔案裡的 MyDrawLine 其實是同一個
        MyDrawLine = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;//右邊是別的檔案裡定義的
        
        List<Transform> RegisterList = new List<Transform>();
        //建樹過程

        //if(MyDrawLine.Length>=1){
        //    int i = MyDrawLine.Length - 1;
        //for (int i = LineInBox.Length - 1; i < LineInBox.Length; i++)
        //for (int i=0; i<MyDrawLine.Length-1; i++)//可能會變很慢
        if(i>=0){//TODO: 這裡有magic魔法/魔術, 有鬼!
            
            //int setJ = 0;
            for (int j = 0; j < MyDrawLine[i].GetComponent<GraphicsLineRenderer>().vertices.Length; j++)
            {
                if (j % 3 == 0) continue;

                GameObject KdtreeGameObject = new GameObject();//這是建出 Kdtree 裡面的小朋友 (KDtree0_1 之類的)
                KdtreeGameObject.transform.parent = GameObject.Find("KDtree" + i).transform;//把小朋友的爸爸設好
                KdtreeGameObject.name = ("KDtree" + i + "_" + j);
                KdtreeGameObject.AddComponent<SetArray>();
                KdtreeGameObject.GetComponent<SetArray>().SetI(i);
                KdtreeGameObject.GetComponent<SetArray>().SetJ(j);
                float _x = MyDrawLine[i].GetComponent<GraphicsLineRenderer>().vertices[j].x;
                float _y = MyDrawLine[i].GetComponent<GraphicsLineRenderer>().vertices[j].y;
                float _z = MyDrawLine[i].GetComponent<GraphicsLineRenderer>().vertices[j].z;
                KdtreeGameObject.transform.position = new Vector3(_x, _y, _z);
                RegisterList.Add(KdtreeGameObject.transform);
                //SetJ++;
            }
        }
        //BlenderKdtree = new KdTree<Transform>();不能在這裡新建 KdTree資料結構,會清空。應該持續用同一個舊的 tree
        BlenderKdtree.AddAll(RegisterList);
        //以上是建樹
    }

    public GameObject[] MyDrawLine;
    public KdTree<Transform> BlenderKdtree;
    public void Blender()
    {
        Debug.Log("開始");


        //建樹
        //Kdtree();
        //建樹
        //以下是查詢
        MyDrawLine = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;

        Vector3[] NewLine = MyDrawLine[MyDrawLine.Length - 1].gameObject.GetComponent<GraphicsLineRenderer>().vertices;
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

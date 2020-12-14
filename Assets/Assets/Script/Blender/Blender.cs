using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    public GraphicsLineRenderer[] Blenderneed;

    public List<Vector3[]> BoxLine = new List<Vector3[]>();
    public List<List<int>> UsedLine = new List<List<int>>();
    public Vector3[] PositionSaveLine;
   
    void Start()
    {
        PositionSaveLine = new Vector3[1];
    }
    public void MyDrawLineADD()//增加陣列大小
    {
        Array.Resize(ref PositionSaveLine, PositionSaveLine.Length + 1);
    }
    public void GetLinePosition(int FindLine)
    {
        GameObject BlendeObject = GameObject.Find("TEST" + FindLine);
        Vector3[] test = BlendeObject.GetComponent<GraphicsLineRenderer>().vertices;
        BoxLine.Add(test);
        List<int> emptyline = new List<int>(test.Length);
        for (int i = 0; i < test.Length; i++) emptyline.Add(0);
        UsedLine.Add(emptyline);
        
            for (int oldLine = 0; oldLine < BoxLine.Count - 1; oldLine++)
            {

                int newLine = BoxLine.Count - 1;
                for (int i = 1; i < BoxLine[oldLine].Length; i++)
                {   if (i % 3 == 0) continue;
                   
                    float check = 999;//設立一個大數字，來讓符合條件的進行比對

                    int Register_i = 0, Register_j = 0;

                    for (int j = 1; j < BoxLine[newLine].Length; j++)
                    {   if (j % 3 == 0) continue;
                      
                        float BoxDist = Vector3.Distance(BoxLine[oldLine][i], BoxLine[newLine][j]);
                        if (BoxDist > 0.09){  continue; }
                        if (BoxDist < check &&  UsedLine[newLine][j]==0)
                        {//如果距離小於大數字，則進行大數字更新 
                            check = BoxDist; //將大數字等於目前距離
                            Register_i = i; Register_j = j;//將目前的點，紀錄。
                        }
                       
                        if (check == 999) { continue; }
                        //OperSubBlend(oldLine, newLine, Register_i, Register_j);
                       
                    }
                    if (check > 0.09) { continue;  }
                    OperSubBlend(oldLine, newLine, Register_i, Register_j);
                }
    
            }
            

    }
    
    void OperSubBlend(int oldL, int newL, int R_I, int R_J)
    {

        
        GameObject oldLiObject = GameObject.Find("TEST" + oldL);
        GameObject newLjObject = GameObject.Find("TEST" + newL);
        Vector3 sv = (BoxLine[oldL][R_I] + BoxLine[newL][R_J]) / 2;
        BoxLine[oldL][R_I] = sv; BoxLine[newL][R_J] = sv;
        UsedLine[oldL][R_I] = 1;
        UsedLine[newL][R_J] = 1;
        oldLiObject.GetComponent<MeshFilter>().mesh.vertices = BoxLine[oldL];
        newLjObject.GetComponent<MeshFilter>().mesh.vertices = BoxLine[newL];

        oldLiObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        newLjObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KdTree;

public class BlenderVersion2 : MonoBehaviour
{

    public GameObject[] LineInBox;
    public void Blender()
    {

        LineInBox = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;

        //LineInBox儲存的陣列
        if (LineInBox.Length > 1)
        {
            for (int oldLine = 0; oldLine < LineInBox.Length - 1; oldLine++)
            {
                int NewLine = LineInBox.Length - 1;
                int OldLineLength=LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices.Length;
                for (int i = 0; i <OldLineLength ; i++)
                {
                    if (i % 3 == 0) continue;
                    if (LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[i] == Vector3.zero) continue;
                    float check = 999;
                    int Register_i = 0, Register_j = 0;
                    int NewLineLength= LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices.Length;
                    for (int j = 0; j <NewLineLength; j++)
                    {
                       bool Check= CheckMate(i,NewLineLength,oldLine,NewLine);
                        if(Check==false)continue;
                        if (j % 3 == 0) continue;
                     
                        if (LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[j] == Vector3.zero) continue;
                        float BoxDist = Vector3.Distance(LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[i], LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[j]);
                        if (BoxDist > 0.05) { continue; }
                        if (check > BoxDist)
                        {
                            check = BoxDist;
                            Register_i = i;
                            Register_j = j;
                        }
                        if (check == 999)
                        {
                            continue;
                        }
                    }
                    if (check > 0.05) continue;
                    LineInBoxBlender(oldLine, NewLine, Register_i, Register_j);
                }
            }
        }
    }
    bool CheckMate(int i,int NewLineLength,int oldLine,int NewLine){
      
            for(int y=0;y<NewLineLength;y++){
                if(LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[i]==
                LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[y]
                )
                {
                    return false;
                }
            }
        
        return true;
    }
        void LineInBoxBlender(int oldLine, int NewLine, int i, int j)
    {
        Vector3 sv = (LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[i] + LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[j]) / 2;

        LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[i] = sv;
        LineInBox[oldLine].gameObject.GetComponent<MeshFilter>().mesh.vertices = LineInBox[oldLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices;
        LineInBox[oldLine].gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();

        LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices[j] = sv;
        LineInBox[NewLine].gameObject.GetComponent<MeshFilter>().mesh.vertices = LineInBox[NewLine].gameObject.GetComponent<GraphicsLineRenderer>().vertices;
        LineInBox[NewLine].gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();

    }
}
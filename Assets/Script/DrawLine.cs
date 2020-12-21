using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEditor;
public class DrawLine : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabPinch;  //板機鍵按鈕
    public SteamVR_Action_Boolean GrapGrip;  //Grip鍵  
    public SteamVR_Action_Boolean SnapTurnLeft;//觸控板左鍵
    public SteamVR_Action_Boolean SnapTurnRight;//觸控板右鍵
    public SteamVR_Action_Boolean Teleport;
    public SteamVR_Action_Single Squeeze;
    private SteamVR_Behaviour_Pose Pose;
    public GraphicsLineRenderer draw;
    public Boxwidth Box;
    public int N = 0;
    public int i = 0;
    public int colorchangecount = 0;
    public Vector3 older; public Vector3 now;
    public float LineWidth = .06f;//0.07
    public GameObject DrawLineTest;
    public GameObject[] MyDrawLine;
    public GameObject BlenderScript;
    private Material gObjectMaterial;

    bool end = false;
    void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    void Start()
    {

 
        MyDrawLine = new GameObject[1];
        BlenderScript = GameObject.Find("BlenderEmpty");
    }
    public void RestartArray(int a, bool Button)
    {
        if (Button == false) { }
        else
        {
            for (int ReArray = a; ReArray < MyDrawLine.Length - 1; ReArray++)
            {
                MyDrawLine[ReArray] = MyDrawLine[ReArray + 1];
            }
            Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);
            if (MyDrawLine.Length <= 3)
            {
                for (int ReArray = a; ReArray < MyDrawLine.Length - 1; ReArray++)
                {
                    MyDrawLine[ReArray].name = "TEST" + ReArray;
                }
            }
            else
            {
                for (int ReArray = a; ReArray < MyDrawLine.Length - 1; ReArray++)
                {
                    MyDrawLine[ReArray].name = "TEST" + ReArray;
                }
            }
            for (int Rename = a + 1; a < MyDrawLine.Length - 1; Rename++)
            {
                if (GameObject.Find("KDtree" + Rename) == null) { break; }
                GameObject.Find("KDtree" + Rename).name = "KDtree" + (Rename - 1);
            }
            for (int Rename = 0; Rename < MyDrawLine.Length; Rename++)
            {
                if (GameObject.Find("KDtree0") == null)
                {
                    break;
                }
               if (GameObject.Find("KDtree" + Rename) == null)
                {
                    break;
                }
            }
            i = MyDrawLine.Length - 1;
            if (i < 0) i = 0;
            Restart();
        }
    }
    void Restart(){

        GameObject RestartReady=GameObject.Find("KDTree");
for(int ReArray=0;ReArray<RestartReady.transform.childCount;ReArray++)
{
  
   for(int ReChildArray=0;ReChildArray<RestartReady.transform.GetChild(ReArray).transform.childCount;ReChildArray++){
    RestartReady.transform.GetChild(ReArray).transform.GetChild(ReChildArray).name=RestartReady.transform.GetChild(ReArray).name+"_"+ReChildArray;
    string[] stringSeparators = new string[] { "KDtree" };
    string[] result = RestartReady.transform.GetChild(ReArray).name.Split(stringSeparators, StringSplitOptions.None);
    int a = int.Parse(result[1]);
    RestartReady.transform.GetChild(ReArray).transform.GetChild(ReChildArray).GetComponent<SetArray>().SetI(a);
   }
  
}

    }

    public void MyDrawLess()
    {
        int y = MyDrawLine.Length-1;
        for (int x = 0; x < y; x++)
        {
            Destroy(MyDrawLine[i - 1]);
            Destroy(GameObject.Find("KDtree"+x));
            Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);
            if (MyDrawLine.Length == 0) { Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1); }
            i--;
        }
    }
     public void MyDrawLessONE()
     {
        if(MyDrawLine.Length>1){
          Destroy(MyDrawLine[i-1]);
        }
        Destroy(GameObject.Find("KDtree"+(i-1)));
        Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);
        if (MyDrawLine.Length == 0)
        {
            Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1);
        }
        i--;
        if(i<0) i=0;
        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().Closet = GameObject.Find("TEST"+(i-1));
        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().LineInBox = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;
        Array.Resize(ref GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().LineInBox, GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().LineInBox.Length -  1);
        BlenderScript.GetComponent<KDtreeBlender>().Kdtree();
    }
    public void MyDrawLineDestroy()
    {
        if (i > 0)
        {
            Debug.Log(i);
            MyDrawLess();
        }
    }

    public float Change;
    public float ChangeS =1.0f;
    public void ChangeColorRecive(float H)
    {
       
        Change = H;
    }
    public void ChangeColorRecive2(float H)
    {
        ChangeS = H;
    }
    void ChangeColor()
    {
        gObjectMaterial = new Material(Shader.Find("Standard"));
        gObjectMaterial.color = Color.HSVToRGB(Change, ChangeS, 1.0f);
    }
    public bool Nodelete = false; 
    void Update()
    {

        now = Pose.transform.position;
        float dist = Vector3.Distance(older, now);
        if (Squeeze.GetAxis(Pose.inputSource)>0.1f &&end == false)
        {
            if (GameObject.Find("LeftHand").GetComponent<UIIcon>().Eraser.activeSelf == true)
            {
                Nodelete = true;
            }

            end = true;
            DrawLineTest = new GameObject();
            DrawLineTest.AddComponent<MeshFilter>();
            DrawLineTest.AddComponent<MeshRenderer>();
            DrawLineTest.AddComponent<Rigidbody>().useGravity = false;
            DrawLineTest.AddComponent<MeshCollider>().convex = true;
            DrawLineTest.AddComponent<MeshColiderAndSphere>();
            DrawLineTest.transform.gameObject.tag = "test";
            //DrawLineTest.GetComponent<MeshCollider>().isTrigger = true;
            draw = DrawLineTest.AddComponent<GraphicsLineRenderer>();
          
            ChangeColor();
            draw.Imat = gObjectMaterial;
            draw.setVectorCross(Pose.transform.up, Pose.transform.forward);
            draw.setwidth(LineWidth);
            draw.AddPoint(now);
            

            DrawLineTest.name = "TEST" + i;

            older = now;
        }
        else if (Squeeze.GetAxis(Pose.inputSource) > 0.1)
        {
            if (dist > 0.03)
            {
                
                draw.ErrorCheck = true;
                draw.setwidth2(Squeeze.GetAxis(Pose.inputSource) );
                draw.setVectorCross(Pose.transform.up, Pose.transform.forward);
                draw.AddPoint(now);
                older = now;
            }

        }
        if (Squeeze.GetAxis(Pose.inputSource) <0.1f && end == true)
        {
            Nodelete = false;
            if (MyDrawLine.Length < 1) { }
            else
            {
                MyDrawLine[i] = GameObject.Find("TEST" + i);
                bool DrawLineCheck = MyDrawLine[i].GetComponent<GraphicsLineRenderer>().Check();
                if (DrawLineCheck == true)
                {

                }
                else
                {
                 KdTreeInstanceFunction();
                }
                //Debug.Log(i);
                end = false;
            }
        }






        //if (Squeeze.GetActive(Pose.inputSource)) {
        // Debug.Log("Squeeze:"+Squeeze.GetAxis(Pose.inputSource));
        //}


        /*
        if (SnapTurnRight.GetStateDown(Pose.inputSource))
        {
            if (LineWidth > 0.1f) LineWidth = 0.09f;
            LineWidth *= 1.2f;
            Box.BoxControlWidth(LineWidth);
            //Debug.Log(LineWidth);
            //Debug.Log("Yes:" + LineWidth);
        }
        else if (SnapTurnLeft.GetStateDown(Pose.inputSource))
        {
            if (LineWidth < 0.02f) LineWidth = 0.02f;
            LineWidth *= 0.8f;
            //Debug.Log("Yes:" + LineWidth);
            Box.BoxControlWidth(LineWidth);
            //Debug.Log(LineWidth);

        }*/

    }
   public void KdTreeInstanceFunction(){
   GameObject KDTreeInstance = new GameObject();
                    KDTreeInstance.name = "KDtree" + i;
                    KDTreeInstance.transform.parent = GameObject.Find("KDTree").transform;
                    BlenderScript.GetComponent<KDtreeBlender>().Kdtree();
                    Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1);
                    i++;
}
}

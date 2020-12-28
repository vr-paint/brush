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
            if (MyDrawLine.Length > 1)
            {

                Destroy(MyDrawLine[a]);
                RestartDrawLine(a);
                Destroy(GameObject.Find("KDtree" + a));
                RestartKDtree(a);
                i = MyDrawLine.Length - 1;
                GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().Closet = GameObject.Find("TEST" + (i - 1));
                GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeReSet();


            }
        }
    }
    public void RestartDrawLine(int _a)
    {
        for (int a = _a; a < MyDrawLine.Length - 1; a++)
        {
            MyDrawLine[a].name = "TEST" + (a - 1);
        }
        for (int a = _a; a < MyDrawLine.Length - 1; a++)
        {
            MyDrawLine[a] = MyDrawLine[a + 1];
        }
        Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);
        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().ResetMydrawLine();
    }
    public void RestartKDtree(int _a)
    {
        for (int a = _a; a < GameObject.Find("KDTree").transform.childCount; a++)
        {
            GameObject.Find("KDTree").transform.GetChild(a).name = "KDtree" + (a - 1);
        }
        for (int a = _a; a < GameObject.Find("KDTree").transform.childCount - 1; a++)
        {
            for (int b = 0; b < GameObject.Find("KDtree" + a).transform.childCount; b++)
            {
                GameObject.Find("KDtree" + a).transform.GetChild(b).GetComponent<SetArray>().I = a;
                int J = GameObject.Find("KDtree" + a).transform.GetChild(b).GetComponent<SetArray>().J;
                GameObject.Find("KDtree" + a).transform.GetChild(b).name = "KDtree" + a + "_" + J;
            }
        }
    }

    void Restart()
    {
    }

    public void MyDrawLess()
    {   //TODO: 因為 MyDrawLine比它的長度再多1,所以只好從 -2開始往回刪
        GameObject.Find("Resume").GetComponent<Resume>().get();
        for (int i = MyDrawLine.Length - 2; i >= 0; i--)//從右往左刪
        {
            Destroy(MyDrawLine[i]);
            Destroy(GameObject.Find("KDtree" + i));
          
        }
        i = 0;//TODO: 這裡之後要重構,global變數名稱不應該用local可能會用到的名字
        Array.Resize(ref MyDrawLine, 1);//這裡不確定會不會要留1 or 留0, 有個奇怪的地雷,使得陣列要比使用的+1
        GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeReBuildAll();
        //MyDrawLine會比 KDtree多1, 也會比完整的 Line 多1。因為它要用來多放1個未來正在畫到一半的線

        //TODO: 要找出這個預存的地雷
        //int y = MyDrawLine.Length-1;
        //for (int x = 0; x < y; x++)
        //{
        //    Destroy(MyDrawLine[i - 1]);
        //    Destroy(GameObject.Find("KDtree"+x));
        //    Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);
        //    if (MyDrawLine.Length == 0) { Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1); }
        //    i--;
        //}
    }
  
    public void MyDrawLessONE()
    {
        if (MyDrawLine.Length > 1)
        {//MyDrawLine會比 KDtree多1, 也會比完整的 Line 多1。因為它要用來多放1個未來正在畫到一半的線
            Destroy(MyDrawLine[i - 1]);//這個和前面 MyDrawLess()很像
            Destroy(GameObject.Find("KDtree" + (i - 1)));//這個和前面 MyDrawLess()很像, 爸爸死掉,裡面的小朋友跟著死掉 (當初是在 KDtreeBlender.cs裡面新增的)
            Array.Resize(ref MyDrawLine, MyDrawLine.Length - 1);//這個和前面 MyDrawLess()很像, Q: 要留1個嗎?
            i--;
            //{
            //    Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1);//TODO: 這就是之前提過的奇怪的地雷
            //}
            //if (i<0) i=0;
            GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().Closet = GameObject.Find("TEST" + (i - 1));//Closet這個單字之後要重構新名字
                                                                                                                     //GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().MyDrawLine = GameObject.Find("RightHand").GetComponent<DrawLine>().MyDrawLine;
                                                                                                                     //jsyeh: 可能會用? 先不要減 
            Array.Resize(ref GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().MyDrawLine, GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().MyDrawLine.Length - 1);
            // BlenderScript.GetComponent<KDtreeBlender>().Kdtree();//Q: 為什麼刪掉時,要建 KDtree() ?

            GameObject.Find("BlenderEmpty").GetComponent<KDtreeBlender>().KdtreeReBuildAll();
        }

    }
    public void MyDrawLineDestroy()
    {
        if (i > 0)
        {
            MyDrawLess();
        }
    }

    public float Change;
    public float ChangeS = 1.0f;
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
        if (Squeeze.GetAxis(Pose.inputSource) > 0.1f && end == false)
        {
            if (GameObject.Find("LeftHand").GetComponent<UIIcon>().Eraser.activeSelf == true)
            {
                Nodelete = true;//TODO:這可能是個地雷,需要改名 & 想一下是不是全部考慮到
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
                draw.setwidth2(Squeeze.GetAxis(Pose.inputSource));
                draw.setVectorCross(Pose.transform.up, Pose.transform.forward);
                draw.AddPoint(now);
                older = now;
            }

        }
        if (Squeeze.GetAxis(Pose.inputSource) < 0.1f && end == true)//TODO: 加一下註解, 這個 if()是什麼意思
        {
            Nodelete = false;//TODO:這可能是個地雷,需要改名 & 想一下是不是全部考慮到
            if (MyDrawLine.Length < 1) { }
            else
            {//這塊其實是 myDrawOneMore() 或 myDrawAddOne() 之類的名字!!!
                MyDrawLine[i] = GameObject.Find("TEST" + i);
                bool DrawLineCheck = MyDrawLine[i].GetComponent<GraphicsLineRenderer>().Check();//用來解決空按的問題, 它的意思 有空物件,刪掉了哦!
                if (DrawLineCheck == true)//TODO: 這個名字 DrawLineCheck 及前一行的 Check() 名字取得不好, 無法馬上知道它的意思,if()就很難想懂
                {

                }
                else
                {//沒有空物件時,才建 KdTree 的爸爸 empty GameObject的transform, transform拿來用
                    KdTreeInstanceFunction();//TODO: 名字看不懂,會忘記的名字,就要改成不會忘記/有意義的名字
                }
                //Debug.Log(i);
                end = false;//TODO: 名字取得不好, 所以別人不好理解
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
    public void KdTreeInstanceFunction()
    {
        GameObject KDTreeInstance = new GameObject();
        KDTreeInstance.name = "KDtree" + i;//這是第i條的彩帶
        KDTreeInstance.transform.parent = GameObject.Find("KDTree").transform;
        BlenderScript.GetComponent<KDtreeBlender>().KdtreeAdd(i);//Q: 這是建 Kdtree Q:是建1條? 還是建全部? A: 這是建第i條的 Kdtree_i 及裡面的小朋友
        Array.Resize(ref MyDrawLine, MyDrawLine.Length + 1);
        GameObject.Find("Resume").GetComponent<Resume>().NotTouchGet();
        i++;

    }
}
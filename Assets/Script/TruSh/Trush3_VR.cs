using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;
public class Trush3_VR : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabPinch;  //板機鍵按鈕
    public SteamVR_Action_Boolean GrapGrip;  //Grip鍵  
    public SteamVR_Action_Boolean Teleport;
    public SteamVR_Action_Vector2 TouchPad;
    private SteamVR_Behaviour_Pose Pose;

   // public SteamVR_TrackedObject trackdeObjec;  
  
    public Vector2 Axis;
    public float pixelHeight = 200, pixelWidth = 200;
    private float angle;
    public Color color_test;
    public Material mat;
    public GameObject controller_circle,Quad_c;
  

    void Awake(){
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        Quad_c = GameObject.Find("Quad_c");


        mat = new Material(Shader.Find("Sprites/Default"));
        mat = Quad_c.GetComponent<Renderer>().material;
    }
    void Start()
    {
     
       controller_circle = GameObject.Find("controller_circle");
     


    }
    
    void Update()
    {


        Axis = TouchPad.GetAxis(Pose.inputSource);
        if (TouchPad.GetAxis(Pose.inputSource)!=Vector2.zero){  
        
            Angle(); 
            
        }
       //TouchPad.GetAxis(Pose.inputSource);
       //print(Axis.x);
    }
    void Angle() {
       
        float angle = Mathf.Atan2(Axis.y, Axis.x);
        angle= (-angle+Mathf.PI/2)/(2*Mathf.PI);
        if(angle<0)angle+=1.0f;
        float h=angle;
        mat.color = Color.HSVToRGB(h, 1.0f, 1.0f);
      
    }

    Quaternion currentRotation;
    Vector3 currentEulerAngles;
  /*  void Controller(){//angle error can't 3D rotation
        float angle = Mathf.Atan2(Axis.y, Axis.x);
        float angle2 = angle * 180 / Mathf.PI - 90;
        currentEulerAngles = new Vector3(0,0, angle * 180 / Mathf.PI - 90);
        
        //print(currentEulerAngles);
        currentRotation.eulerAngles = currentEulerAngles;
        //print(currentRotation);
        controller_circle.transform.rotation= currentRotation;
        print(currentRotation);
 
    }*/
  


}
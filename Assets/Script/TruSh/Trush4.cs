using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;
public class Trush4 : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabPinch;  //板機鍵按鈕
    public SteamVR_Action_Boolean GrapGrip;  //Grip鍵  
    public SteamVR_Action_Vector2 TouchPad;
    private SteamVR_Behaviour_Pose Pose;

    public Vector2 Axis;
    //private float angle; //public Color color_test;
    public Material matc,matd;
    public GameObject controller_circle, Quad_c, Quad_d;

    public GameObject RadioSliderImage;
    void Awake()
    {
        Pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
        Quad_c = GameObject.Find("Quad_c");
        Quad_d = GameObject.Find("Quad_d");
       
        //mat = new Material(Shader.Find("Sprites/Default"));
        matc = Quad_c.GetComponent<Renderer>().material;
        matd= Quad_d.GetComponent<Renderer>().material;
        s = 1.0f;

    }
    void Start()
    {
        RadioSliderImage = GameObject.Find("RadioSliderImage");
    }
   public bool SliderSwitch = false;

    void Update()
    {
        if (SliderSwitch == true)
        {
            if (GrapGrip.GetStateDown(Pose.inputSource))
            {
                RadioSliderImage.SetActive(SliderSwitch);
                SliderSwitch = false;
            }
        }
        else {
            if (GrapGrip.GetStateDown(Pose.inputSource))
            {
                RadioSliderImage.SetActive(SliderSwitch);
                SliderSwitch = true;
            }
        }
        


        if (GameObject.Find("LeftHand").GetComponent<UIIcon>().BoolGrip == true) {
            h = GameObject.Find("RightHand").GetComponent<DrawLine>().Change;
           s= GameObject.Find("RightHand").GetComponent<DrawLine>().ChangeS;
            //  GameObject.Find("RadioSliderImage").SetActive(false);
            if (TouchPad.GetAxis(Pose.inputSource) != Vector2.zero)
            {
                Axis = TouchPad.GetAxis(Pose.inputSource);
                Angle();
            }

        }
        else
        {

            if (TouchPad.GetAxis(Pose.inputSource) != Vector2.zero)
            {

                Axis = TouchPad.GetAxis(Pose.inputSource);
                Angle2();
            }

          
        }


    }
    void Angle()
    {
     
        float angle = Mathf.Atan2(Axis.y, Axis.x);
        angle = (-angle + Mathf.PI / 2) / (2 * Mathf.PI);
        if (angle < 0) angle += 1.0f;
        h = angle;
        matc.color = Color.HSVToRGB(h, s, 1.0f);
        matd.color= Color.HSVToRGB(h, s, 1.0f);

        GameObject.Find("RightHand").GetComponent<DrawLine>().ChangeColorRecive(h);
        //matc.SetColor("_EMISSION",matc.color);
        //matc.EnableKeyword("_EMISSION");
        //matd.SetColor("_EMISSION", matd.color);
        //matc.EnableKeyword("_EMISSION");

    }
    public float h;
    public float s ;
    void Angle2()
    {
        float angle = Mathf.Atan2(Axis.y, Axis.x);
        angle = (-angle + Mathf.PI / 2) / (2 * Mathf.PI);
        if (angle < 0) angle += 1.0f;
        float s = angle;
        GameObject.Find("s_high").transform.localScale=new Vector3(0.1f,s,1) ;
       



        matc.color = Color.HSVToRGB(h, s, 1.0f);
        matd.color = Color.HSVToRGB(h, s, 1.0f);

        GameObject.Find("RightHand").GetComponent<DrawLine>().ChangeColorRecive2(s);
        //matc.SetColor("_EMISSION",matc.color);
        //matc.EnableKeyword("_EMISSION");
        //matd.SetColor("_EMISSION", matd.color);
        //matc.EnableKeyword("_EMISSION");

    }

    /*public void colorChange(int i) {
        RightHand = GameObject.Find("RightHand");
        int xx = RightHand.GetComponent<DrawLine>().i;
        RightHandMaterial = RightHand.GetComponent<GraphicsLineRenderer>().Imat;
        RightHandMaterial.color = matc.color;
    }
    */

}
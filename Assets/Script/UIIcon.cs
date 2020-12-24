using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


public class UIIcon : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabPinch;  //板機鍵按鈕
    public SteamVR_Action_Boolean GrapGrip;  //Grip鍵  
    public SteamVR_Action_Boolean SnapTurnLeft;//觸控板左鍵
    public SteamVR_Action_Boolean SnapTurnRight;//觸控板右鍵
    public SteamVR_Action_Boolean SnapTurnUp;
    public SteamVR_Action_Boolean SnapTurnDown;
    public SteamVR_Action_Boolean Teleport;
    private SteamVR_Behaviour_Pose Pose;
    public GameObject[] icon = new GameObject[4];
    public GameObject[] icon_explain = new GameObject[4];
    private int i = 0;
    private int j = 0;
    private SpriteRenderer Go1;
    private SpriteRenderer Go2;
    private bool IconSwitch_bool;
    private bool IconClick_bool = true;

    private GameObject TrueFalse;
    private GameObject ColorChoose;

    public GameObject teleport;
    public GameObject teleport2;
   // public GameObject sound_change;
    //public GameObject sound_click;
   // public GameObject sound_clear;
    public GameObject Eraser;
    public SpriteRenderer back_alphaColor;
    public DrawLine drawline;


    void Start()
    {
        TrueFalse = GameObject.Find("TrueFal");
        ColorChoose = GameObject.Find("ColorChoose");
        ColorChoose.gameObject.SetActive(false);
        icon[0].transform.gameObject.SetActive(true);
        icon[1].transform.gameObject.SetActive(false);
        icon[2].transform.gameObject.SetActive(false);
        icon[3].transform.gameObject.SetActive(false);
        icon_explain[0].transform.gameObject.SetActive(true);
        icon_explain[1].transform.gameObject.SetActive(false);
        icon_explain[2].transform.gameObject.SetActive(false);
        icon_explain[3].transform.gameObject.SetActive(false);
        //icon[4].transform.gameObject.SetActive(false);
        teleport.transform.gameObject.SetActive(false);
        teleport2.transform.gameObject.SetActive(false);
      //  sound_click.transform.gameObject.SetActive(false);
       // sound_change.transform.gameObject.SetActive(false);
      //  sound_clear.transform.gameObject.SetActive(false);
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        Eraser.transform.gameObject.SetActive(false);

        Go1 = GameObject.Find("GO1").GetComponentInChildren<SpriteRenderer>();
        Go2 = GameObject.Find("GO2").GetComponentInChildren<SpriteRenderer>();
        Go1.sprite = icon[1].GetComponentInChildren<SpriteRenderer>().sprite;
        Go2.sprite = icon[2].GetComponentInChildren<SpriteRenderer>().sprite;

        GameObject.Find("LeftHand").GetComponent<Trush4>().enabled = false;
    }


    void Update()
    {
        if (SnapTurnUp.GetStateDown(Pose.inputSource))
        {
            teleport2.transform.gameObject.SetActive(false);
        }
        if (GrabPinch.GetStateDown(Pose.inputSource))
        {

            IconClick(IconClick_bool);
        }
        if (SnapTurnRight.GetStateDown(Pose.inputSource))
        {
            IconSwitch_bool = true;
            IconSwitch(IconSwitch_bool);
        }

        if (SnapTurnLeft.GetStateDown(Pose.inputSource))
        {
            IconSwitch_bool = false;
            IconSwitch(IconSwitch_bool);
        }
        if (GrapGrip.GetStateDown(Pose.inputSource))
        {
            if (SwitchSystem % 2 == 1)
            { BoolGrip = true;
                Debug.Log("True");
            }
            else
            {
                Debug.Log("False");
                BoolGrip = false;
            }
            SwitchSystem++;
        }
        //
      
        //
    }
    int SwitchSystem = 0;

    void IconSwitch(bool x)
    {
        if (x == true)
        {
            icon[i].transform.gameObject.SetActive(false);
            icon_explain[i].transform.gameObject.SetActive(false);
            if (i + 1 == 5)
            {
                i = -1;
            }
            icon[i + 1].transform.gameObject.SetActive(true);
            icon_explain[i + 1].transform.gameObject.SetActive(true);
            //sound_change.transform.gameObject.SetActive(true);
            i++;

            int i1 = i;
            int i2 = i;
            if (i1 + 1 == 5)
            {
                i1 = -1;
            }
            if (i2 == 0)
            {
                i2 = 5;
            }
            Go1.sprite = icon[i1 + 1].GetComponentInChildren<SpriteRenderer>().sprite;
            Go2.sprite = icon[i2 - 1].GetComponentInChildren<SpriteRenderer>().sprite;

        }
        if (x == false)
        {
            icon[i].transform.gameObject.SetActive(false);
            icon_explain[i].transform.gameObject.SetActive(false);
            if (i - 1 == -1)
            {
                i = 5;
            }

            icon[i - 1].transform.gameObject.SetActive(true);
            icon_explain[i - 1].transform.gameObject.SetActive(true);
            i--;
           // sound_change.transform.gameObject.SetActive(true);

            int i1 = i;
            int i2 = i;
            if (i2 - 1 == -1)
            {
                i2 = 5;
            }
            if (i1 == 4)
            {
                i1 = -1;
            }
            Go1.sprite = icon[i1 + 1].GetComponentInChildren<SpriteRenderer>().sprite;
            Go2.sprite = icon[i2 - 1].GetComponentInChildren<SpriteRenderer>().sprite;

        }
        // sound_change.transform.gameObject.SetActive(false);//須測試
    }
   public bool BoolGrip = true;
    void IconClick(bool x)
    {
        
        //這裡的 程式會很難理解,因為數字變成功能,會很抽象。如果有對照表 or enum 的方法,會容易理解
        if (x == true)
        {
            if (i == 0)//TODO: 天啊!!! 這裡怎麼會有i,太過份了... 要「重新命名」,這是很安全的Visual Studio的重構功能
            {//以後 i 只能在 for迴圈裡拿來用, 才是易理解的作法


                icon_explain[0].transform.gameObject.SetActive(false);

                Debug.Log("GripTrue");
             //   sound_click.transform.gameObject.SetActive(true);
                TrueFalse.gameObject.SetActive(false);
                IconClick_bool = false;
                ColorChoose.gameObject.SetActive(true);
     

                GameObject.Find("LeftHand").GetComponent<Trush4>().enabled = true;
             
        }
            if (i == 1)
            {
                icon_explain[1].transform.gameObject.SetActive(false);

                //sound_click.transform.gameObject.SetActive(true);
                TrueFalse.gameObject.SetActive(false);
                IconClick_bool = false;
                teleport.transform.gameObject.SetActive(true);
                teleport2.transform.gameObject.SetActive(true);

            }
            if (i == 2)
            {
                //icon_explain[2].transform.gameObject.SetActive(false);

                //  sound_clear.transform.gameObject.SetActive(true);
                drawline.MyDrawLessONE();
   
            }
            /*
            if (i == 2)
            {
              //  sound_clear.transform.gameObject.SetActive(true);
                drawline.MyDrawLineDestroy();
            }
            */

            if (i ==4 )
            {
                //icon_explain[3].transform.gameObject.SetActive(false);

                //  sound_click.transform.gameObject.SetActive(true);
                Eraser.transform.gameObject.SetActive(true);
                IconClick_bool = false;
            }
            if (i == 3)
            {
                //icon_explain[4].transform.gameObject.SetActive(false);

                //  sound_clear.transform.gameObject.SetActive(true);
                drawline.MyDrawLineDestroy();
            }
            

        }

        if (x == false)
        {
            //  sound_clear.transform.gameObject.SetActive(false);
            // sound_click.transform.gameObject.SetActive(false);
            if (i == 0) {
                if (BoolGrip == true) { }
                else { SwitchSystem++; }
                
                GameObject.Find("LeftHand").GetComponent<Trush4>().SliderSwitch = false;
                BoolGrip = true;
                GameObject.Find("LeftHand").GetComponent<Trush4>().RadioSliderImage.SetActive(true);
            }

            TrueFalse.gameObject.SetActive(true);
            IconClick_bool = true;
            ColorChoose.gameObject.SetActive(false);
            teleport.transform.gameObject.SetActive(false);
            teleport2.transform.gameObject.SetActive(false);
            Eraser.transform.gameObject.SetActive(false);
            GameObject.Find("LeftHand").GetComponent<Trush4>().enabled = false;
    
            //gameObject.GetComponent<Trush4>().gameObject.SetActive(false);


        }
    }



}
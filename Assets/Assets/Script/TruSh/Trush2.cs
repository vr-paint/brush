using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Trush2 : MonoBehaviour
{
    public float Yy = 0.0f, Xx;
    public float pixelheight = 200, pixelWidth = 200;
    private float angle;
    public Color color_test;
    public Material mat;
    public GameObject controller_circle,Quad_c;
  
    void Start()
    {
       pixelWidth = GameObject.Find("arcSlider").GetComponentInChildren<RectTransform>().rect.width / 2;
       pixelheight = GameObject.Find("arcSlider").GetComponentInChildren<RectTransform>().rect.height / 2;
       //color_test= GameObject.Find("RadioSliderImage").GetComponentInChildren<Image>().color;
       controller_circle = GameObject.Find("controller_circle");
       Quad_c=GameObject.Find("Quad_c");
 
       mat = new Material(Shader.Find("Sprites/Default"));
       mat = Quad_c.GetComponent<Renderer>().material;
   
    }
    
    void Update()
    {
        Xx =Input.mousePosition.x;
        Yy = Input.mousePosition.y;
   
        //if(Input.GetMouseButtonDown(0)){  }
        Angle(); 
        Controller();
       
    }
    void Angle() {
      //   double angle1 = Mathf.Atan2((Yy - pixelheight)*-1, Xx - pixelWidth);
      //   double angle2 = angle1 / Mathf.PI * 180 + 90;
      //   double angle3=(angle2+360)%360;
      //   float h;  h=(float)angle3/360.0f;

        float angle = Mathf.Atan2(Yy-pixelheight, Xx-pixelWidth);
        angle= (-angle+Mathf.PI/2)/(2*Mathf.PI);
        if(angle<0)angle+=1.0f;
        float h=angle;
        mat.color = Color.HSVToRGB(h, 1.0f, 1.0f);
        //print( h*360+  " matcolor = "+mat.color);

        
        Quad_Color();
    }

    void Quad_Color(){
        Color [] c1 = new Color[]{
            Color.black, Color.white, Color.black, mat.color,
        };
        //Quad_c.GetComponent<MeshFilter>().mesh.SetColors(c1,0);

    }

    Quaternion currentRotation;
    Vector3 currentEulerAngles;
    void Controller(){
        float angle = Mathf.Atan2(Yy-pixelheight, Xx-pixelWidth);
   
        currentEulerAngles = new Vector3(0, 0, angle*180/Mathf.PI-90);
        currentRotation.eulerAngles = currentEulerAngles;
        controller_circle.transform.rotation = currentRotation;
        //controller_circle.transform.Rotate(0.0f,0.0f,angle);
        //print(currentEulerAngles);
    }
  
}
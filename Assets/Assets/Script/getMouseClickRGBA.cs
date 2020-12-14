using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class getMouseClickRGBA : MonoBehaviour
{

    public Color UIObjectRGB;
    public Color getPixel;
    public Vector2 imageScale;
    UnityEngine.UI.Text showColorUI;
    UnityEngine.UI.Image showColorSquare;
    public Texture2D sourceTex;
     public Rect sourceRect;


    void Start()
    {
        showColorUI = GameObject.Find("Output").GetComponent<UnityEngine.UI.Text>();
        showColorSquare = GameObject.Find("square").GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        getPixelByMouseClick();
    }

    void getPixelByMouseClick()
    {
        float x=Input.mousePosition.x;
        float y=Input.mousePosition.y;
        
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);
          if (Input.GetMouseButtonDown(0)){
              
            Color[] pix = sourceTex.GetPixels((int)x,(int) y, width, height);
          }
    }



    //取得滑鼠點選UI物件的RGBA

    void showImageColor(Color Color)
    {
        showColorUI.text = Color.ToString("F2");
        showColorSquare.color = Color;
    }
}

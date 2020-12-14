using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class getPixel : MonoBehaviour
{
    // Start is called before the first frame update
    public Color getpixel;
    public Texture2D sourceTex;
    public Rect sourceRect;
    public Vector3 all;
    public  int width;
    public int height;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 all= Input.mousePosition;
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);
        getPixelByMouseClick();
    }
      void getPixelByMouseClick()
    {
      if (Input.GetMouseButtonDown(0)){
         

        Color[] pix = sourceTex.GetPixels((int)all.x, (int)all.y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        GetComponent<Renderer>().material.mainTexture = destTex;

      }
    }
    void showImageColor(Color Color)
    {
        Debug.Log(Color);
    }
}


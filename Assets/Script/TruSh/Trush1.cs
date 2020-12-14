using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trush1 : MonoBehaviour
{
    // Source texture and the rectangular area we want to extract.
    public Texture2D sourceTex;
    public Rect sourceRect;

    void Start()
    {


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            test123();
        }
    }
    void test123()
    {


        sourceRect.x = (int)Input.mousePosition.x;
        sourceRect.y = (int)Input.mousePosition.y;
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels((int)sourceRect.x, (int)sourceRect.y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        GetComponent<Renderer>().material.mainTexture = destTex;

    }
}
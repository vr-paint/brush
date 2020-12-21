using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MeshColiderAndSphere : MonoBehaviour
{
    public GameObject Object;
    public GameObject sound_clear;
    GraphicsLineRenderer VerticeTransform;
    DrawLine FinalErase;
    // Start is called before the first frame update
    void Start()
    {
        VerticeTransform = gameObject.GetComponent<GraphicsLineRenderer>();
    }
    bool Button = true;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("RightHand").GetComponent<DrawLine>().Nodelete == true) { }//TODO:這可能是個地雷,需要改名 & 想一下是不是全部考慮到

        else
        {
            FinalErase = GameObject.Find("RightHand").gameObject.GetComponent<DrawLine>();
            Object = GameObject.Find("Eraser");
            //  sound_clear=GameObject.Find("clear_sound");
            string[] stringSeparators = new string[] { "TEST" };
            string[] result = gameObject.transform.name.Split(stringSeparators, StringSplitOptions.None);
            int a = int.Parse(result[1]);
            //sound_clear.transform.gameObject.SetActive(false);///嘗試

            if (Object != null)
            {
                for (int i = 0; i < VerticeTransform.vertices.Length; i++)
                {
                    float distance = Vector2.Distance(Object.transform.position, VerticeTransform.vertices[i]);

                    if (distance < 0.02f)
                    {
                        // sound_clear.transform.gameObject.SetActive(true);///嘗試
                        Destroy(gameObject);
                        Destroy(GameObject.Find("KDtree" + a));
                        FinalErase.RestartArray(a, Button);
                        Button = false;
                    }
                }

            }
        }
            //sound_clear.transform.gameObject.SetActive(false);///嘗試
        } 
}

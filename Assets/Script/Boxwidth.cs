using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxwidth : MonoBehaviour
{
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BoxControlWidth(float w) {
        //box.transform.position = new Vector3(0.2f,0.2f,0.2f);
        box.transform.localScale = new Vector3(w,w,w);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlescripts : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed *Time.deltaTime);
    }
}

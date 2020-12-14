using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory_line : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "test") //如果aaa碰撞事件的物件標籤名稱是test
        {
            Debug.Log("a");
            Destroy(GameObject.Find(other.name)); //刪除碰撞到的物件(CubeA)
        }

    }
   
}

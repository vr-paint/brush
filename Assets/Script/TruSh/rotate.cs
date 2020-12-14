using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Rotate : MonoBehaviour
{
    /// <summary>
    /// 子物体公转的对象
    /// </summary>
    public Transform m_parent;
    /// <summary>
    /// 自转的子物体
    /// </summary>
    public Transform m_children;
 
    void Update()
    {
        //自转
        m_children.Rotate(Vector3.up, Space.Self);
        //公转
        m_children.RotateAround(m_parent.position, m_parent.up, Time.deltaTime * 10);
    }
}

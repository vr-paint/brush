using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class just_test_kdtree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        List<Transform> enemyList = new List<Transform>();
        for (int i = 0; i < 10; i++) {
            GameObject empty = new GameObject();
            Transform newTransform = empty.transform;
            newTransform.position = new Vector3(i, 0, 0);
            enemyList.Add(newTransform);
        }
        KdTree<Transform> enemyKdTree = new KdTree<Transform>();
        enemyKdTree.AddAll(enemyList);
        Transform now = enemyKdTree.FindClosest(new Vector3(5.2f, 0, 0));
        print(now.position);*/
        //以上方法, 是參考 http://gyanendushekhar.com/2020/02/23/find-closest-enemy-in-unity-3d/ 使用 https://github.com/orifmilod/KdTree-Unity3D : Used in this example 的結構

        List<myNode> nodeList = new List<myNode>();
        //之後就 for(i彩帶
        //         for(j點
        for(int i=0; i<10; i++)
        {
            GameObject one = new GameObject();//要洗成 GameObject 的 component
            one.AddComponent<myNode>();//加進去
            myNode node = one.GetComponent<myNode>();//再拿出來
            //node.I = i; node.J = j; 
            node.transform.position = new Vector3(i, 0, 0);//將來就有 tranform 可以做 KdTree的比較了...
            nodeList.Add(node);//這個 node有 tranform哦!!!!
        }
        KdTree<myNode> nodeKdTree = new KdTree<myNode>();
        nodeKdTree.AddAll(nodeList);
        myNode nearest = nodeKdTree.FindClosest(new Vector3(4.2f, 0, 0));
        print("hahaha" + nearest.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

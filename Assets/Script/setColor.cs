
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class setColor : MonoBehaviour {
	public GameObject m;
    public static setColor _instance;
	float temp;
	public Texture2D setCo;
     
	private RectTransform im;
	public float _sizeDeltaY;
 
 
	public float local_V;
	public float local_y;
	public float scenceY;
 
 
	private float   height;
 
 
 
	private float baifeibi;
 
    void Awake()
    {
        _instance = this;
    }
	void Start()
	{
		height = Screen.height;
		im = GetComponent<RectTransform> ();
		_sizeDeltaY = im.sizeDelta.y;
		local_V = im.anchoredPosition3D.y;
		local_y = im.anchoredPosition3D.y + _sizeDeltaY;
	}
	
	// Update is called once per frame
	public void OnMouseDown () 
	{
        if (m == null)
            return;
		temp = (Input.mousePosition.y - height);
	    baifeibi=(temp - local_V)/_sizeDeltaY;
		//(int)(baifeibi*setCo.height)
		Color co= setCo.GetPixel (1,(int)(baifeibi*setCo.height));
		print ((int)(baifeibi * setCo.height));
        MeshRenderer[] mrs;
        mrs = m.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in mrs)
        {
            foreach (Material items in item.materials)
            {
                items.color = co;
            }
             
        }
       
	}
}

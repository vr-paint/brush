using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class ArcSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image handleButton;
    float circleRadius = 0.0f;
 
    bool isPointerDown = false;
    public Image baseCircle;
 
    //忽略圈内的交互
    public float ignoreInTouchRadiusHandleOffset = 10;
 
    Vector3 handleButtonLocation;
 
    [Tooltip("初始角度到终止角度")]
    public float firstAngle = 30;
    public float secondAngle = 150;
 
    float tempAngle = 30;//用来缓动
    public void Start()
    {
        circleRadius = Mathf.Sqrt(Mathf.Pow(handleButton.GetComponent<RectTransform>().localPosition.x, 2) + Mathf.Pow(handleButton.GetComponent<RectTransform>().localPosition.y, 2));
        ignoreInTouchRadiusHandleOffset = circleRadius - ignoreInTouchRadiusHandleOffset;
 
        handleButtonLocation = handleButton.GetComponent<RectTransform>().localPosition;
    }
    public void Update()
    {
        //用来重置
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReSet();
        }
    }
    public void ReSet()
    {
        handleButton.GetComponent<RectTransform>().localPosition = handleButtonLocation;
    }
	public void OnPointerEnter( PointerEventData eventData )
	{
		StartCoroutine( "TrackPointer" );
	}
	
	//如果需要移动到外部时仍然有效可以去掉这里的
	public void OnPointerExit( PointerEventData eventData )
	{
		StopCoroutine( "TrackPointer" );
	}
 
	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown= true;
	}
 
	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown= false;
	}
    
	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();
 
		var text = GetComponentInChildren<Text>();
		
		if( ray != null && input != null )
		{
			while( Application.isPlaying )
			{                    
				//这个是左侧的
				if (isPointerDown)
				{
					Vector2 localPos;
                    //获取鼠标当前位置out里赋值
					RectTransformUtility.ScreenPointToLocalPointInRectangle( transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos );
 
                    localPos.x = -localPos.x;
 
                    //半径
                    float mouseRadius = Mathf.Sqrt(localPos.x*localPos.x+localPos.y*localPos.y);
 
                    //阻止圆内部点击的响应，只允许在一个圆环上进行响应
                    if (mouseRadius > ignoreInTouchRadiusHandleOffset)// && handleButton.GetComponent<RectTransform>().localPosition.x <= 0
                    {
                        //0-180  -180-0偏移后的角度 从第一象限校正到0-360
                        float angle = (Mathf.Atan2(localPos.y, localPos.x)) * Mathf.Rad2Deg;
                        if (angle < 0) angle = 360 + angle;;
 
                        if (angle < firstAngle) angle = firstAngle;
                        if (angle > secondAngle) angle = secondAngle;
 
                        angle = (tempAngle + angle) / 2f;
                        tempAngle = angle;
                        //改变小圆的位置
                        handleButton.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Cos(-angle / Mathf.Rad2Deg + 45.0f * Mathf.PI) * circleRadius, Mathf.Sin(-angle / Mathf.Rad2Deg + 45.0f * Mathf.PI) * circleRadius, 0);
 
 
                      //this.transform.parent.GetComponent<Image>().color = Color.Lerp(Color.green, Color.blue, (angle - firstAngle) / (secondAngle - firstAngle));
 
                            
                        //数值的偏移值
                        float temp = secondAngle - firstAngle;// 360 - 285 + 64;
 
                        float tempangle = (angle - firstAngle)/ (secondAngle - firstAngle);
                            
                        //可能会出现很小的数 注意保留小数位数
                        text.text = tempangle.ToString();
                    }
                }
				yield return 0;
			}        
		}   
	}
}

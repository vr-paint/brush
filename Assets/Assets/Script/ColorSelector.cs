using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour {
    //调整颜色的事件，方便在其他地方修改颜色
    public delegate void ColorChanged(Color color);
    public event ColorChanged colorChanged;
    //展示选择的颜色
    public Graphic showRawImage;
    public Color32 mainColor = Color.white;
    bool isManuColor = false;
    //调色板
    public RawImage selectorImage;
    //选择图标
    public Transform smallIcon;
    //记录之前的位置
    Vector3 oldposition;
    
    void Start () {
        //给调色板添加按下，进入，出去和拖拽的方法
        AddEventTrigger(selectorImage.transform, EventTriggerType.PointerDown, Selector_Drag);
        AddEventTrigger(selectorImage.transform, EventTriggerType.PointerEnter, Selector_Enter);
        AddEventTrigger(selectorImage.transform, EventTriggerType.PointerExit, Selector_Exit);
        AddEventTrigger(selectorImage.transform, EventTriggerType.Drag, Selector_Drag);
        oldposition = smallIcon.position;
    }
    //这个是固定的绑定书写格式，参数一：给那个物体绑定，参数二：触发类型，参数三：触发时执行的方法
    public void AddEventTrigger(Transform insObject, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> myFunction)//泛型委托
    {
        //获取该物体上的组件
        EventTrigger trigger = insObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //绑定触发类型
        entry.eventID = eventType;
        //绑定触发执行的方法
        entry.callback.AddListener(myFunction);
        //将这个触发类型和方法添加到列表中等待执行
        trigger.triggers.Add(entry);
    }
    //进入方法
    public void Selector_Enter(BaseEventData eventData)
    {
        isManuColor = true;
    }
    //退出方法
    public void Selector_Exit(BaseEventData eventData)
    {
        isManuColor = false;
    }
    //拖拽方法
    public void Selector_Drag(BaseEventData eventData)
    {
        //需要将背景调色板的中心点改为左下角（0，0）
        PointerEventData ped = (PointerEventData)eventData;
        //PointerEventData.position返回的是当前指针的位置，左下角为原点（0,0），右上角（屏幕宽，屏幕高），根据分辨率来算
        smallIcon.position = ped.position;
        //获取调色板图片
        Texture2D tex = (Texture2D)selectorImage.texture;
        //获取鼠标（smallIcon）在调色板上的x，y轴上的比例
        float xtemp = smallIcon.localPosition.x / selectorImage.rectTransform.rect.width;
        float ytemp = smallIcon.localPosition.y / selectorImage.rectTransform.rect.height;
        //当鼠标移出调色板的时候，将smallIcon位置变为移出之前的位置
        if (xtemp < -1 || xtemp > 0 || ytemp < -1 || ytemp > 0)
        {
            smallIcon.position = oldposition;
            return;
        }
        oldposition = smallIcon.position;
        //通过鼠标在调色板的位置比例，获取鼠标在调色板上的具体(X,Y)坐标
        int x = (int)(xtemp * tex.width);
        int y = (int)(ytemp * tex.height);
        //通过Texture2D.GetPixel这个方法来获取该位置下的像素的颜色值
        Color color = tex.GetPixel(x, y);
        if (color.a < 0.1f) color = Color.white;
        if(color!=mainColor)
        {
            //修改颜色值
            mainColor = color;
            showRawImage.color = mainColor;
            if (colorChanged != null)
                colorChanged(mainColor);
        }
        Debug.Log("<color='#" + ColorUtility.ToHtmlStringRGBA(color) + "'>███████</color>");
    }

    private void Update()
    {
        //简单的附加功能，点击空白的地方关闭调色板
        if (!isManuColor)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                selectorImage.transform.parent.gameObject.SetActive(false);
            }
        }
    }

}
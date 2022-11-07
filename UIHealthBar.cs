using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //创建公有静态成员属性，获取当前血条，静态血条实例对象
    public static UIHealthBar Instance { get; private set; }

    //创建UI图形对象MASK
    public Image mask;

    //设置变量，记录遮罩层初始长度
    float originalSize;

    private void Awake()
    {
        //设置静态实例为当前类对象
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //获取遮罩层图像的初始宽度
       originalSize = mask.rectTransform.rect.width;
    }


    //创建方法，更改MASK遮罩层宽度
    public void SetValue(float value)
    {
        //设置更改MASK遮罩层的宽度，并且根据参数进行更改
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

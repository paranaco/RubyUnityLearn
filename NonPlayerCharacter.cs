using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    //对话框显示时间
    public float displayTime = 4.0f;
    
    //获取对话框对象
    public GameObject dialogbox;
    //计时器，倒计时文本框显示的时间
    float timerDiaplay;

    //创建游戏对象来获取TMP控件
    public GameObject dlgTxtProGameObject;
    //创建游戏组件类对象
    TextMeshProUGUI _tmTxtBox;
    //设置变量存储当前页数
    int _currentPage = 1;
    //声明总页数变量
    int _totalPages;


    // Start is called before the first frame update
    void Start()
    {   
        //不显示对话框
        dialogbox.SetActive(false);
        //设置计时器不可用
        timerDiaplay = -1.0f;

        //获取对话框组件
        _tmTxtBox =dlgTxtProGameObject.GetComponent<TextMeshProUGUI>();

}

    // Update is called once per frame
    //在Update中倒计时
    private void Awake()
    {
        
    }
    void Update()
    {
        //获取对话框组件中对话文字总页数
        _totalPages = _tmTxtBox.textInfo.pageCount;
        //如果倒计时未结束
        if (timerDiaplay >= 0.0f)
        {
            //翻页功能
            //检测用户输入，SPACE键弹起时激活
            if(Input.GetKeyUp(KeyCode.Space))
            {
                //如果不是最大页，向后翻页
                if(_currentPage<_totalPages)
                {
                    _currentPage++;
                }
                //否则回到第一页
                else
                {
                    _currentPage = 1;
                }
                //设置现在显示的页数
                _tmTxtBox.pageToDisplay =_currentPage;
            }
            //开始倒计时
            timerDiaplay-=Time.deltaTime;
            //如果倒计时结束，隐藏对话框
            if (timerDiaplay < 0.0f)
            {
                dialogbox.SetActive(false);
            }
        }
        
    }
    //显示文本框方法
    public void DisplayDialog()
    {
        //重置计时器
        timerDiaplay = displayTime;
       //显示对话框
       dialogbox.SetActive(true);
    }
}

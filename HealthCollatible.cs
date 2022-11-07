using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//触发器碰撞事件，碰撞执行其中代码
public class HealthCollatible : MonoBehaviour
{
    //声明一个公开的音频剪辑变量，挂接音频文件
    public AudioClip healthClip;

    public int amount = 1 ;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //获取Conoller 组件中的脚本组件
        Conoller rubyConoller = other. GetComponent <Conoller>();

        if(rubyConoller!= null )
        {
            if(rubyConoller.health < rubyConoller.maxHealth )
            {
                 
            
                 //更改生命值
                rubyConoller.ChangeHealth( amount );
                //销毁游戏对象
                 Destroy (gameObject);
                //播放声音文件
                rubyConoller.PlaySound(healthClip);
            } 
        }
       else
       {
        UnityEngine.Debug.LogError("未获取到对象组件");
       }
    }
}

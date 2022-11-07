using System.Runtime.CompilerServices;
using System.Timers;
using System.Threading;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeyConoller1 : MonoBehaviour
{
    
    //定义移动速度，实际是刷新时的移动距离
    public float speed;
    //定义是否垂直移动,true按Y轴移动，false按X轴移动
    public bool vertical;

    AudioSource EmeySource;
    public AudioClip fixedClip;
    public AudioClip hitClip;

    //声明刚体对象
    Rigidbody2D rigidbodyEmeny;

    //朝一个方向移动的总时间
    public float changeTime = 3.0f;

    //计时器
    float timer;
    //方向
    int direction = 1;

    Animator animator;

    //开放属性，获取烟雾对象
    public ParticleSystem smokeEffect;

    
    //声明布尔变量，初始值代表机器人损坏
    bool broked = true;


    // Start is called before the first frame update
    void Start()
    {
        //获取游戏对象刚体组件
        rigidbodyEmeny = GetComponent<Rigidbody2D>();
        timer = changeTime;
        //获取游戏对象动画组件
        animator = GetComponent<Animator>();
        EmeySource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //在update中操作计时器G
    void Update()
    {   
        
        //如果修复完成则退出update方法
        if (!broked){
           return; 
        }
        //每次减少时间
        timer-= Time.deltaTime;
        //如果timer到0 重置
        if (timer < 0)
        {
            direction = - direction;
            timer = changeTime;
        }
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {

        //如果修复完成则退出update方法
        if (!broked){
           return; 
        }
        //获取当前游戏对象刚体组件所在位置
        UnityEngine.Vector2 position = rigidbodyEmeny.position;


        //纵向移动，沿着Y轴移动
        if (vertical)
        {
            position.y = position.y + Time.deltaTime *speed * direction;

            //使用SetFloat函数将参数发送给Animator，第一个参数为名称，第二个参数为当前值
            animator.SetFloat("Move X",0);
            //使用direction来控制Y轴的移动方向
            animator.SetFloat("Move Y",direction);
        }
        //否则，沿着X轴移动
        else {
            position.x = position.x + Time.deltaTime *speed* direction;

            //X轴部分与Y轴相反
            animator.SetFloat("Move X",direction);
            animator.SetFloat("Move Y",0);
        }

        //刚体自带方法，移动到位置
        rigidbodyEmeny.MovePosition(position);
    }

    //刚体碰撞事件
    //在此方法添加玩家伤害逻辑
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
         //获取和敌人对象碰撞玩家角色对象 
         Conoller ruby = other.gameObject.GetComponent<Conoller>();

         if (ruby != null)
         {
            //玩家角色掉血
            ruby.ChangeHealth(-1);
         }
    }
    //修复机器人方法
    public void Fix () {
        //更改状态为已修复
        broked = false;
        //让机器人不再发饰碰撞
        //刚体对象取消物理引擎
        rigidbodyEmeny.simulated = false;
        //播放修复后动画
        animator.SetTrigger("Fixed");

        //销毁粒子系统对象
        //Destroy(smokeEffect);

        //粒子系统停止生成
        smokeEffect.Stop();
        GetComponent<AudioSource>().Stop();
        EmeySound(fixedClip);
    }
    public void EmeySound(AudioClip EmeyClip)
    {
        EmeySource.PlayOneShot(EmeyClip);
    }
} 

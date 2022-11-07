using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading;
using System;
using System.Net.Mime;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Conoller : MonoBehaviour
{
    //声明音频源对象，用来控制音频播放
    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip hitClip;
    //声明游戏对象projectilePrefab，获取子弹预制件对象
    public GameObject projectilePrefab;

     
    //设置玩家无敌时间间隔
    public float timeInvincible = 2.0f;
    //设置玩家是否无敌的布尔变量，默认为false
    bool isInvincible;
    //定义无敌时间计时器,进行无敌时间计时
    float invincibleTimer;

    //
    public ParticleSystem eatEffect;




    //声明对象最大生命值变量
    public float maxHealth = 5;

    //封装变量，对数据成员保护，增加属性
    //属性是公有的，通过取值器get\赋值器set设定对应访问规则，满足规则才能访问
    //声明当前生命值变量
    public float health{get {return currentHealth;}
    //set {currentHealth = value ;}
    }
    float currentHealth;

    //声明刚体对象变量
    Rigidbody2D rigidbodyRuby;
    //声明输入轴输入
    float horizontal;
    float vertical;


    //声明Animator对象
    Animator RubyAnimator;
    //声明Vector2类，存储静止状态下角色朝向（面对的方向）
    //角色站立不动时，Move X，Y均为0，需要给状态机参数决定朝向
    UnityEngine.Vector2 lookDirection = new UnityEngine.Vector2(1,0);

    //移动
    UnityEngine.Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        //获取刚体组件
      rigidbodyRuby = GetComponent<Rigidbody2D>(); 
      //初始状态执行，当前生命值为最大
      currentHealth = maxHealth;
      

      //实例化Animator对象，获取当前对象的Amimator对象组件
      RubyAnimator = GetComponent<Animator>();
      
      //获取声音源组件对象
      audioSource = GetComponent<AudioSource>();

    }

    //声明移动值变量
    public float speed = 0.1f;


    // Update is called once per frame
    void Update()
    {
       //判断是否处于无敌状态，来进行计时器更改
       if (isInvincible)
       {
        //如果无敌，进入计时器倒计时
        //每次update减去一帧所消耗的时间
        invincibleTimer = invincibleTimer - Time.deltaTime;
        //如果计时器时间消耗完
        if (invincibleTimer < 0)
        {
            //取消无敌状态
            isInvincible = false;
        }
       }


        //获取输入轴的输入值
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

      
    


        //实例化Vector2存储Buby的移动信息
        move= new UnityEngine.Vector2 (horizontal,vertical);
        //如果move x/y不为零，则角色正在移动
        //设置角色面向方向为移动方向
        //停止移动，保持朝向，使用if重新赋值转向后朝向
        //使用Mathf.approximately比较两个浮点值，相似返回true  
        //！表示不为零
        //if条件表示正在移动
        if( !Mathf.Approximately ( move.x , 0.0f ) || !Mathf.Approximately ( move.y , 0.0f ) ) {
            //让角色朝向移动方向
            // lookDirection.x = move.x  lookDirection.y = move.y
            lookDirection.Set(move.x,move.y);


            //blend tree中表示方向的取值为-1.0到1.0使用向量归一化，表示方向
            lookDirection.Normalize();
        }
        //传递Ruby的朝向给blend tree
        RubyAnimator.SetFloat("Look X",lookDirection.x);
        RubyAnimator.SetFloat("Look Y",lookDirection.y);

        //传递ruby的速度给blend tree 
        //使用Vector的magnitue,返回长度绝对值
        RubyAnimator.SetFloat("Speed",move.magnitude);
        

        //获取用户输入数据，发射子弹
        if(Input.GetKeyDown(KeyCode.C) || Input.GetAxis("Fire1")!=0) {
           Launch();
            PlaySound(clip);
        }  
        //按下X键激活射线投射
        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("xxx");
            //创建一个射线投射碰撞对象，接收射线投射碰撞信息
            //使用Physics2D.Raycast方法投射射线
            //参数1：射线投射初始参数
            //参数2：投射方向
            //参数3：投射距离
            //参数4：射线生效的层
            RaycastHit2D hit = Physics2D.Raycast(rigidbodyRuby.position + UnityEngine.Vector2.up * 0.2f, lookDirection,1.25f,LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log($"bbb{hit.collider.gameObject}");
                //定义npc代码组件
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    //调用显示对话框方法
                    npc.DisplayDialog();
                }
            }
        }

    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// 周期函数，影响物理组件，固定时间间隔执行更新的方法，默认为0.02秒执行一次
    private void FixedUpdate()
    {
        //声明向量，赋值为对象位置
        UnityEngine.Vector2 position = transform.position;
        //位置*速度*输入轴*每帧时间间隔
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        //设置刚体位置
        rigidbodyRuby.position = position;
    }
    //更改生命值函数
    public void ChangeHealth (int amount )
    {
        //假设玩家受到伤害间隔为2秒
        //判断玩家是否受到伤害
        if (amount < 0) 
        {
            //判断玩家是否处于无敌状态
            if (isInvincible)
            {
                //处于无敌状态，退出ChangHealth方法
                return;
            }
            //当不是无敌状态时，执行以下代码
            //重置无敌状态为真
            isInvincible = true;
            //重置无敌时间
            invincibleTimer = timeInvincible;
            //播放角色受伤名为Hit的动画，发送True
            RubyAnimator.SetTrigger("Hit");
            PlaySound(hitClip);
        }
         //限制函数，限制当前生命值的赋值范围 0-max
        currentHealth = Mathf.Clamp (currentHealth + amount , 0 ,maxHealth );

        UIHealthBar.Instance.SetValue(currentHealth/(float)maxHealth);
        
    }
    //发射子弹
    void Launch ()
    {
        //生成绑定预制件projectilePrefab的游戏对象
        GameObject ProjectileObject = Instantiate(projectilePrefab,rigidbodyRuby.position+UnityEngine.Vector2.up*0.5f,UnityEngine.Quaternion.identity);
        
        //获取子弹游戏对象的脚本组件对象
        //使用脚本Projectile类来实例化
        Projectile projectile = ProjectileObject.GetComponent<Projectile>();
    
    
        //通过脚本对象的方法调用子弹移动
        //lookDirection为移动方向，即玩家的朝向
        //300为作用力大小，即发射速度
        projectile.Launch(lookDirection,300);

        RubyAnimator.SetTrigger("Launch"); 
    }
    //公有方法，调用会播放指定的音频剪辑
    public void PlaySound(AudioClip audioClip)
    {
        //调用音频源的PlayOneShot方法，播放指定音频
        audioSource.PlayOneShot(audioClip);
    }
}


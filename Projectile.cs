using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //声明刚体对象
    Rigidbody2D proRigidbody;


    public ParticleSystem boomEffect;

    // Start is called before the first frame update
    void Awake()
    {
        //实例化刚体对象
        proRigidbody = GetComponent<Rigidbody2D>();
    }


    //抛射方法
    public void Launch (UnityEngine.Vector2 direction,float force) {

        //通过刚体对象调用物理系统的Add Force方法
        //对游戏对象施加作用力使其移动
        proRigidbody.AddForce(direction*force);

        
    }

    //碰撞事件方法
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //获取子弹碰撞到的机器人对象的脚本组件
        EmeyConoller1 emeyConoller1 = collision.collider.GetComponent<EmeyConoller1>();
        if(emeyConoller1 !=null) {
            emeyConoller1.Fix();
        }
        //碰撞后销毁游戏对象
        Destroy(gameObject);
        //实例化指定的碰撞特效，位置为发生碰撞的位置
        Instantiate(boomEffect,transform.position,Quaternion.identity);
    } 
    // Update is called once per frame
    void Update()
    {
        //如果飞行距离超过100则销毁
        if(transform.position.magnitude>100.0f) {
        Destroy(gameObject);
        }  
    }
}

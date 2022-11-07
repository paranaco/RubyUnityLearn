using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
      //每次伤害数值
     public int damageNum = -1;

    private void OnTriggerStay2D (Collider2D other)
    {
         
          // 获取脚本组件对象，返回值为字符串
          Conoller rubyConoller = other.GetComponent <Conoller>();

          if(rubyConoller != null ) {
           rubyConoller.ChangeHealth(damageNum) ;
          } 
         
    } 
        
    
}

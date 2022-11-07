using System.ComponentModel;
using System.Threading;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speede = 1f;
    Rigidbody2D rigidbody2d;
    public int dict = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void FixedUpdate()
    {
        UnityEngine.Vector2 position = transform.position;
        position.x = position.x + speede * Time.deltaTime * dict;
        position.y = position.y * speede * Time.deltaTime;
        rigidbody2d.position = position;
    }
}

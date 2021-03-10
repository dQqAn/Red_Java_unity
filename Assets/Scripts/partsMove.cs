using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partsMove : MonoBehaviour
{
    Rigidbody2D fizik;        
    Vector2 vec;
    float x;
    float y;

    void Start()
    {               
        x = Random.Range(-100, 100);
        y = Random.Range(10, 200);
        fizik = GetComponent<Rigidbody2D>();
        vec = new Vector2(x, y);
        fizik.AddForce(vec);

        //float hiz = 10;
        //fizik.MovePosition(fizik.position + Vector2.up * hiz * Time.fixedDeltaTime);        //gerek yok
        //fizik.velocity = new Vector2(Random.Range(1000, 2000), Random.Range(1000, 2000));
    }


    void FixedUpdate()
    {
        
    }
}

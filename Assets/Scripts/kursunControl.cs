using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunControl : MonoBehaviour
{

    enemyControl enemyControlScript;
    Rigidbody2D fizik;

    void Start()
    {
        enemyControlScript = GameObject.FindGameObjectWithTag("dusman_tag").GetComponent<enemyControl>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(enemyControlScript.getYon() * 1000);
    }
}

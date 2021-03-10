using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createParts : MonoBehaviour
{
    GameObject player;
    public GameObject[] parts;    
    bool control = true;

    public int deneme = 10;

    void Start()
    {                                             
        player= GameObject.FindGameObjectWithTag("Player");        
    }
    
    public void crtParts()
    {
        if (control && control==true)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                Vector2 vec2 = new Vector2(player.transform.position.x, player.transform.position.y);
                Instantiate(parts[i], vec2, Quaternion.identity);                
            }
            control = false;
        }
    }    
}

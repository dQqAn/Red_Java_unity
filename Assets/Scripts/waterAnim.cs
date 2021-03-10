using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterAnim : MonoBehaviour
{
    public Sprite[] animasyon;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animInterval = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.05f)
        {
            spriteRenderer.sprite = animasyon[animInterval++];
            if (animasyon.Length == animInterval)
            {
                animInterval = 0;
            }
            zaman = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private LivingEntity livingEntity;
    public float invincibility = 0;
    float alpha = 1;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Awake()
    {
        livingEntity = GetComponent<LivingEntity>();
    }
    private void Update()
    {
        if (invincibility > 1.2f)
        {
            alpha -= 2 * Time.deltaTime;
            spriteRenderer.material.color = new Color(1,1,1,alpha);
            invincibility -= Time.deltaTime;
            
        }
        else if(invincibility > 0.8f)
        {
            alpha +=  2*Time.deltaTime;
            spriteRenderer.material.color = new Color(1,1,1, alpha);
            invincibility -= Time.deltaTime;
        }
        else if (invincibility > 0.4f)
        {
            alpha -=  2*Time.deltaTime;
            spriteRenderer.material.color = new Color(1,1,1, alpha);
            invincibility -= Time.deltaTime;
        }
        else if (invincibility > 0)
        {
            alpha +=  2*Time.deltaTime;
            spriteRenderer.material.color = new Color(1,1,1, alpha);
            invincibility -= Time.deltaTime;
        }
        else
            alpha = 1;
    } 
}

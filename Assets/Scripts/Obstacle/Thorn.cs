using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Player" && collision.gameObject.GetComponent<PlayerHealth>().invincibility <= 0)
        {
            collision.gameObject.GetComponent<PlayerHealth>().invincibility = 1.5f;
            DamageMessage dmg;
            dmg.damager = gameObject;
            dmg.damageAmount = 10;
            dmg.hitPoint = transform.position;
            collision.gameObject.GetComponent<LivingEntity>().ApplyDamage(dmg);
            
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}

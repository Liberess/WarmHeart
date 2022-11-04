using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    float FireMoveSpeed=3f;
    float destoryTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(1, 0, 0);
        Destroy(gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(FireMoveSpeed *Time.deltaTime,0,0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            DamageMessage dmg;
            dmg.damager = gameObject;
            dmg.damageAmount = 10;
            dmg.hitPoint = transform.position;
            collision.gameObject.GetComponent<LivingEntity>().ApplyDamage(dmg);
            Destroy(gameObject);
        }
    }

}

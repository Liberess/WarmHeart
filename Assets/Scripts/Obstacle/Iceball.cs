using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    [SerializeField] private float fireMoveSpeed = 3f;
    [SerializeField] private float destoryTime = 10f;

    private void Start()
    {
        transform.Translate(3, 0, 0);
        Destroy(gameObject, destoryTime);
    }

    private void Update()
    {
        transform.Translate(fireMoveSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="PlayerBullet")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerHealth>().invincibility <= 0)
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
}

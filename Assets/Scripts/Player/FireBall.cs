using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float fireMoveSpeed = 3f;
    [SerializeField] private float destoryTime = 10f;

    private void Start()
    {
        transform.Translate(1, 0, 0);
        Destroy(gameObject, destoryTime);
    }

    private void Update()
    {
        transform.Translate(fireMoveSpeed *Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out LivingEntity livingEntity))
            {
                DamageMessage dmg;
                dmg.damager = gameObject;
                dmg.damageAmount = 10;
                dmg.hitPoint = transform.position;
                livingEntity.ApplyDamage(dmg);
            }
        }

        Destroy(gameObject);
    }
}

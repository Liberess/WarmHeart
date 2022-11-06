using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private ColoredFlash coloredFlash;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        coloredFlash = GetComponent<ColoredFlash>();

        minTimeBetDamaged = 1.5f;
    }

    private void Start()
    {
        DeathAction += () => anim.SetTrigger("doDie");
    }

    public override void ApplyDamage(DamageMessage dmgMsg)
    {
        if (IsDamageable)
        {
            coloredFlash.Duration = 0.5f;
            coloredFlash.Flash();

            var cam = FindObjectOfType<CamShake>();
            cam.VibrateForTime(0.5f, 0.1f);
        }
        
        base.ApplyDamage(dmgMsg);
    }
}

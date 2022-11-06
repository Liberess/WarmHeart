using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : LivingEntity
{
    [SerializeField] private GameObject iceEffectPrefab;

    private void Start()
    {
        
    }

    public override void ApplyDamage(DamageMessage dmgMsg)
    {
        base.ApplyDamage(dmgMsg);
    }
}

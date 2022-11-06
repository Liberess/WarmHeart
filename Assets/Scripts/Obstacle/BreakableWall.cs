using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Obstacle
{
    private LivingEntity livingEntity;

    private void Start()
    {
        livingEntity = GetComponent<LivingEntity>();
        livingEntity.DeathAction += () => Destroy(gameObject);
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }


    public override void OnStay()
    {
        
    }

    public override void OnInteract()
    {

    }
}

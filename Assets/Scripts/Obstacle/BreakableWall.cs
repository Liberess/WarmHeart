using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Obstacle
{
    private LivingEntity livingEntity;

    private void Awake()
    {
        livingEntity = GetComponent<LivingEntity>();
        livingEntity.DeathAction += Death;
    }

    private void Start()
    {

    }

    private void Death()
    {
        Debug.Log("gg");
        Destroy(gameObject);
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

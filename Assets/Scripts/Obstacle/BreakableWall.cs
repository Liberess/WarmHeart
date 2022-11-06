using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Obstacle
{
    private LivingEntity livingEntity;
    public Sprite Hp30;
    public Sprite Hp20;
    public Sprite Hp10;
    private void Start()
    {
        livingEntity = GetComponent<LivingEntity>();
        livingEntity.DeathAction += () => Destroy(gameObject);
    }
    private void Update()
    {
        switch (livingEntity.CurrentHp)
        {
            case 30:
                GetComponent<SpriteRenderer>().sprite = Hp30;
                break;
            case 20:
                GetComponent<SpriteRenderer>().sprite = Hp20;
                break;
            case 10:
                GetComponent<SpriteRenderer>().sprite = Hp10;
                break;
        }
        
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

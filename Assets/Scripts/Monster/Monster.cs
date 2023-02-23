using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    
    private IState currentState;
    private int Hp;
    protected Monstertype monstertype = Monstertype.MFLY;

    public float MonsterLeft;
    public float MonsterRight;
    public float MonsterRoundSize;
    public float MonsterAttackSize;
    public bool MonsterWay;

    public GameObject Player;

    private void Start()
    {
        Hp = CurrentHp;
        Player = GameObject.FindWithTag("Player");
        MonsterWay = true;
        MonsterLeft = transform.position.x - MonsterRoundSize;
        MonsterRight = transform.position.x + MonsterRoundSize;
    }
    private void Awake()
    { 
        SetState(new IdleState());
    }

    private void Update()
    {
        currentState.Update();
        
    }
    public void SetState(IState nextState)

    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = nextState;
        currentState.OnEnter(this);
    }
    void die_check()
    {
        if (CurrentHp == 0)
            SetState(new DeathState());
        else
            SetState(new IdleState());
    }
    void die()
    {
        Destroy(gameObject);
    }
    void PlayerAttack()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 2.2f && Player.transform.position.y<transform.position.y)
        {
            DamageMessage dmg;
            dmg.damager = gameObject;
            dmg.damageAmount = 10;
            dmg.hitPoint = transform.position;
            Player.gameObject.GetComponent<PlayerHealth>().ApplyDamage(dmg);
        }
    }
}
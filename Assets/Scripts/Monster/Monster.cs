using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    
    private IState currentState;
    protected Monstertype monstertype = Monstertype.MFLY;
    public bool PlayerIn=false;

    public float MonsterLeft;
    public float MonsterRight;
    public float MonsterRoundSize;
    public float MonsterAttackSize;
    public bool MonsterWay;

    public GameObject Player;

    private void Start()
    {
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
        if (CurrentHp == 0)
        {
            SetState(new DeathState());
            Debug.Log(0);
        }
        
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
    void die()
    {
        Destroy(gameObject);
    }
    void PlayerAttack()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 2)
        {
            DamageMessage dmg;
            dmg.damager = gameObject;
            dmg.damageAmount = 10;
            dmg.hitPoint = transform.position;
            Player.gameObject.GetComponent<PlayerHealth>().ApplyDamage(dmg);
        }
    }
}
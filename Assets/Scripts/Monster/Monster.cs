using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    
    private IState currentState;
    protected Monstertype monstertype = Monstertype.MFLY;
    public bool PlayerIn=false;
    public float PlayerX;
    public float PlayerY;

    public float MonsterLeft;
    public float MonsterRight;
    public float MonsterRoundSize;
    public bool MonsterWay;

    GameObject Player;

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerIn = true;
            PlayerX = collision.transform.position.x;
            PlayerY = collision.transform.position.y;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayerIn = false;
    }
    void die()
    {
        Destroy(gameObject);
    }
    void PlayerAttack()
    {
        if (Mathf.Abs(transform.position.x - PlayerX) < 2)
        {
            DamageMessage dmg;
            dmg.damager = gameObject;
            dmg.damageAmount = 10;
            dmg.hitPoint = transform.position;
            Player.gameObject.GetComponent<PlayerHealth>().ApplyDamage(dmg);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    private Monster ms;
    void IState.OnEnter(Monster ms)
    {
        this.ms = ms;
    }

    void IState.Update()
    {
        ms.GetComponent<SpriteRenderer>().flipX = ms.MonsterWay ? true : false;
        ms.transform.Translate((ms.MonsterWay ? 1 : -1) * Time.deltaTime, 0, 0);
        if (ms.transform.position.x <= ms.MonsterLeft) ms.MonsterWay = true;
        if (ms.transform.position.x >= ms.MonsterRight) ms.MonsterWay = false;
        if (Vector2.Distance( ms.transform.position , ms.Player.transform.position) < ms.MonsterAttackSize)
            ms.SetState(new AttackState());
    }
    void IState.OnExit()
    {

    }


}

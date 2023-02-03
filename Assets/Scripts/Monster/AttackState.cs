using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    // Start is called before the first frame update
    private Monster ms;
    void IState.OnEnter(Monster ms)
    {
        this.ms = ms;
    }

    void IState.Update()
    {
        if (Vector2.Distance(ms.transform.position, ms.Player.transform.position) < ms.MonsterAttackSize)
        {
            if (Mathf.Abs(ms.transform.position.x - ms.Player.transform.position.x) < 2)
            {
                ms.GetComponent<Animator>().SetBool("Attack", true);
            }
            else
                ms.GetComponent<Animator>().SetBool("Attack", false);
            ms.GetComponent<SpriteRenderer>().flipX = ms.transform.position.x - ms.Player.transform.position.x <= 0 ? true : false;
            ms.transform.Translate((ms.transform.position.x - ms.Player.transform.position.x <= 0 ? 1 : -1) * Time.deltaTime, 0, 0);
        }
        else
            ms.SetState(new IdleState());
    }
    void IState.OnExit()
    {

    }
}

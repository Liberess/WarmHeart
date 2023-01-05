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

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(0);
            //ms.SetState(new MoveState());
        }

    }
    void IState.OnExit()
    {


    }


}

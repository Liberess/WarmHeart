using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    private Monster ms;
    void IState.OnEnter(Monster ms)
    {
        this.ms = ms;
    }

    void IState.Update()
    {


    }
    void IState.OnExit()
    {
        
    }
}

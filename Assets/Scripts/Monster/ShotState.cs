using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotState : IState
{
    private Monster ms;
    void IState.OnEnter(Monster ms)
    {
        this.ms = ms;
        ms.GetComponent<Animator>().SetBool("Shot", true);
    }

    void IState.Update()
    {

    }
    void IState.OnExit()
    {
        ms.GetComponent<Animator>().SetBool("Shot", false);
    }
}

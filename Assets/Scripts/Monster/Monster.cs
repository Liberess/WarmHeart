using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private IState currentState;

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

}
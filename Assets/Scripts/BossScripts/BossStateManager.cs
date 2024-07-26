using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    State currentState;
    public AwakeState awakeState;

    private void Start()
    {
        currentState = awakeState;
    }
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState =currentState?.RunCurrentState();

        if(nextState != null )
        {
            //switch to the next state
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}

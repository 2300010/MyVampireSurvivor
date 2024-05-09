using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine machine;
    public abstract void OnStateUpdate();

    public abstract void OnStateEnter();

    protected State(StateMachine machine)
    {
        this.machine = machine;
    }
}

public class ChasingState : State
{
    public ChasingState(StateMachine machine) : base(machine)
    {
    }

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateUpdate()
    {
        //if(Vector2.Distance(machine.EnemyManager.gameObject.transform
           // .position,  ))
    }
}

public class AttackingState : State
{
    float timer;
    public AttackingState(StateMachine machine) : base(machine)
    {
    }

    public override void OnStateEnter()
    {
        timer = 1f;
        //machine.EnemyManager.SpawnProjectile();
    }

    public override void OnStateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            //machine.Transition<ChasingState>();
        }
    }
}

public class CooldownState : State
{
    public CooldownState(StateMachine machine) : base(machine)
    {
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {

    }
}

public class StateMachine 
{
    public EnemyManager EnemyManager;
    Dictionary<string, State> states = new();
    State currentState;

    public StateMachine(State currentState)
    {
        this.currentState = currentState;
    }

    public void AddState(State state)
    {
        if(currentState == null)
        {
            currentState = state;
            state.OnStateEnter();
        }
        states.Add(state.GetType().Name, state);
    }

    public void Transition<T>(T state) where T : State
    {
        currentState = states[state.GetType().Name];
        currentState.OnStateEnter();
    }
}

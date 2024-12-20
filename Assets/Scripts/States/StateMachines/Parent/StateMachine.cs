using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StateMachine : MonoBehaviour
{
    private IState currentState;

    public IState CurrentState => currentState;

    public Idle idleState;
    public Running runningState;
    public Punching punchingState;
    public Kicking kickingState;
    public Chasing chasingState;
    public Waiting waitingState;
    public Casting castingState;
    public Block blockState;
    //public GameObject gameObject;

    public StateMachine(GameObject gameObject)
    {
        this.idleState = new Idle(gameObject);
        this.runningState = new Running(gameObject);
        this.punchingState = new Punching(gameObject);
        this.kickingState = new Kicking(gameObject);
        this.chasingState = new Chasing(gameObject);
        this.waitingState = new Waiting(gameObject);
        this.castingState = new Casting(gameObject);
        this.blockState = new Block(gameObject);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void TransitionTo(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void UpdateState()
    {
        currentState.UpdateState();
    }

}

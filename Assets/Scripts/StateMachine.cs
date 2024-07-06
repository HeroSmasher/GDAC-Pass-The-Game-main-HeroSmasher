// Written by Bernard Powers https://github.com/almost-friday
/* 25 June 2024
 * Defined state machine and states
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Finite State Machine (FSM) that will be used to control AI.
/// </summary>
public class StateMachine
{
    public IState CurrentState;
    public StateMachine(IState initialState)
    {
        CurrentState = initialState;
    }
    public void ChangeState(IState newState, NPCManager O)
    {
        CurrentState.Exit(O);
        Debug.Log($"Changing from {CurrentState.Name} state to {newState.Name} state");
        CurrentState = newState;
        newState.Enter(O);
    }
}
/// <summary>
/// State Interface, implements entry, update, and exit methods.
/// </summary>
public interface IState
{
    public string Name { get; }
    public void Enter(NPCManager O);
    public void Update(NPCManager O);
    public void Exit(NPCManager O);
}
public class IdleState : IState
{
    public string Name => "Idle";
    public void Enter(NPCManager O)
    {
    }
    public void Exit(NPCManager O)  
    {
    }
    public void Update(NPCManager O)
    {
        if (O.PlayerNear)
        {
            O.FSM.ChangeState(NPCManager.ConverseState, O);
            return;
        }
    }
}
public class ConverseState : IState
{
    public string Name => "Conversing";
    public void Enter(NPCManager O)
    {
        O.TargetPlayer();
        O.ShowMessage(O.order);
    }
    public void Exit(NPCManager O)
    {
        O.HideMessage();
    }
    public void Update(NPCManager O)
    {
        O.FaceTarget();

        if (!O.PlayerNear)
        {
            O.FSM.ChangeState(NPCManager.IdleState, O);
            return;
        }
    }
}
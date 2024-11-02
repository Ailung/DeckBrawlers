using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Kicking : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private Leg leg;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Kicking(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
        if (characterController != null)
        {
            leg = characterController.gameObject.GetComponentInChildren<Leg>(true);
        }
        if (enemyController != null)
        {
            leg = enemyController.gameObject.GetComponentInChildren<Leg>(true);
        }
    }
    public void Enter()
    {
        
        leg.Attack();
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        if (!leg.IsAttacking)
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }
    }
}

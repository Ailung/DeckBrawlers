using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punching : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private Hands hand;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Punching(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
        if (characterController != null)
        {
            hand = characterController.gameObject.GetComponentInChildren<Hands>(true);
        }
        if (enemyController != null) 
        {
            hand = characterController.gameObject.GetComponentInChildren<Hands>(true);
        }
    }
    public void Enter()
    {
        Debug.Log("Entro en Punching");
        hand.Attack();
    }

    public void Exit()
    {
        Debug.Log("Salio en Punching");
    }

    public void UpdateState()
    {
        if (!hand.IsAttacking && enemyController == null)
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }
        else if (!hand.IsAttacking && enemyController != null) 
        {
            enemyController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }
    }
}

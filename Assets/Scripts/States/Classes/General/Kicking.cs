using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        leg = characterController.gameObject.GetComponentInChildren<Leg>(true);
    }
    public void Enter()
    {
        Debug.Log("Entro en Punching");
        leg.Attack();
    }

    public void Exit()
    {
        Debug.Log("Salio en Punching");
    }

    public void UpdateState()
    {
        if (!leg.IsAttacking)
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }
    }
}

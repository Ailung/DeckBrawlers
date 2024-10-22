using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour, IState
{
    private GameObject parentGameObject;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Chasing(GameObject gameObject)
    {
        parentGameObject = gameObject;
        parentGameObject.TryGetComponent<CharacterController>(out characterController);
        parentGameObject.TryGetComponent<EnemyController>(out enemyController);
    }
    public void Enter()
    {
        Debug.Log("Entro en Chasing");
    }

    public void Exit()
    {
        Debug.Log("Salio en Chasing");
    }

    public void UpdateState()
    {
        if (enemyController.PlayerDistance > enemyController.ChaseDistance)
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.idleState);
        }
        if (enemyController.PlayerDistance <= enemyController.StopDistance)
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.punchingState);
        }
    }
}

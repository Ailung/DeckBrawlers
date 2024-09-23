using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Idle : MonoBehaviour, IState
{ 
    private GameObject m_gameObject;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Idle(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
    }
    public void Enter()
    {
        Debug.Log("Entro en Idle");
    }

    public void Exit()
    {
        Debug.Log("Saliste de Idle");
    }

    public void UpdateState()
    {

        if (characterController != null) 
        {
            if (Mathf.Abs(characterController.Rb.velocity.x) > 0.01f || Mathf.Abs(characterController.Rb.velocity.y) > 0.01f)
            {
                characterController.StateMachine.TransitionTo(characterController.StateMachine.runningState);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                characterController.StateMachine.TransitionTo(characterController.StateMachine.punchingState);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                characterController.StateMachine.TransitionTo(characterController.StateMachine.kickingState);
            }
        } else
        {
            if (enemyController.PlayerDistance < enemyController.ChaseDistance && enemyController.PlayerDistance > enemyController.StopDistance)
            {
                enemyController.StateMachine.TransitionTo(enemyController.StateMachine.chasingState);
            }
        }
        


    }

}

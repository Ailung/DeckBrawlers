using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Block(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
    }
    public void Enter()
    {
        Debug.Log("entre a block");
        characterController.Rb.velocity = Vector3.zero;
        characterController.Shield(true);
    }

    public void Exit()
    {
        Debug.Log("sali de block");
        characterController.Shield(false);
    }

    public void UpdateState()
    {
        characterController.Rb.velocity = Vector3.zero;
        if (characterController != null)
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
            }
        }

    }
}

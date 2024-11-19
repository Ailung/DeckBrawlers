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
        if (enemyController.PlayerDistance > enemyController.ChasingDistance)
        {
            if (enemyController.transform.position.x < enemyController.CharacterController.transform.position.x && enemyController.IsFacingRight)
            {
                enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
                enemyController.IsFacingRight = false;
            }
            else if (enemyController.transform.position.x > enemyController.CharacterController.transform.position.x && !enemyController.IsFacingRight)
            {
                enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
                enemyController.IsFacingRight = true;
            }
            enemyController.transform.position = Vector2.MoveTowards(enemyController.transform.position, enemyController.CharacterController.transform.position, enemyController.Speed * Time.deltaTime);
        }

        if (enemyController.PlayerDistance <= enemyController.ChasingDistance)
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.punchingState);
        }
    }
}

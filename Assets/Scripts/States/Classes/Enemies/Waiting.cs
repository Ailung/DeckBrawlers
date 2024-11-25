using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : MonoBehaviour, IState
{
    private GameObject parentGameObject;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Waiting(GameObject gameObject)
    {
        parentGameObject = gameObject;
        parentGameObject.TryGetComponent<EnemyController>(out enemyController);
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        //if (enemyController.PlayerDistance > enemyController.StopDistance)
        //{
        //    if (enemyController.transform.position.x < enemyController.CharacterController.transform.position.x && enemyController.IsFacingRight)
        //    {
        //        enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
        //        enemyController.IsFacingRight = false;
        //    }
        //    else if (enemyController.transform.position.x > enemyController.CharacterController.transform.position.x && !enemyController.IsFacingRight)
        //    {
        //        enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
        //        enemyController.IsFacingRight = true;
        //    }
        //    enemyController.transform.position = Vector2.MoveTowards(enemyController.transform.position, enemyController.CharacterController.transform.position, enemyController.Speed * Time.deltaTime);
        //}
        //if (enemyController.PlayerDistance < enemyController.StopDistance - 0.5f && enemyController.PlayerDistance > enemyController.ChaseDistance)
        //{
        //    if (enemyController.transform.position.x < enemyController.CharacterController.transform.position.x && enemyController.IsFacingRight)
        //    {
        //        enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
        //        enemyController.IsFacingRight = true;
        //    }
        //    else if (enemyController.transform.position.x > enemyController.CharacterController.transform.position.x && !enemyController.IsFacingRight)
        //    {
        //        enemyController.transform.localScale = new Vector3(enemyController.transform.localScale.x * -1, enemyController.transform.localScale.y, enemyController.transform.localScale.z);
        //        enemyController.IsFacingRight = false;
        //    }
        //    enemyController.transform.position = Vector2.MoveTowards(enemyController.transform.position, enemyController.CharacterController.transform.position, enemyController.Speed * Time.deltaTime * -1 * 0.5f);
        //}
        //if (enemyController.PlayerDistance <= enemyController.ChaseDistance)
        //{
        //    enemyController.StateMachine.TransitionTo(enemyController.StateMachine.chasingState);
        //}
    }

}

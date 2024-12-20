using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Punching : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private Hands hand;
    private CharacterController characterController;
    private EnemyController enemyController;
    private float agilityStat = 0;

    public Punching(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
        if (characterController != null)
        {
            hand = characterController.gameObject.GetComponentInChildren<Hands>(true);
            agilityStat = characterController.StatAgility;
        }
        if (enemyController != null) 
        {
            hand = enemyController.gameObject.GetComponentInChildren<Hands>(true);
        }
    }
    public void Enter()
    {
        hand.Attack(agilityStat);
        if (characterController != null)
        {
            characterController.ComboList.Add("punch");
            Debug.Log("append punch");
            characterController.ResetComboTimer();
            characterController.Animation(3, hand.AttackSpeed * (agilityStat / 10) + hand.AttackSpeed);
        }
        if (enemyController != null)
        {
            Debug.Log("append punch");
            enemyController.Animation(3, hand.AttackSpeed * (agilityStat / 10) + hand.AttackSpeed);
        }
        
        
    }

    public void Exit()
    {
        Debug.Log("stop punch");
    }

    public void UpdateState()
    {
        if (!hand.IsAttacking && enemyController == null)
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }
        else if (!hand.IsAttacking && enemyController != null) 
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.chasingState);
        }
    }
}

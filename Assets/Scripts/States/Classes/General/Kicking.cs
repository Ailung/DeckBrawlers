using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Kicking : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private Leg leg;
    private CharacterController characterController;
    private EnemyController enemyController;
    private float agilityStat = 0;

    public Kicking(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
        if (characterController != null)
        {
            leg = characterController.gameObject.GetComponentInChildren<Leg>(true);
            agilityStat = characterController.StatAgility;
        }
        if (enemyController != null)
        {
            leg = enemyController.gameObject.GetComponentInChildren<Leg>(true);
        }
    }
    public void Enter()
    {
        
        leg.Attack();
        if (characterController != null)
        {
            characterController.ComboList.Add("kick");
            characterController.ResetComboTimer();
            Debug.Log("append kick");
            characterController.Animation(4, leg.AttackSpeed * (agilityStat / 10) + leg.AttackSpeed);
        }
        if (enemyController != null)
        {
            Debug.Log("append kick");
            enemyController.Animation(4, leg.AttackSpeed * (agilityStat / 10) + leg.AttackSpeed);
        }
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

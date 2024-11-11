using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FindObjectsOfType<EnemyController>().Length <= 0)
        {
            GameManager.Instance.ChangeScene("Win");
        }
    }
}

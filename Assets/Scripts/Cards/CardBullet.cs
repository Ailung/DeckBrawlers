using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBullet : MonoBehaviour
{
    CharacterController controller;

    private float speed = 24f;
    private bool shootRight;

    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    private void Start()
    {
        controller = FindObjectOfType<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (shootRight)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
    public void ShootRight(bool right,Color color)
    {
        shootRight = right;
        sr.color = color;

    }
}

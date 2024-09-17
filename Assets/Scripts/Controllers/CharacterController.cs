using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] int speed = 3;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject foot;

    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontal * speed,vertical * speed);
        transform.Translate(movement * Time.deltaTime);
    }
}

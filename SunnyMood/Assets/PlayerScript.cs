using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private new BoxCollider2D _collider;
    private float speed = 0.0f;
    private bool isJumpPressed;
    private bool isGrounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        
        
        isJumpPressed = Input.GetButtonDown("Jump");
        if (isJumpPressed && isGrounded)
        {
            //print("jump");
            isGrounded = false;
            rb.AddForce(new Vector2(0, 300));
            transform.Translate(0, 0.1f, 0);
            //rb.velocity = new Vector2(0, 5);
        }
    }

    private void FixedUpdate()
    {
        speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        rb.velocity = new Vector2(speed * 200, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground")
        {
            //print("Ground");
            isGrounded = collision.collider.bounds.min.y < transform.position.y;
            //print(isGrounded);
        }
        
    }

    
}

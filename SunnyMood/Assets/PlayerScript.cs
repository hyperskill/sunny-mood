using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;

    public SpriteRenderer sr;
    //[FormerlySerializedAs("_collider")] public BoxCollider2D collider;
    private float speed = 0.0f;
    private bool isJumpPressed;
    private bool isGrounded = true;
    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //_collider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

        isJumpPressed = Input.GetButtonDown("Jump");
        if (isJumpPressed && isGrounded)
        {
            //print("jump");
            isGrounded = false;
            animator.SetBool(IsJumping, !isGrounded);
            rb.AddForce(new Vector2(0, 300));
            transform.Translate(0, 0.1f, 0);
            //rb.velocity = new Vector2(0, 5);
        }
    }

    private void FixedUpdate()
    {
        speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        rb.velocity = new Vector2(speed * 200, rb.velocity.y);
        animator.SetFloat(Speed, Mathf.Abs(speed*200));

        if (speed < 0)
        {
            sr.flipX = true;
        }
        else if (speed > 0)
        {
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground" || collision.collider.CompareTag("Platform"))
        {
            //print("Ground");
            isGrounded = collision.collider.bounds.min.y < transform.position.y;
            animator.SetBool(IsJumping, !isGrounded);
            //print(isGrounded);
        }
        else if (collision.collider.CompareTag("Gem"))
        {
            //Destroy(collision.gameObject);
        }
        
    }

    
}

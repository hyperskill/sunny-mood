using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D r;
    private SpriteRenderer sr;
    private bool playerOnTheJump = false;
    private bool spacePressed = false;
    private float movementSpeed = 10;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!playerOnTheJump)
        {
            playerOnTheJump = true;
            animator.SetBool("Jump", playerOnTheJump);
            r.AddForce(new Vector2(0, 400));
        }
        
    }
    private void FixedUpdate()
    {
        float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(speed));
        r.velocity = new Vector2(speed * 500, r.velocity.y);
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
        if (collision.collider.name == "Ground"||collision.collider.tag=="Platform")
        {
            if (collision.collider.bounds.min.y < transform.position.y)
            {
                playerOnTheJump = false;
                animator.SetBool("Jump", playerOnTheJump);
            }
        }
    }
}

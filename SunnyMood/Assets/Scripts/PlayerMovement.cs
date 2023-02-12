using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D r;
    private bool playerOnTheJump = false;
    private bool spacePressed = false;
    private float movementSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
            spacePressed = false;
        if (Input.GetKeyDown(KeyCode.Space))
            spacePressed = true;
        
        if (spacePressed&&!playerOnTheJump)
        {
            playerOnTheJump = true;
            r.AddForce(new Vector2(0, 300));
        }
        
    }
    private void FixedUpdate()
    {
        float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        r.velocity = new Vector2(speed * 500, r.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Ground")
        {
            if(collision.collider.bounds.min.y < transform.position.y)
                playerOnTheJump = false;
        }
    }
}

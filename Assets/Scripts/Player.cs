using System;

using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player player;
    
    public Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    [ReadOnly]
    public bool grounded;

    void Awake() {
        player = this;
    }

    void FixedUpdate() {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 3.1f,
                                     LayerMask.GetMask("Walkable"));
        
        if (grounded)
        {
            float accel = Input.GetAxisRaw("Horizontal");
            accel *= speed;
            accel *= Time.deltaTime;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                float jump = jumpForce;
                jump *= Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }

            rb.AddForce(new Vector2(accel, 0), ForceMode2D.Impulse);
        }
    }
}

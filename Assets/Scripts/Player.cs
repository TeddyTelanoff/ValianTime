using UnityEngine;

public class Player: MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    public bool grounded;

    void FixedUpdate() {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, transform.lossyScale.y + 0.1f, LayerMask.GetMask("Walkable"));
        grounded = hit;
        print(hit.collider);
        
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

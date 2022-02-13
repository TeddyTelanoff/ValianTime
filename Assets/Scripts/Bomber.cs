using System;

using UnityEngine;

public class Bomber: MonoBehaviour
{
    public Rigidbody2D rb;
    
    public GameObject bomb;
    public float sightRange;
    public float shootRange;
    public float speed;

    public float adjust;
    
    [ReadOnly]
    public bool grounded;

    void FixedUpdate() {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, transform.lossyScale.y + 0.1f,
                                     LayerMask.GetMask("Walkable"));
        if (grounded)
            rb.SetRotation(Quaternion.identity);
        
        float dist = Vector2.Distance(Player.player.transform.position, transform.position);
        if (dist < sightRange)
        {
            Vector2 dir = Player.player.transform.position - transform.position;
            dir.Normalize();
            dir *= speed;
            dir *= Time.deltaTime;

            transform.position += (Vector3) dir;
        }
    }
}

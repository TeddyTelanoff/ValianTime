using System;

using UnityEngine;

public class Bomber: MonoBehaviour
{
    public Rigidbody2D rb;
    
    public GameObject bomb;
    public float sightRange;
    public float shootRange;
    public float speed;
    public float reload;
    public float pewpew;

    [ReadOnly]
    public float cooldown;
    [ReadOnly]
    public bool grounded;

    void FixedUpdate() {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, transform.lossyScale.y + 0.1f,
                                     LayerMask.GetMask("Walkable"));

        rb.freezeRotation = grounded;
        if (grounded)
            rb.SetRotation(Quaternion.identity);

        float dist = Vector2.Distance(Player.player.transform.position, transform.position);
        if (dist < sightRange)
        {
            float dir = Mathf.Sign(Player.player.transform.position.x - transform.position.x);
            if (dist < shootRange)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < 0)
                    Shoot(dir);
                return;
            }
            
            dir *= speed;
            dir *= Time.deltaTime;

            rb.velocity = new Vector2(dir, rb.velocity.y);
        }
    }

    public void Shoot(float dir) {
        cooldown = reload;
            
        var obj = Instantiate(bomb);
        obj.transform.position = transform.position + new Vector3(transform.lossyScale.x * dir, 0);
        
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * pewpew, 0), ForceMode2D.Impulse);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0, 10)]
    public float moveSpeed;
    public Vector2 direction;
    public LayerMask ground;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        // destroy after 3 seconds 
        Destroy(gameObject, 3f);
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move with rigidbody
        body.velocity = direction * moveSpeed;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f,ground);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null)
        {
            // destroy projectile
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // if projectile hits enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            // destroy enemy
            Destroy(other.gameObject);
            // destroy projectile
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            // destroy projectile
            Destroy(this.gameObject);
        }if (other.gameObject.CompareTag("Spike"))
        {
            // destroy projectile
            Destroy(this.gameObject);
        }
    }

}

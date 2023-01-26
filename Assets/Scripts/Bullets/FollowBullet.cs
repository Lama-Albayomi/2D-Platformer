using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : MonoBehaviour, IShootable
{
    public float moveSpeed;
    public LayerMask ground;
    private Rigidbody2D body;
    private Transform enemy;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }
    public void OnHit()
    {

    }

    public void OnUpdate(Vector2 direction)
    {
        // move bullet twoards enemy
        if (enemy != null)
        {
            Vector2 dir = enemy.position - transform.position;
            body.velocity = dir.normalized * moveSpeed;
        }
        else
        {
            body.velocity = direction * moveSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")){
            enemy = other.transform;
        }
    }
}

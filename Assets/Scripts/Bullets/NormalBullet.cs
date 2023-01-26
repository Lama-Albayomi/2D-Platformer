using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet :MonoBehaviour,IShootable
{
    public float moveSpeed;
    public LayerMask ground;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }
    public void OnHit()
    {

    }
    public void OnUpdate( Vector2 direction)
    {
        body.velocity = direction * moveSpeed;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f,ground);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null)
        {
            Destroy(gameObject);
        }
    }
}

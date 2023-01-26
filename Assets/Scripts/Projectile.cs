using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0, 10)]
    IShootable behavior;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponent<IShootable>();
    }

    // Update is called once per frame
    void Update()
    {
        behavior.OnUpdate(direction);
        // move with rigidbody

    }
    void OnCollisionEnter2D(Collision2D other)
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableProjectile : MonoBehaviour
{
    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // if player collides with collectable projectile, destroy collectable projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInvetory.instance.bullets[bullet.index].amount +=1;
            Destroy(gameObject);


        }
    }
}

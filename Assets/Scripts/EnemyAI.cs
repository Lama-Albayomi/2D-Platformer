using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f; 
        public LayerMask ground;
        public Transform chackPoint; // and shoot point 
        public GameObject projectile;

        private Rigidbody2D rigidbody; 
        public Collider2D triggerCollider;
        public Vector2 direction;
        bool pLayerisNear;
        
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            
            InvokeRepeating("Shoot", 0f, 0.5f);
        }

        void Update()
        {
            
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
            
            CheckIfPlayerIsInfront();
        }

        void FixedUpdate()
        {
            if(IsNotTouchingGround()|| IsTouchingWall() )
            {
                Flip();
            }
            
        }
        private void Shoot(){
            if (!pLayerisNear)return;
            // create projectile 
            GameObject bullet = Instantiate(projectile, chackPoint.position, chackPoint.rotation);
            // set direction of projectile
            bullet.GetComponent<Projectile>().direction = direction;
        }
        private void CheckIfPlayerIsInfront()
        {
            // 2d raycast 
            RaycastHit2D raycastHit = Physics2D.Raycast(chackPoint.position, direction, 5f);
            // draw raycast 
            Debug.DrawRay(chackPoint.position, direction * 5f, Color.blue);
            if (raycastHit.collider.tag== "Player" )
            {
                pLayerisNear = true;
            }
            else{
                pLayerisNear = false;
            }
        }
        // finction that retuns if raycast hi object 
        private bool IsTouchingWall()
        {
            // 2d raycast 
            RaycastHit2D raycastHit = Physics2D.Raycast(chackPoint.position, chackPoint.right, 1f,ground);
            // draw raycast 
            Debug.DrawRay(chackPoint.position, chackPoint.right * 0.5f, Color.red);
            if (raycastHit.collider != null )
            {
                return true;
            }
            else{
                return false;
            }
        }
        // function that flips the enemy if there is no ground under them
        private bool IsNotTouchingGround()
        {
            // 2d raycast 
            RaycastHit2D raycastHit = Physics2D.Raycast(chackPoint.position, -chackPoint.up, 1f,ground);
            // draw raycast 
            Debug.DrawRay(chackPoint.position, chackPoint.right * 0.5f, Color.red);
            if (raycastHit.collider == null )
            {
                return true;
            }
            else{
                return false;
            }

        }
        
        private void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            moveSpeed *= -1;
            direction = new Vector2(moveSpeed,0);
        }
    }
}
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
        public bool isIdel;

        private Rigidbody2D rigidbody; 
        public Collider2D triggerCollider;
        public Vector2 direction;
        bool pLayerisNear;
        private Animator animator;
        bool Death;
        
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            InvokeRepeating("Shoot", 0f, 0.5f);


            if (isIdel){
                
                animator.SetInteger("State", 3);
            }
            else{
                animator.SetInteger("State", 0);
            }
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            CheckIfPlayerIsInfront();
            
            if (isIdel) return;

            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);

            if(IsNotTouchingGround()|| IsTouchingWall() )
            {
                Flip();
            }
            
        }
        private void Shoot(){
            if (!pLayerisNear&&!Death)return;
            // set animator state to 1 
            animator.SetInteger("State", 1);
            // create projectile 
            GameObject bullet = Instantiate(projectile, chackPoint.position, chackPoint.rotation);
            // set direction of projectile
            bullet.GetComponent<EnemyProjectile>().direction = direction;
        }
        private void CheckIfPlayerIsInfront()
        {
            // 2d raycast 
            RaycastHit2D raycastHit = Physics2D.Raycast(chackPoint.position, direction, 5f);
            // draw raycast 
            Debug.DrawRay(chackPoint.position, direction * 5f, Color.blue);
            if (raycastHit.collider == null ) return;

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
        // corotin funtion 
        IEnumerator Die(){
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
            // restart secene
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                
                animator.SetInteger("State", 2);
                Death= true;
                Destroy(other.gameObject);
                // disable collider
                StartCoroutine(Die());
                // destroy enemy
                // destroy projectile
                //Destroy(gameObject);
            
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

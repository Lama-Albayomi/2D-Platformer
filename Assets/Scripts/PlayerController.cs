using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        public GameObject projectile;
        public Transform shootingPoint;
        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private int bulletIndex = 0;
        

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            projectile = PlayerInvetory.instance.bullets[bulletIndex].bullet;
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update(){
            if (Input.GetButton("Horizontal") && !deathState) 
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
                if (deathState) animator.SetInteger("playerState", 3); // Turn on death animation
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded)animator.SetInteger("playerState", 2); // Turn on jump animation

            if(facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
            }
            
            // if input z Shoot project Tiles
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (PlayerInvetory.instance.bullets[bulletIndex].amount<=0) return;

                PlayerInvetory.instance.bullets[bulletIndex].amount-=1;
                
                // create projectile
                GameObject projectileInstance = Instantiate(projectile, shootingPoint.position, Quaternion.identity);
                // set direction of projectile to the player direction 
                projectileInstance.GetComponent<Projectile>().direction.x = facingRight ? 1 : -1;

            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                bulletIndex++;
                if (bulletIndex >= PlayerInvetory.instance.bullets.Length)
                {
                    bulletIndex = 0;
                }
                projectile = PlayerInvetory.instance.bullets[bulletIndex].bullet;
                CanvasManager.instance.selected(bulletIndex);

            }
        }

        private void Flip(){
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround(){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }
        // corotin funtion 
        IEnumerator Death(){
            yield return new WaitForSeconds(0.5f);
            // restart secene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void OnCollisionEnter2D(Collision2D other){
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spike" || other.gameObject.tag == "EnemyProjecttile" ){
                deathState = true; // Say to GameManager that player is dead\
                StartCoroutine(Death());
                
            }
            else{
                deathState = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.tag == "Coin"){
                //gameManager.coinsCounter += 1;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "EndPoint"){
                //gameManager.Win();
            }
        }
    }
}

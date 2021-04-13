using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;
    

    public LayerMask enemyLayer;

    public int playerHealth;
    public float radius;
    public float speed;
    public float JumpForce;
    private bool isJumping = false;
    private bool doubleJump;
    private bool isAttacking;
    private bool recovery;
    


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        Attack();
    }
    
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //se não pressionar nada, retorna 0. Se pressionar direita retorna 1, esquerda -1.
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2 (movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
            
            transform.eulerAngles = new Vector3 (0,0,0);
        }
        if (movement < 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }

            
            transform.eulerAngles = new Vector3 (0,180,0);
        }
        if (movement == 0 && !isJumping && !isAttacking)
        {
            anim.SetInteger("transition", 0);
        
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
             if(!isJumping)
             {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
             }
             else if (doubleJump)
             {
                 anim.SetInteger("transition", 2);
                 rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                 doubleJump = false;
             }
            
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            anim.SetInteger("transition", 3);
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);

            if (hit != null)
            {
                
                
                if (hit.GetComponent<Slime>())
                {
                    hit.GetComponent<Slime>().OnHit();
                }

                if (hit.GetComponent<Goblin>())
                {
                    hit.GetComponent<Goblin>().OnHit();
                }
            }

            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }
    
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.layer == 8)
        {
            isJumping = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            OnHit();
        }

        if (collision.CompareTag("Coin"))
        {
            collision.GetComponent<Animator>().SetTrigger("hit");
            GameController.instance.GetCoin();
            Destroy(collision.gameObject, 0.2f);
        }
    }
    
    float recoveryCount = 2f;
    public void OnHit()
    {
       
       recoveryCount += Time.deltaTime;

       if(recoveryCount >= 2f)
       {
            anim.SetTrigger("hit");
            playerHealth--;

            recoveryCount = 0f;
            
       }
        
        if(playerHealth <= 0 && !recovery)
        {
            recovery = true;
            anim.SetTrigger("dead");
            //fazer game over screen
        }
    }
}

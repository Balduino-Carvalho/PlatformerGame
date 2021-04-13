using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool isFront;
    private Vector2 direction;
    private bool isDead;

    public bool isRight;
    public float stopDistance;

    public int health =3;
    public float speed;
    public float maxVision;
    public Transform point;
    public Transform backpoint;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
        
            if (isRight) //vira pra direita
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                
                
            }
            else //vira pra esquerda
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                
                
            }
        
    }

    


    void OnMove()
    {
        if(isFront && !isDead)
        {
            anim.SetInteger("transition", 1);
            if (isRight) //vira pra direita
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2 (speed, rig.velocity.y);
                
            }
            else //vira pra esquerda
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2 (-speed, rig.velocity.y);
                
            }
        }
    }
    
    
    void FixedUpdate()
    {
        
        GetPlayer();
        OnMove();

        
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null && !isDead)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if(distance <= stopDistance) //aqui ele vai atacar melee
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;
                    anim.SetInteger("transition", 2);

                    hit.transform.GetComponent<Player>().OnHit();
                }

            }
        }

        RaycastHit2D backHit = Physics2D.Raycast(backpoint.position, -direction, maxVision);

        if(backHit.collider != null)
        {
            if(backHit.transform.CompareTag("Player"))
            {
                isRight = !isRight;
                isFront = true;
            }
        }

        
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");
        health--;
        if (health <= 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("death");
            Destroy(gameObject, 1f);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(backpoint.position, -direction * maxVision);
        
    }
}

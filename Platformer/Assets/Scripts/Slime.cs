using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public int health;
    public float speed;
    public Transform point;
    public float radius;

    public LayerMask layer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        rig.velocity = new Vector2 (speed, rig.velocity.y);
        OnCollision();
    }

    void OnCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);

        if (hit != null)
        {
            //só é chamado quando o inimigo bate num obj que tenha a layer indicada
            speed = -speed;
            if(transform.eulerAngles.y ==0)
            {
            transform.eulerAngles = new Vector3 (0, 180, 0);
            }
            else
            {
            transform.eulerAngles = new Vector3 (0, 0, 0);
            }
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");
        health--;

        if (health <=0)
        {
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

}

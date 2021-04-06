using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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
            transform.eulerAngles = new Vector3 (0,0,0);
        }
        if (movement < 0)
        {
            transform.eulerAngles = new Vector3 (0,180,0);
        }
    }
}

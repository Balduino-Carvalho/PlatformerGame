using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtom : MonoBehaviour
{
    private Animator anim;
    public Animator barrierAnim;
    
    
   void Start()
   {
       anim = GetComponent<Animator>();
   }
   void OnPressed()
   {
       anim.SetBool("isPressed", true);
       barrierAnim.SetBool("down", true);

   }

   void OnExit()
   {
       anim.SetBool("isPressed", false);
       barrierAnim.SetBool("down", false);
   }

    //retorna enquanto o objeto estiver em colisão com o outro
   private void OnCollisionStay2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stone"))
       {
           OnPressed();
       }
   }

   //retorna quando a colisão acaba
   private void OnCollisionExit2D (Collision2D collision)
   {
       if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stone"))
       {
           OnExit();
       }
   }
}

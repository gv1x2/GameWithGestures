using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float viteza;

    [SerializeField] private float jumpPower;
    
    private Rigidbody2D corp;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float inputOrizontal;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    

    private void Awake()
    {//Prinde referinte la rigidbody2D and Animation
        corp = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    /*
         private void Update()
         {

             inputOrizontal = Input.GetAxis("Horizontal");



             //schimbarea directiei caracteruli
             if (inputOrizontal > 0.01f)
                 transform.localScale = Vector3.one;
             else if (inputOrizontal < -0.01f)
                 transform.localScale = new Vector3(-1, 1, 1);

             if (Input.GetKey(KeyCode.Space) && isGrounded())
                 Jump();
             //Set animator parametri
             anim.SetBool("run", inputOrizontal != 0);
             anim.SetBool("grounded", isGrounded());

             // Saritura pe pereti logica
             if (wallJumpCooldown > 0.2f)
             {
                 corp.linearVelocity = new Vector2(inputOrizontal * viteza, corp.linearVelocity.y);

                 if (onWall() && !isGrounded())
                 {
                     corp.gravityScale = 0;
                     corp.linearVelocity = Vector2.zero;
                 }
                 else
                     corp.gravityScale = 7;
             }
             else
                 wallJumpCooldown += Time.deltaTime;

             if (Input.GetKey(KeyCode.Space))
                 Jump();
         }
    */


    
     private void Update()
     {
         string gesture = readPython.CurrentGesture;

         if (gesture == "CLOSED")
             inputOrizontal = 1f; // Move right
         else
             inputOrizontal = 0f;

         if (gesture == "POINT" && isGrounded())
             Jump();

         // Flip direction
         if (inputOrizontal > 0.01f)
             transform.localScale = Vector3.one;
         else if (inputOrizontal < -0.01f)
             transform.localScale = new Vector3(-1, 1, 1);

         anim.SetBool("run", inputOrizontal != 0);
         anim.SetBool("grounded", isGrounded());

         if (wallJumpCooldown > 0.2f)
         {
             corp.linearVelocity = new Vector2(inputOrizontal * viteza, corp.linearVelocity.y);

             if (onWall() && !isGrounded())
             {
                 corp.gravityScale = 0;
                 corp.linearVelocity = Vector2.zero;
             }
             else
             {
                 corp.gravityScale = 7;
             }
         }
         else
         {
             wallJumpCooldown += Time.deltaTime;
         }
     }
    

    private void Jump()
    {
        if(isGrounded() )
        {
            corp.linearVelocity = new Vector2(corp.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (inputOrizontal == 0)
            {
                corp.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                corp.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCooldown = 0;
            
        }  
       
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null ;
    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return inputOrizontal == 0 && isGrounded() && !onWall();
    }

}

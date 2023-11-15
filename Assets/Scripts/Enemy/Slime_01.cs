using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_01 : MonoBehaviour
{
   [SerializeField] float moveSpeed = 1f;
   Rigidbody2D rigidbody;
   BoxCollider2D boxCollider;
   private Animator animator;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
         animator = GetComponent<Animator>();
    }

   
    void Update()
    {
         // Update is called once per frame

         if(IsFacingRight()){
            rigidbody.velocity=new Vector2(moveSpeed,0f);

             


         }else
         {
             rigidbody.velocity=new Vector2(-moveSpeed,0f);
            
         }
        
    }
    private void OnTriggerEnter2D(Collider2D collision){

       transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.velocity.x)),transform.localScale.y);
 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
             animator.Play("SlimeDie");
              StartCoroutine(DeathActiveAfterDelay(1.25f));
              Death();
       
        }
    }

    
    private bool IsFacingRight()
    {
 return transform.localScale.x>0.001f;

 }
    private void Death()
    {
      //  animator.Play("SlimeDie");
        Destroy(gameObject);
    }
     IEnumerator DeathActiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }


}


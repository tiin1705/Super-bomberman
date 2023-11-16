using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeY : MonoBehaviour
{
   [SerializeField] float moveSpeed = 1f;
   Rigidbody2D rigidbody;
   BoxCollider2D boxCollider;
   int A = 1;
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
            rigidbody.velocity=new Vector2(0f,moveSpeed);

             


         }else
         {
             rigidbody.velocity=new Vector2(0f,-moveSpeed);
            
         }
        
    }
    private void OnTriggerEnter2D(Collider2D collision){

      // transform.localScale = new Vector2(transform.localScale.x,-(Mathf.Sign(rigidbody.velocity.y)));

       A = -A;
         if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
             animator.Play("SlimeDie");
              StartCoroutine(DeathActiveAfterDelay(1.25f));
              Death();
       
        }

    }
    private bool IsFacingRight()
    {
 return A>0;

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

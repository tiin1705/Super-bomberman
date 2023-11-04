using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Players : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;

    public LayerMask HardObjectLayer;
    public LayerMask SoftObjectLayer;
    private bool isMoving;
    private Vector2 input;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var playerPos = transform.position;
                playerPos.x += input.x * 0.5f;
                playerPos.y += input.y * 0.5f;

                if (isWalkable(playerPos))
                    StartCoroutine(Move(playerPos));

            }
        }
        animator.SetBool("isMoving", isMoving);
    }

  /*  void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(x, y, 0).normalized;
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("runx", Mathf.Abs(moveInput.x));
        animator.SetFloat("runy", moveInput.y);
        animator.SetFloat("run-y", Mathf.Abs(moveInput.y));

        if (moveInput.x != 0)
        {
            animator.SetFloat("runx", Mathf.Abs(moveInput.x));
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        else if (moveInput.y > 0)
        {
            animator.SetFloat("runy", moveInput.y);
            animator.SetFloat("run-y", -1);
        }

        else if (moveInput.y < 0)
        {
            animator.SetFloat("run-y", Mathf.Abs(moveInput.y));
        }
    }*/

    IEnumerator Move(Vector3 playerPos)
    {
        isMoving = true;
        while ((playerPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPos, 0.5f);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            yield return null;

        }
        transform.position = playerPos;
        isMoving = false;
    }
    

    
    //   private void OnTriggerEnter2D(Collider2D collision)
    // { 
    //     string Objectname =collision.attachedRigidbody.gameObject.name ;
    //     if (collision.gameObject.CompareTag("wood"))
    //     {
         
    //        Destroy(GameObject.Find(Objectname));
    //        Debug.Log(">>>>>>>>>>.>>>>>>>>>>>>>>.");

    //     }

       
   // }

    private bool checkHardObject(Vector3 playerPos)
    {
        if(Physics2D.OverlapCircle(playerPos, 0.01f, HardObjectLayer) != null)
        {
            return false;
        }
        return true;
    }
    private bool checkSoftObject(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.01f, SoftObjectLayer) != null)
        {
            return false;
        }
        return true;
    }

    private bool isWalkable(Vector3 playerPos)
    {
        if(checkHardObject(playerPos) && checkSoftObject(playerPos))
        {
            return true;
        }
        return false;
    }
  


}

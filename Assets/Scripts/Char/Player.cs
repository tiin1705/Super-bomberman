using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 moveInput;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    void MoveInput()
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




}

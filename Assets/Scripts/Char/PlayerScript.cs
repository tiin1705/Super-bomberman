using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerScript : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;

    public LayerMask HardObjectLayer;
    public LayerMask SoftObjectLayer;
    public LayerMask ForeGroundLayer;
    public LayerMask BombLayer;
    public AnimatedSpriteRenderer spriteRendererDeath;

    //private int score = 0;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    private bool isDeath = false;
    private bool isMoving;
    private Vector2 input;

    public bool canReceiveInput = true;

    private PlayerID playerID;
    private int playerScore = 0;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Gán PlayerIdScript cho player khi bắt đầu game
        playerID = new PlayerID();

        // Lấy PlayerId từ PlayerIdScript
        string playerId = playerID.PlayerId;

        // Hiển thị PlayerId hoặc làm bất cứ điều gì khác bạn cần
        Debug.Log("Player ID: " + playerId);

        // Nếu có điểm lưu trữ trước đó, lấy điểm đó
        if (PlayerPrefs.HasKey(playerId + "_Score"))
        {
            playerScore = PlayerPrefs.GetInt(playerId + "_Score");
            Debug.Log("Player Score loaded: " + playerScore);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canReceiveInput) { 
        if (!isMoving)
        {
            input.x = GetAxisWithKey("Horizontal");
            input.y = GetAxisWithKey("Vertical");
                if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var playerPos = transform.position;
                playerPos.x += input.x ;
                playerPos.y += input.y ;

                if (isWalkable(playerPos))
                    StartCoroutine(Move(playerPos));

            }
        }
        }
        animator.SetBool("isMoving", isMoving);
        if (isDeath)
        {
            Debug.Log("isDeath");
            transform.position = transform.position;
            canReceiveInput = false;
            animator.SetBool("isDeath", isDeath);
            StartCoroutine(DeathActiveAfterDelay(1.25f));
        }
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

    float GetAxisWithKey(string axisName)
    {
        if (Input.GetKey(inputLeft) && axisName == "Horizontal")
        {
            return -1f;
        }
        else if(Input.GetKey(inputRight) && axisName == "Horizontal")
        {
            return 1f;
        }
        else if(Input.GetKey(inputUp) && axisName == "Vertical")
        {
            return 1f;
        }else if(Input.GetKey(inputDown) && axisName == "Vertical")
        {
            return -1f;
        }
        return 0f;
    }
    IEnumerator Move(Vector3 playerPos)
    {
        isMoving = true;
        while ((playerPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
         //   Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPos, 0.5f);
            transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
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
    private bool checkForeGround(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.01f, ForeGroundLayer) != null)
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
    private bool checkBomb(Vector3 playerPos)
    {
        if (Physics2D.OverlapCircle(playerPos, 0.01f, BombLayer) != null)
        {
            return false;
        }
        return true;
    }

    private bool isWalkable(Vector3 playerPos)
    {
        if(checkHardObject(playerPos) && checkSoftObject(playerPos) & checkForeGround(playerPos) & checkBomb(playerPos))
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Death();
        }
    }
    
    private void Death()
    {
        isDeath = true;
    }
    IEnumerator DeathActiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Player Score: " + playerScore);

        // Lưu điểm vào PlayerPrefs
        PlayerPrefs.SetInt(playerID.PlayerId + "_Score", playerScore);
        PlayerPrefs.Save(); // Lưu thay đổi
    }



}

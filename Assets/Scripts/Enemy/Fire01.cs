using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Fire01 : MonoBehaviour
{
    [SerializeField] public Tilemap destructibleTiles;
    [SerializeField] float moveSpeed = 1f;
    public LayerMask explosionLayerMask;
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

        if (IsFacingRight())
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0f);




        }
        else
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0f);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)


    {
         if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            animator.Play("SlimeDie");
          
            Death();

        }
          Vector2 position = transform.position;

          transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.velocity.x)),transform.localScale.y);
           if (IsFacingRight())
        {
             Explode(position, Vector2.left);

        }
        else
        {
           Explode(position, Vector2.right);

        }

       

      

      
       



    }
    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);
        destructibleTiles.SetTile(cell, null);



    }
    private void Explode(Vector2 position, Vector2 direction)
    {


        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);

            return;
        }



        Explode(position, direction);
    }



    private bool IsFacingRight()
    {
        return transform.localScale.x > 0.001f;

    }
    private void Death()
    {
        //  animator.Play("SlimeDie");
        Destroy(gameObject);
    }
    


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder = false;
    private bool isClimbing = false;

    [SerializeField] private Rigidbody2D rb;

  

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical") * speed;

        if(isLadder)
        {
            //Debug.Log("Ladder");
            vertical = Input.GetAxisRaw("Vertical") * speed;
        }

        
        //if (isLadder && Mathf.Abs(vertical) > 0f)
        //{
        //    Debug.Log("Ladder");
        //    isClimbing = true;
        //}
    }

    private void FixedUpdate()
    {
        //if (isClimbing)
        //{
        //    Debug.Log("Baba");
        //    rb.gravityScale = 0f;
        //    rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        //}
        //else
        //{
        //    rb.gravityScale = 3f;
        //}
        if(isLadder)
        {
            //Debug.Log(vertical);
            rb.isKinematic = true;
            rb.velocity = new Vector2(rb.velocity.x, vertical);
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            isLadder = false;
     
        }
    }


}

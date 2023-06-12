using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteR;
    private BoxCollider2D _coll;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
   


    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 1f;

//    private bool facingRight = true;
    private Vector2 movement;

    private enum MovementState { idle, running, jumping, falling}
 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteR = GetComponent<SpriteRenderer>();
        _coll = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        dirX = Input.GetAxisRaw ("Horizontal");
     
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);

        if (Input.GetKeyDown("space") && IsGrounded())
        {
            jumpSoundEffect.Play();
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;

        bool flipped = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            //flip();
            state = MovementState.running;
            //_spriteR.flipX = false;
          
        }
        else if (dirX < 0f)
        {
            //flip();
            state = MovementState.running;
            //_spriteR.flipX = true;
          
        }
        else
        {
            state = MovementState.idle;
        }


        if(_rb.velocity.y > .1f)
        {
            if (IsGrounded())
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.jumping;
            }
           
        }
        else if(_rb.velocity.y < -.1f)
        {
            if (IsGrounded())
            {
                state = MovementState.running;
            }
            else
            {
                state = MovementState.falling;
            }
            
        }

        _anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void FixedUpdate()
    {
        
    }


}

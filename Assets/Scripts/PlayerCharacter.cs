using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    BoxCollider2D boxCollider;
    private float boxColliderOffset;

    [SerializeField] BoxCollider2D footCollider;
    private float footColliderOffset;

    private float horizontal;
    private float move;
    [SerializeField] float jump;
    [SerializeField] float speed;

    bool isGrounded;
    private int nmbOfJump = 0;

    [SerializeField] Animator playerAnim;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxColliderOffset = boxCollider.offset.x;
        footColliderOffset = footCollider.offset.x;
    }
    
    void Update()
    {
        playerAnim.SetInteger("Jump",nmbOfJump);
        horizontal = Input.GetAxis("Horizontal");
        move = speed * horizontal;
        
        if (Input.GetButtonDown("Jump") && nmbOfJump >=1)
        {
            body.velocity = new Vector2(body.velocity.x,jump);
            if (nmbOfJump == 1)
            {
                nmbOfJump = 0;
            }
        }

        if (body.velocity.x == 0.0f)
        {
            playerAnim.SetBool("Run",false);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(move,body.velocity.y);
        if (body.velocity.x < 0.0f)
        {
            sprite.flipX = true;
            boxCollider.offset = new Vector2(-boxColliderOffset,boxCollider.offset.y);
            footCollider.offset = new Vector2(-footColliderOffset,footCollider.offset.y);
            playerAnim.SetBool("Run",true);
        }
        if (body.velocity.x > 0.0f)
        {
            sprite.flipX = false;
            boxCollider.offset = new Vector2(boxColliderOffset,boxCollider.offset.y);
            footCollider.offset = new Vector2(footColliderOffset,footCollider.offset.y);
            playerAnim.SetBool("Run",true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            nmbOfJump = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            nmbOfJump--;
        }
    }
}

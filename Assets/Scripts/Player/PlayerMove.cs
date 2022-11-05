using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField, Range(0f, 5f)] private float Movespeed;

    [SerializeField] private Vector2 inputValue;
    [SerializeField] private Vector2 moveVec;

    public bool XFlip => (moveVec.x > 0) ? true : false;
    public int XDirection => (moveVec.x > 0) ? 1 : -1;

    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        moveVec.x = 1;
    }

    private void Update()
    {
        if(inputValue != Vector2.zero)
        {
            moveVec = new Vector2 (inputValue.x, inputValue.y);
            anim.SetBool("isWalk", true);

            sprite.flipX = XFlip;
            rigid.AddForce(moveVec * Movespeed , ForceMode2D.Impulse);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        this.inputValue = inputValue.Get<Vector2>();
    }
}

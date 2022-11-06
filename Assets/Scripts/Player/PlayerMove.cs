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
    [SerializeField] float wind_maxspeed;

    public bool XFlip => (moveVec.x > 0) ? true : false;
    public int XDirection => (moveVec.x > 0) ? 1 : -1;

    private Animator anim;
    private SpriteRenderer sprite;
    float left_maxspeed;
    float right_maxspeed;
    private void Awake()
    {
        left_maxspeed = Movespeed;
        right_maxspeed = Movespeed;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        moveVec.x = 1;
    }

    private void Update()
    {
        if (rigid.velocity.x < left_maxspeed * (-1))//¿ÞÂÊ
        {
            rigid.velocity = new Vector2(left_maxspeed * (-1), rigid.velocity.y);
        }
        if (rigid.velocity.x > right_maxspeed)//¿À¸¥ÂÊ
        {
            rigid.velocity = new Vector2(right_maxspeed, rigid.velocity.y);
        }
        if (inputValue != Vector2.zero)
        {
            moveVec = new Vector2 (inputValue.x, inputValue.y);
            anim.SetBool("isWalk", true);

            sprite.flipX = XFlip;
        }
        else
        {
            AudioManager.Instance.StopSFX(SFXNames.FootStep);
            anim.SetBool("isWalk", false);
        }
        if (GetComponent<PlayerFly>().FlyCount == 1)
        {
            AudioManager.Instance.StopSFX(SFXNames.FootStep);
            float roll = rigid.velocity.x > 0 ? 24 * (-rigid.velocity.x / right_maxspeed) : 24 * (-rigid.velocity.x / left_maxspeed);
            transform.rotation = Quaternion.Euler(0f, 0f, roll);
        }
        else
        {
            
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        this.inputValue = inputValue.Get<Vector2>();
    }
  

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(GetComponent<PlayerFly>().FlyCount == 0)
                AudioManager.Instance.PlaySFX(SFXNames.FootStep);
            rigid.AddForce(Vector2.left, ForceMode2D.Impulse);
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (GetComponent<PlayerFly>().FlyCount == 0)
                AudioManager.Instance.PlaySFX(SFXNames.FootStep);
            rigid.AddForce(Vector2.right , ForceMode2D.Impulse);
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WindBlow windblow))
        {
            switch (windblow.Direction)
            {
                case 2:
                    left_maxspeed -= wind_maxspeed;
                    right_maxspeed += wind_maxspeed;
                    break;
                case 3:
                    left_maxspeed += wind_maxspeed;
                    right_maxspeed -= wind_maxspeed;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        left_maxspeed = Movespeed;
        right_maxspeed = Movespeed;
    }

}

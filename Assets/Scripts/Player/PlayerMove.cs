using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerFly playerFly;
    
    Rigidbody2D rigid;
    
    [SerializeField, Range(-5f, 5f)] public float Movespeed=0;
    [SerializeField, Range(0f, 5f)] public float Flyforce;

    private Vector2 moveVec;
    [SerializeField] float wind_maxspeed;

    [SerializeField] private Transform breathPos;

    public bool XFlip => (moveVec.x > 0) ? true : false;
    public int XDirection => (moveVec.x > 0) ? 1 : -1;
    public bool Abnormalstatus=false;

    private Animator anim;
    private SpriteRenderer sprite;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        playerFly = GetComponent<PlayerFly>();

        moveVec.x = 1;
    }

    private void Start()
    {
        breathPos = transform.Find("BreathPos");
    }

    private void Update()
    {
        if (rigid.velocity.y > Flyforce)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Flyforce);
        }
        if (rigid.velocity.x > Movespeed)//����
        {
            rigid.velocity = new Vector2(Movespeed , rigid.velocity.y);
        }
        if (rigid.velocity.x < Movespeed)//������
        {
            rigid.velocity = new Vector2(Movespeed, rigid.velocity.y);
        }
        if (rigid.velocity != Vector2.zero)
        {
            moveVec = rigid.velocity;
            anim.SetBool("isWalk", true);

            sprite.flipX = XFlip;
        }
        else
        {
            AudioManager.Instance.StopSFX(SFXNames.FootStep);
            anim.SetBool("isWalk", false);
        }
        if (playerFly.FlyCount == 1)
        {
            AudioManager.Instance.StopSFX(SFXNames.FootStep);
            float roll = rigid.velocity.x > 0 ? 24 * (-rigid.velocity.x / Movespeed) : 24 * (-rigid.velocity.x / Movespeed*-1);
            transform.rotation = Quaternion.Euler(0f, 0f, roll);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    
    // Update is called once per frame
    public void JoyMove()
    {
        if (Movespeed > 0)
        {
           // sprite.flipX = true;
            if (playerFly.FlyCount == 0)
                AudioManager.Instance.PlaySFX(SFXNames.FootStep);
            rigid.AddForce(Vector2.left, ForceMode2D.Impulse);
            breathPos.localPosition = new Vector3(-0.32f, -0.105f, 0f);
        }
        if (Movespeed < 0)
        {
          //  sprite.flipX = false;
            if (playerFly.FlyCount == 0)
                AudioManager.Instance.PlaySFX(SFXNames.FootStep);
            rigid.AddForce(Vector2.right , ForceMode2D.Impulse);
            breathPos.localPosition = new Vector3(0.20f, -0.105f, 0f);
        }
    }
}

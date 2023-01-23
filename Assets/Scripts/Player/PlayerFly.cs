using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField, Range(0f, 5f)] private float Flyforce;
    [SerializeField, Range(0f, 100f)] public float FlyPower;
    public int FlyCount=0;
    public GameObject Fire;
    public bool Isfly;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.y > Flyforce)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Flyforce);
        }
        if (FlyCount == 0 && FlyPower < 100)
        {
            FlyPower = FlyPower + Time.deltaTime * 10 >= 100 ? 100 : FlyPower + Time.deltaTime * 10;
        }
        if (Isfly)
        {
            if (rigid.gravityScale == 0.3f)
                rigid.gravityScale = 1;
            if (FlyPower - Time.deltaTime * 3 >= 0)
            {
                AudioManager.Instance.PlaySFX(SFXNames.Fly);
                FlyPower -= Time.deltaTime * 5;
                Fire.gameObject.SetActive(true);
                rigid.AddForce(Vector2.up, ForceMode2D.Impulse);
                
            }
        }
        else
        {
            AudioManager.Instance.StopSFX(SFXNames.Fly);
            Fire.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.gravityScale = 0.3f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (!Input.GetKey(KeyCode.S))
            {
                AudioManager.Instance.PlaySFX(SFXNames.Land);
            }
            FlyCount = 0;
            anim.SetBool("isFly", false);
            rigid.gravityScale = 1f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            FlyCount = 0;
            anim.SetBool("isFly", false);
            rigid.gravityScale = 1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            FlyCount = 1;
            anim.SetBool("isFly", true);
            AudioManager.Instance.StopSFX(SFXNames.FootStep);
        }
    }
}

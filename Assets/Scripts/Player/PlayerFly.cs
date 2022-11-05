using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField, Range(0f, 5f)] private float Flyforce;
    [SerializeField, Range(0f, 100f)] public float FlyPower;
    public int FlyCount=0;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FlyCount == 0 && FlyPower < 100)
        {
            FlyPower = FlyPower + Time.deltaTime >= 100 ? 100 : FlyPower + Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (FlyPower - Time.deltaTime * 3 >= 0)
            {
                FlyPower -= Time.deltaTime * 3;
                rigid.AddForce(Vector2.up * Flyforce, ForceMode2D.Impulse);
            }
            else
            {
                rigid.gravityScale = 0.3f;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            FlyCount = 0;
            rigid.gravityScale = 1f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Ground")
        {
            FlyCount = 1;
        }
    }
}

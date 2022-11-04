using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charatorfly : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField, Range(0f, 5f)] private float Flyforce;
    [SerializeField, Range(0f, 100f)] private float FlyPower;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (FlyPower - Time.deltaTime * 3 >= 0)
            {
                //FlyPower-=
                rigid.AddForce(Vector2.up * Flyforce, ForceMode2D.Impulse);
            }
        }
    }
}

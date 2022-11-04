using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charatormove : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField, Range(0f, 5f)] private float Movespeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(Vector2.left * Movespeed , ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector2.right * Movespeed  , ForceMode2D.Impulse);

        }
        
    }
}

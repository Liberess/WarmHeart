using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBlock : MonoBehaviour
{
    public GameObject Block;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Block == null)
        {
            rigid.gravityScale = 1;
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

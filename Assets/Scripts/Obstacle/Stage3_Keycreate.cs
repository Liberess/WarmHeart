using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Keycreate : MonoBehaviour
{
    public GameObject Goal_obj;
    public GameObject Key;
    [SerializeField] Vector2 Key_position;

    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Goal_obj)
        {
            Instantiate(Key,Key_position ,Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    public float Power;
    GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            player.GetComponent<PlayerMove>().Abnormalstatus = true;
            float x_direction = transform.position.x - collision.transform.position.x;
            float y_direction = transform.position.y - collision.transform.position.y;
            player.GetComponent<PlayerMove>().Movespeed = -x_direction * 2;
            player.GetComponent<PlayerMove>().Flyforce = -y_direction * 2;
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * y_direction * Power, ForceMode2D.Impulse);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * x_direction * Power, ForceMode2D.Impulse);
            Invoke("delete", 1f);
        }
    }
    void delete()
    {
        player.GetComponent<PlayerMove>().Abnormalstatus = false;
        player.GetComponent<PlayerMove>().Flyforce = 3;
        Destroy(gameObject);
    } 
}

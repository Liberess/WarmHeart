using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IItem
{
    public void OnUse()
    {
        Debug.Log("Destroy KeyItem");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.TryGetComponent(out ))
        GameManager.Instance.GetKey();
    }
}

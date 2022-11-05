using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IItem
{
    [SerializeField, Range(0, 500)] private int cureAmount = 10;

    public void OnUse()
    {
        Debug.Log("Destroy KeyItem");
        GameManager.Instance.PickupKey();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.CureHealthPoint(cureAmount);
            OnUse();
        }    
    }
}

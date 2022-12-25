using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlow : MonoBehaviour
{
    [SerializeField] EDirectionType dircType;
    public EDirectionType DircType { get => dircType; }
    [SerializeField] float windPower = 0.1f;
    private Vector2 dircVec = Vector2.zero;

    private void Start()
    {
        switch (dircType)
        {
            case EDirectionType.Up: dircVec = Vector2.up; break;
            case EDirectionType.Down: dircVec = Vector2.down; break;
            case EDirectionType.Left: dircVec = Vector2.left; break;
            case EDirectionType.Right: dircVec = Vector2.right; break;
        }
    }

    [ContextMenu("Update WindTrap Rotation")]
    private void UpdateWindTrapRotation()
    {
        switch(dircType) 
        {
            case EDirectionType.Up: transform.rotation = Quaternion.Euler(0f, 0f, 0f); break;
            case EDirectionType.Down: transform.rotation = Quaternion.Euler(0f, 0f, 180f); break;
            case EDirectionType.Left: transform.rotation = Quaternion.Euler(0f, 0f, 90f); break;
            case EDirectionType.Right: transform.rotation = Quaternion.Euler(0f, 0f, -90f); break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerFly playerFly))
        {
            if(playerFly.FlyCount > 0)
            {
                var playerRigid = collision.GetComponent<Rigidbody2D>();
                playerRigid.AddForce(dircVec * windPower, ForceMode2D.Impulse);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float fireMoveSpeed = 3f;
    [SerializeField] protected float destoryTime = 10f;
    protected int direction;
    protected DamageMessage dmgMsg;

    private SpriteRenderer sprite;

    protected abstract void OnEnter(Collider2D collider);

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.Translate(3, 0, 0);
        Destroy(gameObject, destoryTime);
    }

    private void FixedUpdate()
    {
        transform.Translate((Vector2.right * direction) * fireMoveSpeed * Time.deltaTime);
    }

    public void SetupBullet(DamageMessage _dmgMsg, int _direction)
    {
        dmgMsg = _dmgMsg;
        direction = _direction;
        sprite.flipX = (direction > 0) ? false : true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnEnter(collider);
    }
}
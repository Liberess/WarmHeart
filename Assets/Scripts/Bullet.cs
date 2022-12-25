using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Bullet : MonoBehaviour
{
    private GameManager gameMgr;

    [SerializeField] protected float fireMoveSpeed = 3f;
    [SerializeField] protected float destoryTime = 10f;

    [SerializeField] protected GameObject effectPrefab;
    protected Vector3 dircVec;
    protected DamageMessage dmgMsg;

    protected abstract void OnEnter(Collider2D collider);

    private void Update()
    {
        if (!gameMgr.IsGamePlay)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * fireMoveSpeed * Time.deltaTime);
    }

    public void SetupBullet(DamageMessage _dmgMsg, Vector3 _dircVec)
    {
        dmgMsg = _dmgMsg;
        dircVec = _dircVec;

        gameMgr = GameManager.Instance;
        Destroy(gameObject, destoryTime);

        if (_dircVec == Vector3.up)
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        else if (_dircVec == Vector3.down)
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        else if (_dircVec == Vector3.left)
            transform.rotation = Quaternion.Euler(0f, -180, 0f);
        else if (_dircVec == Vector3.right)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        /*        switch (dircType)
                {
                    case DirectionType.Up:
                        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                        break;
                    case DirectionType.Down:
                        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                        break;
                    case DirectionType.Left:
                        transform.rotation = Quaternion.Euler(0f, -180, 0f);
                        transform.Translate(1, 0, 0);
                        break;
                    case DirectionType.Right:
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        break;
                }*/
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (effectPrefab != null)
            Instantiate(effectPrefab, transform.position, Quaternion.identity);

        OnEnter(collider);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRoket : MonoBehaviour
{
    private GameManager gameMgr;

    [SerializeField] private GameObject iceBulletPrefab;
    [SerializeField, Range(0f, 10f)] private float responTime = 2f;

    private void Start()
    {
        gameMgr = GameManager.Instance;

        StartCoroutine(CreateBulletCo());
    }

    private IEnumerator CreateBulletCo()
    {
        WaitForSeconds delay = new WaitForSeconds(responTime);

        DamageMessage dmg;
        dmg.damager = gameObject;
        dmg.damageAmount = 10;
        dmg.hitPoint = transform.position;

        while (true)
        {
            if (!gameMgr.IsGamePlay)
                break;

            yield return delay;

            var bullet = Instantiate(iceBulletPrefab, transform.position, Quaternion.identity)
                .GetComponent<IceBullet>();
            bullet.SetupBullet(dmg, (transform.rotation.y == 1f ? 1 : -1));
        }

        yield return null;
    }
}

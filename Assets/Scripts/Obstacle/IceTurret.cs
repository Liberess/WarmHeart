using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IceTurret : MonoBehaviour
{
    private GameManager gameMgr;

    [SerializeField] private GameObject iceBulletPrefab;
    [SerializeField, Range(0f, 10f)] private float responTime = 2f;
    [SerializeField] private EDirectionType dircType;

    [SerializeField] private Transform shotPos;

    private void Start()
    {
        gameMgr = GameManager.Instance;

        shotPos = transform.Find("ShotPos");

        StartCoroutine(CreateBulletCo());
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * 2f);
    }
#endif

    private IEnumerator CreateBulletCo()
    {
        WaitForSeconds delay = new WaitForSeconds(responTime);

        DamageMessage dmg;
        dmg.damager = gameObject;
        dmg.damageAmount = 10;
        dmg.hitPoint = shotPos.position;

        while (true)
        {
            if (!gameMgr.IsGamePlay)
                break;

            yield return delay;

            var bullet = Instantiate(iceBulletPrefab, shotPos.position, Quaternion.identity)
                .GetComponent<IceBullet>();
            bullet.SetupBullet(dmg, transform.up);
        }

        yield return null;
    }
}

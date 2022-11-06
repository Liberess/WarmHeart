using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerFly PlayerFly { get; private set; }
    public PlayerMove PlayerMove { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerInteract PlayerInteract { get; private set; }

    private void Awake()
    {
        PlayerFly = GetComponent<PlayerFly>();
        PlayerMove = GetComponent<PlayerMove>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInteract = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        PlayerHealth.DeathAction -= () => OnDeath();
        PlayerHealth.DeathAction += () => OnDeath();
    }

    private void OnEnable()
    {
        SetChildObjectActive(true);
        SetComponentsActive(true);
    }

    private void OnDeath()
    {
        SetChildObjectActive(false);
        SetComponentsActive(false);
    }

    private void SetChildObjectActive(bool value)
    {
        transform.rotation = Quaternion.identity;
        transform.GetChild(0).gameObject.SetActive(!value);

        for (int i = 1; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(value);
    }

    public void SetComponentsActive(bool value)
    {
        PlayerFly.enabled = value;
        PlayerMove.enabled = value;
        PlayerAttack.enabled = value;
        PlayerHealth.enabled = value;
        PlayerInteract.enabled = value;
    }
}

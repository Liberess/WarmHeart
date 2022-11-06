using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public PlayerFly PlayerFly { get; private set; }
    public PlayerMove PlayerMove { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerInteract PlayerInteract { get; private set; }

    [SerializeField] private GameObject background;
    [SerializeField] private GameObject darkBackground;

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
        SetDeathStage(false);
        SetChildObjectActive(true);
        SetComponentsActive(true);
    }

    private void OnDeath()
    {
        GameManager.Instance.OnGameOverAction();

        SetDeathStage(true);
        SetChildObjectActive(false);
        SetComponentsActive(false);
    }

    private void SetDeathStage(bool value)
    {
        if (value)
        {
            background.SetActive(false);
            darkBackground.SetActive(true);

            GameObject.Find("== Environment Group ==").transform.Find("MapGlobalLight2D").
                GetComponent<Light2D>().intensity = 0.1f;

            GameObject.Find("== Environment Group ==").transform.Find("ObstacleGlobalLight2D").
                GetComponent<Light2D>().intensity = 0.1f;
        }
        else
        {
            background.SetActive(true);
            darkBackground.SetActive(false);

            GameObject.Find("== Environment Group ==").transform.Find("MapGlobalLight2D").
                GetComponent<Light2D>().intensity = 0.7f;

            GameObject.Find("== Environment Group ==").transform.Find("ObstacleGlobalLight2D").
                GetComponent<Light2D>().intensity = 1.0f;
        }
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

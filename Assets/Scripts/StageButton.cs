using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public LightFlicker LightFlicker { get; private set; }

    [SerializeField] private int stageNum = 0;
    public int StageNum { get; private set; }

    [SerializeField] private Sprite clearSprite;

    [SerializeField] private GameObject[] lights = new GameObject[2];
    private GameObject currentLight;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (DataManager.Instance.GameData.isClears[stageNum - 1] || stageNum == 1)
        {
            GetComponent<Image>().sprite = clearSprite;
            currentLight = lights[0];
            LightFlicker = currentLight.GetComponentInChildren<LightFlicker>();
            currentLight.SetActive(true);
            anim.SetBool("isUnlock", true);
        }
    }

    public void SetActiveLight(bool active)
    {
        if (!DataManager.Instance.GameData.isClears[stageNum - 1] && stageNum != 1)
        {
            lights[0].SetActive(false);
            lights[1].SetActive(true);
            return;
        }

        currentLight.SetActive(active);
        LightFlicker.enabled = active;

        if (active)
            LightFlicker.StartFadeFlow();
    }
}
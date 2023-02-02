using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpiritStatue : MonoBehaviour, IInteractable
{
    [SerializeField] private EStage stage;
    [SerializeField] private List<Image> keyAmountSlotList = new List<Image>();

    private int currentKeyAmount = 0;
    
    private void Start()
    {
        InitializedKeyAmountSlots();
    }

    private void InitializedKeyAmountSlots()
    {
        if (keyAmountSlotList.Count <= 0)
            keyAmountSlotList = GetComponentsInChildren<Image>().ToList();

        for (int i = 0; i < keyAmountSlotList.Count; i++)
        {
            currentKeyAmount = GameManager.Instance.KeyAmountList[(int)stage];
            if(currentKeyAmount - i >= 1)
                keyAmountSlotList[i].color = Color.green;
            else
                keyAmountSlotList[i].color = Color.white;
        }
    }

    public void UpdateSpiritStatue(int amount)
    {
        currentKeyAmount = amount;
        keyAmountSlotList[amount - 1].color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerControl player))
        {
            if(currentKeyAmount >= 3)
                GameManager.Instance.OpenStageDoor((int)stage);
        }
    }

    public void OnEnter()
    {
        
    }

    public void OnStay()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnInteract()
    {
        if(currentKeyAmount >= 3)
            GameManager.Instance.OpenStageDoor((int)stage);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private EDialogType dialogType;
    [SerializeField] private string dialogKey;

    private bool isInteractable = true;
    [SerializeField, Range(1f, 10f)] private float activeDelayTime = 3f;
    
    private void ActiveInteractable() => isInteractable = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!isInteractable || !col.CompareTag("Player"))
            return;

        isInteractable = false;
        DialogManager.Instance.ShowDialogTask(dialogType, dialogKey).Forget();
        Invoke(nameof(ActiveInteractable), activeDelayTime);
    }
}

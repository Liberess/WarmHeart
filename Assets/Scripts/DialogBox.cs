using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private EDialogType dialogType;
    [SerializeField] private string dialogKey;
    [SerializeField] private DialogEntity dialogEntity;

    private void Start()
    {
        dialogEntity = DialogManager.Instance.GetDialogEntity(dialogKey);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}

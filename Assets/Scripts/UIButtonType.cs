using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonType : MonoBehaviour
{
    [SerializeField] private EUIButtonType eUIButtonType;
    public EUIButtonType EUIButtonType => eUIButtonType;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnClickUIButton(this));
    }
}
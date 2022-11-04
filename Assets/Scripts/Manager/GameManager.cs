using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool isKey = false;
    public bool IsKey { get => isKey; }

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(this.gameObject);
    }

    public void GetKey() => isKey = true;
}
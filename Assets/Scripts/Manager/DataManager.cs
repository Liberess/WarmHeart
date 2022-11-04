using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        LoadGameData();
    }

    private void Start()
    {
        SaveGameData();
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void LoadGameData()
    {

    }

    private void SaveGameData()
    {

    }
}
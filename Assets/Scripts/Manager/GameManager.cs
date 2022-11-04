using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Space(5), Header("==== InGame ====")]
    [SerializeField] private bool isKey = false;
    public bool IsKey { get => isKey; }

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(this.gameObject);
    }

    public void PickupKey() => isKey = true;

    public void GoToScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
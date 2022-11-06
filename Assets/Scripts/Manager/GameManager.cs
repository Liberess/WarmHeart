using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Space(5), Header("==== InGame ====")]
    [SerializeField] private bool isKey = false;
    public bool IsKey { get => isKey; }

    public string SceneName { get; private set; }

    public bool IsGamePlay { get; private set; } = true;

    public UnityAction OnGameOverAction;

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else if(Instance != this)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;

        if(SceneManager.GetActiveScene().buildIndex > 5 && FadePanel.Instance != null)
            FadePanel.Instance.FadeOut();

        Time.timeScale = 1f;

        isKey = false;

        OnGameOverAction -= () => IsGamePlay = false;
        OnGameOverAction += () => IsGamePlay = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(AudioManager.Instance.SetActiveOptionPanel())
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void SetGamePlayPause(bool active) => IsGamePlay = !active;

    public void PickupKey() => isKey = true;

    public void OnClickUIButton(UIButtonType uIButtonType)
    {
        switch (uIButtonType.EUIButtonType)
        {
            case EUIButtonType.Restart:
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case EUIButtonType.Resume:
                Time.timeScale = 1f;
                AudioManager.Instance.SetActiveOptionPanel();
                break;
            case EUIButtonType.Lobby:
                GoToScene("Lobby");
                break;
        }
    }

    public void GoToStage(int stageNum)
    {
        GoToScene(string.Concat("Stage_", stageNum.ToString()));
    }

    public void GoToScene(string sceneName) => SceneManager.LoadScene(sceneName);

    private void OnSkip()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void OnPressBtn()
    {
        if(DataManager.Instance.GameData.isNewGame)
        {
            DataManager.Instance.GameData.isNewGame = false;
            SceneManager.LoadScene("NewGameCutScene");
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private GameObject mimimapPanel;

    [Space(5), Header("==== InGame ====")]
    [SerializeField] private List<int> keyAmountList = new List<int>() { 0, 0, 0};
    public List<int> KeyAmountList => keyAmountList;

    [SerializeField] private List<SpiritStatue> spiritStatueList = new List<SpiritStatue>();
    [SerializeField] private List<StageKeyDoor> stageKeyDoorList = new List<StageKeyDoor>();

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

        if (spiritStatueList.Count <= 0)
            spiritStatueList = FindObjectsOfType<SpiritStatue>().ToList();

        if (stageKeyDoorList.Count <= 0)
            stageKeyDoorList = FindObjectsOfType<StageKeyDoor>().ToList();

        OnGameOverAction -= () => IsGamePlay = false;
        OnGameOverAction += () => IsGamePlay = false;
    }

    public void SetGamePlayPause(bool active) => IsGamePlay = !active;

    public void PickupKey(EStage stage)
    {
        int index = (int)stage;
        ++keyAmountList[index];

        spiritStatueList[index].UpdateSpiritStatue(keyAmountList[index]);
    }

    public void OpenStageDoor(int index) => stageKeyDoorList[index].OpenDoor();
    
    public void FlipMenuPanel()
    {
        if (!menuPanel)
            return;
        
        if(menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PauseGameTime(bool isPause) => Time.timeScale = isPause ? 0f : 1f;

    public void OnClickUIButton(UIButtonType uiBtnType)
    {
        switch (uiBtnType.EUIButtonType)
        {
            case EUIButtonType.Restart:
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            
            case EUIButtonType.Resume:
                if (uiBtnType.transform.parent.name == "Grid")
                {
                    FlipMenuPanel();
                }
                else
                {
                    Time.timeScale = 1f;
                    uiBtnType.transform.parent.parent.gameObject.SetActive(false);
                }
                break;
            
            case EUIButtonType.Lobby:
                GoToScene("Lobby");
                break;
            
            case EUIButtonType.Quit:
                Application.Quit();
                break;
            
            case EUIButtonType.Close:
                FlipMenuPanel();
                uiBtnType.transform.parent.parent.gameObject.SetActive(false);
                break;
            
            case EUIButtonType.Audio:
                audioPanel.SetActive(true);
                menuPanel.SetActive(false);
                break;
            
            case EUIButtonType.Manual:
                manualPanel.SetActive(true);
                menuPanel.SetActive(false);
                break;
            
            case EUIButtonType.Minimap:
                PauseGameTime(true);
                mimimapPanel.SetActive(true);
                menuPanel.SetActive(false);
                break;
        }
    }

    public void GoToStage(int stageNum)
    {
        GoToScene(string.Concat("Stage_", stageNum.ToString()));
    }

    public void GoToScene(string sceneName) => SceneManager.LoadScene(sceneName);

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
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    #region �ν��Ͻ�ȭ

    private static DataManager m_Instance;

    public static DataManager Instance
    {
        get
        {
            if (!m_Instance)
            {
                m_Container = new GameObject();
                m_Container.name = "DataManager";
                m_Instance = m_Container.AddComponent(
                    typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(m_Container);
            }

            return m_Instance;
        }
    }

    private static GameObject m_Container;

    [Space(5), Header("==== Game Data Information ====")] [SerializeField]
    private GameData m_GameData;

    public GameData GameData
    {
        get
        {
            if (m_GameData == null)
                LoadGameData();

            return m_GameData;
        }
    }

    #endregion
    
    private readonly float SaveDataInterval = 15f;
    private DateTime lastSaveTime;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GPGSBinder.Instance.Login((success, localUser) =>
        {
            if (success)
            {
                LoadGameData();
                AutoSave().Forget();
            }
        });
    }
 
    private async UniTaskVoid AutoSave()
    {
        while(true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(SaveDataInterval), DelayType.UnscaledDeltaTime);
            SaveGameData();
        }
    }

    #region Game Data Load & Save

    public void InitializedGameData()
    {
        m_GameData.deathCount = 0;

        m_GameData.isNewGame = true;

        for (int i = 0; i < m_GameData.isClears.Length; i++)
            m_GameData.isClears[i] = false;

        m_GameData.sfx = 1.0f;
        m_GameData.bgm = 1.0f;

        m_GameData.playTime = 0.0f;
        m_GameData.saveTime = DateTime.Now;
    }

    public void OnClickNewGame()
    {
        InitializedGameData();
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickContinueGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void LoadGameData()
    {
        GPGSBinder.Instance.LoadCloud(nameof(m_GameData), (success, data) =>
        {
            if (success)
            {
                m_GameData = JsonUtility.FromJson<GameData>(data);
            }
            else
            {
                m_GameData = new GameData();
                InitializedGameData();
            }
        });
    }

    private void SaveGameData(bool immediately = false)
    {
        if (!Social.localUser.authenticated)
            return;
        
        TimeSpan timeCal = DateTime.Now - lastSaveTime;
        if(!immediately && timeCal.Seconds < SaveDataInterval)
            return;
        
        lastSaveTime = DateTime.Now;
        SaveTime();
        
        string toJsonData = JsonUtility.ToJson(m_GameData);
        GPGSBinder.Instance.SaveCloud(nameof(m_GameData), toJsonData);
    }

    private void SaveTime()
    {
        var currentSaveTime = m_GameData.saveTime;
        var nowSaveTime = DateTime.Now;
        if (currentSaveTime != nowSaveTime)
        {
            var playTime = currentSaveTime - nowSaveTime;
            m_GameData.playTime += playTime.TotalSeconds;
        }

        m_GameData.saveTime = nowSaveTime;
        m_GameData.saveTimeStr = m_GameData.saveTime.ToString();
    }

    #endregion

    private void OnApplicationQuit()
    {
        SaveGameData(true);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    private Transform playerPos;
    private RectTransform dialogTxtRect;

    [SerializeField] private GameObject dialogTxtGroup;
    [SerializeField] private GameObject dialogImgGroup;
    
    private bool isResizing = false;
    private bool isShowDialog = false;
    [SerializeField, Range(0.1f, 5f)] private float dialogVelocity = 0.5f;

    [SerializeField] private DialogSO dialogSo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(this);
        
        playerPos = FindObjectOfType<PlayerMove>().transform;
        dialogTxtRect = dialogTxtGroup.GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(DownloadDialogSO());
        
        dialogTxtGroup.SetActive(false);
        dialogImgGroup.SetActive(false);
    }

    #region Download Dialog Scriptable Obejct (Google Sheet)
    [ContextMenu("DownloadDialogSO")]
    public void DownloadDialog()
    {
        StartCoroutine(DownloadDialogSO());
    }
    
    private IEnumerator DownloadDialogSO()
    {
        UnityWebRequest defaultWWW = UnityWebRequest.Get(Tags.DialogURL);
        yield return defaultWWW.SendWebRequest();
        string[] row = defaultWWW.downloadHandler.text.Split('\n');
        int rowSize = row.Length;
        
        Tags.DialogURLDic.Clear();
        for (int i = 0; i < rowSize; i++)
        {
            Tags.DialogURLDic.Add(row[i].Split('\t')[0], row[i].Split('\t')[1]);
        }
        
        dialogSo.dialogEntityList.Clear();
        foreach (var URL in Tags.DialogURLDic)
        {
            UnityWebRequest www = UnityWebRequest.Get(string.Concat(Tags.DialogURL, "&gid=",URL.Value) );
            yield return www.SendWebRequest();
            SetDialogSO(URL.Key, www.downloadHandler.text);
        }
        
        yield return null;
    }

    private void SetDialogSO(string key, string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        List<string> dialogList = new List<string>();
        for (int i = 0; i < rowSize; i++)
            dialogList.Add(row[i].Split('\t')[0]);

        dialogSo.dialogEntityList.Add(new DialogEntity(key, dialogList.ToArray()));
    }

    public DialogEntity GetDialogEntity(string key)
    {
        return dialogSo.dialogEntityList.Find(e => e.key == key);
    }
    #endregion

    private async UniTaskVoid SetDialogTextTask(string msg)
    {
        isResizing = true;
        var txt = dialogTxtGroup.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = msg;

        var width = txt.preferredWidth + 4f;
        if (width > 100f)
            width = 100f;

        var height = txt.preferredHeight + 4f;

        Vector2 targetVec = new Vector2(width, height);

        while (true)
        {
            float gap = (targetVec - dialogTxtRect.sizeDelta).magnitude;
            if (gap <= 0.01f)
            {
                dialogTxtRect.sizeDelta = targetVec;
                break;
            }

            dialogTxtRect.sizeDelta = Vector2.Lerp(dialogTxtRect.sizeDelta, targetVec, Time.unscaledDeltaTime * 2f);
            await UniTask.Yield();
        }

        isResizing = false;
    }

    public async UniTaskVoid ShowDialogTask(EDialogType type, string key)
    {
        if (type == EDialogType.Text)
        {
            isShowDialog = true;
            UpdateDialogPositionTask().Forget();
            dialogTxtGroup.SetActive(true);
            
            string[] dialogs = GetDialogEntity(key).dialogs;
            foreach (string dialog in dialogs)
            {
                SetDialogTextTask(dialog).Forget();
                await UniTask.WaitUntil(() => isResizing == false);
                await UniTask.Delay(TimeSpan.FromSeconds(dialogVelocity), ignoreTimeScale:false);
            }
            
            isShowDialog = false;
            dialogTxtGroup.SetActive(false);
        }
        else
        {
            dialogImgGroup.SetActive(true);
            GameManager.Instance.PauseGameTime(true);

            await UniTask.Delay(TimeSpan.FromSeconds(5f), ignoreTimeScale:false);

            GameManager.Instance.PauseGameTime(false);
            dialogImgGroup.SetActive(false);
        }
    }

    private async UniTaskVoid UpdateDialogPositionTask()
    {
        dialogTxtRect.position = CalculateTargetPosition(playerPos.position);
        
        while (isShowDialog)
        {
            dialogTxtRect.position = Vector3.Lerp(dialogTxtRect.position, CalculateTargetPosition(playerPos.position), Time.unscaledDeltaTime);
            await UniTask.Yield();
        }
    }

    private Vector3 CalculateTargetPosition(Vector3 pos)
    {
        return Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y + 1f, 0f));
    }
}

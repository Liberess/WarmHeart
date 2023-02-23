using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] private DialogSO dialogSo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(this);
    }

    private void Start()
    {
        StartCoroutine(DownloadDialogSO());
    }

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
}

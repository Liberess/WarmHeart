using System.Collections.Generic;

[System.Serializable]
public struct DialogEntity
{
    public string key;
    public string[] dialogs;

    public DialogEntity(string key, string[] s)
    {
        this.key = key;
        dialogs = s;
    }
}

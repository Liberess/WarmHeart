using System.Collections.Generic;

public enum EStage { Stage1, Stage2, Stage3 }

public enum EUIButtonType { None, Restart, Resume, Lobby, Quit, Close, Audio, Manual, Minimap }

public enum ETouchablePanelType { Menu, Sub, Minimap };

public enum EDialogType { Text, PopUp }

[System.Serializable]
public static class Tags
{
    public static readonly string GameDataTag = "GameData";
    public static readonly string DialogURL = "https://docs.google.com/spreadsheets/d/1v-Ysdr8LobhZgsBbL605fs96kgU5zmPsyAmP339eJss/export?format=tsv&range=A2:B";
    public static readonly Dictionary<string, string> DialogURLDic = new Dictionary<string, string>();
}
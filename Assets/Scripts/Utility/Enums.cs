public enum EStage { Stage1, Stage2, Stage3 }

public enum EUIButtonType { None, Restart, Resume, Lobby, Quit, Close, Audio, Manual, Minimap }

public enum ETouchablePanelType { Menu, Sub, Minimap };

[System.Serializable]
public static class Tags
{
    public static readonly string GameDataTag = "GameData";
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Data",
    menuName = "Scriptable Object/Dialog Data", order = int.MaxValue)]
public class DialogSO : ScriptableObject
{
    public List<DialogEntity> dialogEntityList = new List<DialogEntity>();
}

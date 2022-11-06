using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCutScene : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ExitCut), 10f);
    }

    private void ExitCut()
    {
        GameManager.Instance.GoToScene("Lobby");
    }
}
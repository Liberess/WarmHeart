using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageKeyDoor : MonoBehaviour
{
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnEnter()
    {
        /*if (GameManager.Instance.IsKey)
        {
            StartCoroutine(OpenDoorCo());
        }*/
    }

    public void OpenDoor()
    {
        Debug.Log("open door");
        gameObject.SetActive(false);
        //StartCoroutine(OpenDoorCo());
    }

    private IEnumerator OpenDoorCo()
    {
        int sceneNum = 1;

        if (GameManager.Instance.SceneName == "Stage_TEST")
        {
            sceneNum = 1;
        }
        else
        {
            sceneNum = int.Parse(GameManager.Instance.SceneName.Substring(6));
            DataManager.Instance.GameData.isClears[sceneNum] = true;
        }

        if (openSprite)
            sprite.sprite = openSprite;

        GameManager.Instance.SetGamePlayPause(true);

        yield return new WaitForSeconds(2f);

        FadePanel.Instance.FadeIn();

        while (true)
        {
            if (FadePanel.Instance.IsCompleteFade)
                break;

            yield return new WaitForEndOfFrame();
        }

        switch (sceneNum)
        {
            case 1:
                GameManager.Instance.GoToScene("Stage1CutScene");
                break;
            case 2:
                GameManager.Instance.GoToScene("Stage2CutScene");
                break;
            case 3:
                GameManager.Instance.GoToScene("Stage3CutScene");
                break;
        }

        yield return null;
    }
}
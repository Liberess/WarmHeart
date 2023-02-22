using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchablePanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ETouchablePanelType eType;
    
    public Action OnTouchAction;

    private void Start()
    {
        switch (eType)
        {
            case ETouchablePanelType.Menu:
                OnTouchAction += () => GameManager.Instance.FlipMenuPanel();
                break;
            
            case ETouchablePanelType.Sub:
                OnTouchAction += () => GameManager.Instance.FlipMenuPanel();
                OnTouchAction += () => gameObject.SetActive(false);
                break;            
            
            case ETouchablePanelType.Minimap:
                OnTouchAction += () => GameManager.Instance.PauseGameTime(false);
                OnTouchAction += () => gameObject.SetActive(false);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsPointerOverUIObject())
            OnTouchAction?.Invoke();
    }

    /// <summary>
    /// UI 터치가 발생한 곳에 다른 UI가 있으면 true, 없으면 false
    /// </summary>
    protected bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject && results[0].gameObject != this.gameObject &&
                results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
            else
                return false;
        }

        return false;
    }
}
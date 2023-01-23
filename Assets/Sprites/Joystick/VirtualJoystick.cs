using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject joystickbase;
    public GameObject joystick;
    public GameObject Player;
    Vector2 origin_p;
    Vector2 last_p;
    [SerializeField, Range(0, 100)] float LeverdistanceX;
    [SerializeField, Range(0, 100)] float LeverdistanceY;
    bool Isfly;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Player.GetComponent<PlayerMove>().Movespeed = joystick.GetComponent<RectTransform>().anchoredPosition.x / 10;
        Player.GetComponent<PlayerMove>().JoyMove();
        if (Isfly)
        {
            joystickbase.GetComponent<RectTransform>().anchoredPosition = new Vector2(origin_p.x, origin_p.y+LeverdistanceY);
            Player.GetComponent<PlayerFly>().Isfly=true;
        }
        else
        {
            joystickbase.GetComponent<RectTransform>().anchoredPosition = origin_p;
            Player.GetComponent<PlayerFly>().Isfly = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        last_p = eventData.position - origin_p;
        if (Mathf.Abs(last_p.x) > LeverdistanceX)
            last_p.x = last_p.x >= LeverdistanceX ? LeverdistanceX : -LeverdistanceX;
        if (last_p.y >= LeverdistanceY)
        {
            last_p.y = LeverdistanceY;
            Isfly = true;
        }
        else
        {
            last_p.y = 0;
            Isfly = false;
        }
        joystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(last_p.x / 2, last_p.y/2);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        origin_p = eventData.position;
        joystickbase.GetComponent<RectTransform>().anchoredPosition = origin_p;
        joystickbase.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        Isfly = false;
        joystickbase.SetActive(false);
    }
}
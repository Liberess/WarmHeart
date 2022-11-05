using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public GameObject Player;
    Image _image;
    public Image _image1;
    Animator anim;
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        anim = _image1.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float c_fill = Player.GetComponent<PlayerFly>().FlyPower;
        if (c_fill != 100)
        {
            anim.SetBool("isActive", true);
            _image.fillAmount = c_fill / 100;
            _image.color = gradient.Evaluate(_image.fillAmount);
            //Debug.Log(_image.color);
        }
        else
        {
            anim.SetBool("isActive", false);
        }
        
    }
}

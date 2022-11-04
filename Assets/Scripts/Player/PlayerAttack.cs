using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Fireball;
    public PlayerFly PlayerFly { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        PlayerFly = GetComponent<PlayerFly>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (PlayerFly.FlyPower - 5 >= 0)
            {
                PlayerFly.FlyPower -= 5;
                Instantiate(Fireball, transform.position, transform.rotation);
            }
        }
    }
}

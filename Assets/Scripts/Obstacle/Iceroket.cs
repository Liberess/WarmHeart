using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceroket : MonoBehaviour
{
    public GameObject iceroket;
    float respontime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (respontime > 0)
            respontime -= Time.deltaTime;
        else
        {
            respontime = 2;
            Instantiate(iceroket,gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}

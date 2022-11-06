using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField, Range(0.0f, 5.0f)] private float shakeAmount = 1.0f;
    [SerializeField, Range(0.0f, 5.0f)] private float shakeTime = 1.0f;

    private bool isShake = false;

    private void Update()
    {
        if (isShake)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                transform.position = (Random.insideUnitSphere * shakeAmount) + transform.position;
            }
            else
            {
                isShake = false;
                shakeTime = 0f;
            }
        }
    }

    public void VibrateForTime(float time = 1.0f, float power = 1.0f)
    {
        isShake = true;
        shakeTime = time;
        shakeAmount = power;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D  myLight;

    private float time = 0f;
    [SerializeField, Range(0f, 10f)] private float fadeTime = 0.2f;
    [SerializeField, Range(0f, 50f)] private float lightIntensity = 0.7f;

    private void Start()
    {
        StartCoroutine(FadeFlow());
    }

    public void StartFadeFlow()
    {
        StopAllCoroutines();
        StartCoroutine(FadeFlow());
    }

    public IEnumerator FadeFlow()
    {
        time = 0f;

        myLight.intensity = 0;

        while (myLight.intensity < lightIntensity)
        {
            time += Time.deltaTime / fadeTime;
            myLight.intensity = Mathf.Lerp(0, lightIntensity, time);
            yield return null;
        }

        time = 0f;

        yield return new WaitForSeconds(1f);

        while (myLight.intensity > 0f)
        {
            time += Time.deltaTime / fadeTime;
            myLight.intensity = Mathf.Lerp(lightIntensity, 0, time);
            yield return null;
        }

        StartCoroutine(FadeFlow());
        //yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
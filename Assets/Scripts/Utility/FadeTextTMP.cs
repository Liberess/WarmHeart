using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeTextTMP : MonoBehaviour
{
    private TextMeshProUGUI txt;

    [SerializeField] private float fadeSpeed = 1.0f;

    private void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeFlow());
    }

    private IEnumerator FadeFlow()
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);

        WaitForSecondsRealtime delayTime = new WaitForSecondsRealtime(0.5f);

        while (true)
        {
            while (txt.color.a < 1.0f)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + (Time.unscaledDeltaTime / fadeSpeed));
                yield return null;
            }

            yield return delayTime;

            while (txt.color.a > 0.0f)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - (Time.unscaledDeltaTime / fadeSpeed));
                yield return null;
            }

            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0.0f);

        while (txt.color.a < 1.0f)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + (Time.unscaledDeltaTime / fadeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);

        while (txt.color.a > 0.0f)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - (Time.unscaledDeltaTime / fadeSpeed));
            yield return null;
        }
    }
}

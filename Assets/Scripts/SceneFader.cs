using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image Img;
    public AnimationCurve Curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = Curve.Evaluate(t);
            Img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while(t <= 1f)
        {
            t += Time.deltaTime;
            float a = Curve.Evaluate(t);
            Img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}

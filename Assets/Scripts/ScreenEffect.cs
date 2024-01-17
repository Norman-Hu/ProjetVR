using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
    public Material screenDamageMat;
    private Coroutine screenDamageTask;

    void ScreenDamageEffect(float intensity)
    {
        if (screenDamageTask != null)
            StopCoroutine(screenDamageTask);
        screenDamageTask = StartCoroutine(screenDamage(intensity));
    }

    private IEnumerator screenDamage(float intensity)
    {
        float targetRadius = Remap(intensity, 0, 1, 0.4f, -0.15f);
        float curRadius = 1;

        for (float t = 0f; curRadius != targetRadius; t += Time.deltaTime)
        {
            curRadius = Mathf.Lerp(1, targetRadius, t);
            screenDamageMat.SetFloat("_Vignette_radius", curRadius);
            yield return null;
        }

        for (float t = 0f; curRadius < 1; t += Time.deltaTime)
        {
            curRadius = Mathf.Lerp(targetRadius, 1, t);
            screenDamageMat.SetFloat("_Vignette_radius", curRadius);
            yield return null;
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return Mathf.Lerp(toMin, toMax, Mathf.InverseLerp(fromMin, fromMax, value));
    }
}

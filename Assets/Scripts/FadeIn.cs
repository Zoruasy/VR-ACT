using UnityEngine;
using System.Collections;

public class FadeCube : MonoBehaviour
{
    public float delay = 260f;
    public float fadeDuration = 2f;

    Material mat;
    float startAlpha;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        startAlpha = mat.color.a;  
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;
        Color c = mat.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, 0f, t / fadeDuration);
            mat.color = c;
            yield return null;
        }

        c.a = 0f;
        mat.color = c;
    }
}

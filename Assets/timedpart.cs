using UnityEngine;

public class FireFadeOut : MonoBehaviour
{
    public float fadeDuration = 5f;   // Tiempo del desvanecimiento
    public float startDelay = 20f;    // Tiempo antes de comenzar a desvanecer

    private ParticleSystem ps;
    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            mainModule = ps.main;
            Invoke(nameof(StartFade), startDelay);
        }
    }

    void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float time = 0f;
        Color startColor = mainModule.startColor.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);

            mainModule.startColor = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        // Asegurar que desaparezca
        mainModule.startColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        // Desactivar o destruir
        gameObject.SetActive(false);
        // Destroy(gameObject);  // si quieres eliminarlo completamente
    }
}

using UnityEngine;

/// <summary>
/// Genera un sonido de terremoto sintético sin necesidad de archivo de audio.
/// Se reproduce automáticamente cuando StartEarthquakePhase() lo solicita.
/// </summary>
public class EarthquakeSoundGenerator : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isGenerating = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    /// <summary>
    /// Inicia a generar sonido de terremoto
    /// </summary>
    public void StartEarthquakeSound()
    {
        if (isGenerating) return;
        isGenerating = true;
        
        // Generar sonido sintético (ruido blanco grave)
        int sampleRate = 44100;
        float duration = 30f; // 30 segundos
        int samples = (int)(sampleRate * duration);
        
        AudioClip clip = AudioClip.Create("EarthquakeSound", samples, 1, sampleRate, false);
        float[] data = new float[samples];
        
        // Generar ruido grave con bajo rumble
        for (int i = 0; i < samples; i++)
        {
            float time = (float)i / sampleRate;
            
            // Ruido grave (baja frecuencia)
            float lowFreq = Mathf.Sin(time * 40f * Mathf.PI * 2f); // 40 Hz
            float rumble = Mathf.Sin(time * 20f * Mathf.PI * 2f); // 20 Hz
            float noise = Random.Range(-0.3f, 0.3f);
            
            data[i] = (lowFreq * 0.4f + rumble * 0.3f + noise * 0.3f) * 0.5f;
        }
        
        clip.SetData(data, 0);
        
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.6f;
        audioSource.Play();
        
        Debug.Log("[EarthquakeSoundGenerator] 🔊 Sonido de terremoto generado y reproduciendo");
    }
    
    /// <summary>
    /// Detiene el sonido de terremoto
    /// </summary>
    public void StopEarthquakeSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            isGenerating = false;
            Debug.Log("[EarthquakeSoundGenerator] 🔇 Sonido de terremoto detenido");
        }
    }
}

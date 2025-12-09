using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simula un terremoto: temblor de cámara, caída de objetos, intensidad variable
/// </summary>
public class EarthquakeSimulator : MonoBehaviour
{
    [Header("Parámetros del Sismo")]
    [SerializeField] private float earthquakeDuration = 5f;
    [SerializeField] private float maxShakeIntensity = 0.15f;
    [SerializeField] private float shakeFalloff = 1.5f; // Qué tan rápido desaparece el temblor
    
    [Header("Objetos que caen")]
    [SerializeField] private List<GameObject> fallingObjects = new List<GameObject>();
    [SerializeField] private float debrisSpawnDelay = 0.2f;
    [SerializeField] private float debrisSpeed = 10f;

    private Camera mainCamera;
    private Vector3 originalCameraPos;
    private bool isEarthquakeActive = false;
    private float earthquakeTimeRemaining;

    public delegate void EarthquakeEventDelegate();
    public event EarthquakeEventDelegate OnEarthquakeStart;
    public event EarthquakeEventDelegate OnEarthquakeEnd;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            originalCameraPos = mainCamera.transform.localPosition;
        }
    }

    /// <summary>
    /// Iniciar el terremoto
    /// </summary>
    public void StartEarthquake()
    {
        if (isEarthquakeActive) return;

        isEarthquakeActive = true;
        earthquakeTimeRemaining = earthquakeDuration;
        OnEarthquakeStart?.Invoke();

        Debug.Log($"🌍 TERREMOTO INICIADO - Duración: {earthquakeDuration}s");

        // Iniciar caída de objetos
        StartCoroutine(SpawnFallingDebris());
    }

    private void Update()
    {
        if (!isEarthquakeActive) return;

        earthquakeTimeRemaining -= Time.deltaTime;

        if (earthquakeTimeRemaining <= 0)
        {
            StopEarthquake();
            return;
        }

        // Aplicar temblor a la cámara
        ApplyCameraShake();
    }

    /// <summary>
    /// Aplicar temblor visual a la cámara
    /// </summary>
    private void ApplyCameraShake()
    {
        if (mainCamera == null) return;

        // Calcular intensidad decreciente
        float progress = 1f - (earthquakeTimeRemaining / earthquakeDuration);
        float intensity = maxShakeIntensity * Mathf.Sin(progress * shakeFalloff * Mathf.PI);
        intensity = Mathf.Max(0, intensity);

        // Aplicar temblor aleatorio
        Vector3 shakeOffset = Random.insideUnitSphere * intensity;
        mainCamera.transform.localPosition = originalCameraPos + shakeOffset;
    }

    /// <summary>
    /// Generar objetos que caen durante el terremoto
    /// </summary>
    private IEnumerator SpawnFallingDebris()
    {
        float spawnTime = 0f;

        while (isEarthquakeActive && spawnTime < earthquakeDuration)
        {
            if (fallingObjects.Count > 0)
            {
                // Seleccionar objeto aleatorio
                GameObject prefab = fallingObjects[Random.Range(0, fallingObjects.Count)];
                Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 4f, Random.Range(-3f, 3f));
                
                GameObject debris = Instantiate(prefab, randomPos, Quaternion.identity);
                
                // Aplicar velocidad de caída
                Rigidbody rb = debris.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.down * debrisSpeed + Random.insideUnitSphere * 2f;
                    rb.mass = Random.Range(0.5f, 2f);
                }

                Debug.Log("💥 Escombro cayendo");
            }

            spawnTime += debrisSpawnDelay;
            yield return new WaitForSeconds(debrisSpawnDelay);
        }
    }

    /// <summary>
    /// Detener el terremoto
    /// </summary>
    public void StopEarthquake()
    {
        if (!isEarthquakeActive) return;

        isEarthquakeActive = false;

        // Restaurar cámara
        if (mainCamera != null)
        {
            mainCamera.transform.localPosition = originalCameraPos;
        }

        OnEarthquakeEnd?.Invoke();
        Debug.Log("🌍 Terremoto finalizado");
    }

    public bool IsEarthquakeActive() => isEarthquakeActive;
    public float GetTimeRemaining() => earthquakeTimeRemaining;
}

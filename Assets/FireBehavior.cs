using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    [SerializeField] public float currentIntensity = 100f;
    [SerializeField] private float maxIntensity = 100f;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;
    private bool isExtinguished = false;
    
    void Start()
    {
        if (fireParticles == null)
            fireParticles = GetComponentInChildren<ParticleSystem>();
        
        if (fireLight == null)
            fireLight = GetComponentInChildren<Light>();
        
        currentIntensity = maxIntensity;
        Debug.Log($"[FireBehavior] {gameObject.name} inicializado con intensidad {currentIntensity}");
    }
    
    void Update()
    {
        // Mantener intensidad >= 0
        if (currentIntensity < 0) currentIntensity = 0;
        if (currentIntensity > maxIntensity) currentIntensity = maxIntensity;
        
        // Actualizar visuals
        UpdateVisuals();
    }
    
    public void TakeDamage(float damage)
    {
        if (isExtinguished) return; // Si ya está apagado, ignorar daño
        
        currentIntensity -= damage;
        
        if (currentIntensity <= 0)
        {
            currentIntensity = 0;
            isExtinguished = true;
            OnFireExtinguished();
        }
    }
    
    // Método compatible con scripts antiguos
    public void ReduceIntensity(float amount)
    {
        TakeDamage(amount);
    }
    
    void UpdateVisuals()
    {
        // Escala según intensidad
        float scale = (currentIntensity / maxIntensity) * 0.5f;
        transform.localScale = new Vector3(scale, scale, scale);
        
        // Luz
        if (fireLight != null)
            fireLight.intensity = (currentIntensity / maxIntensity) * 2f;
        
        // Partículas
        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = (currentIntensity / maxIntensity) * 50f;
        }
        
        // Destruir si no hay intensidad
        if (currentIntensity <= 0 && !isExtinguished)
            Destroy(gameObject, 0.5f);
    }
    
    void OnFireExtinguished()
    {
        Debug.Log($"[FireBehavior] 🔥 {gameObject.name} ¡Fuego apagado! (Intensidad final: {currentIntensity})");
    }
    
    public bool IsExtinguished()
    {
        return isExtinguished;
    }
}
using UnityEngine;
using System.Collections;

public class EmergencyFireExtinguish : MonoBehaviour
{
    [Header("Configuración de Auto-Apagado")]
    public float autoExtinguishTime = 5f;
    public bool enableAutoExtinguish = true;
    
    private FireBehavior fireBehavior;
    private float timer = 0f;
    private bool isExtinguishing = false;

    void Start()
    {
        fireBehavior = GetComponent<FireBehavior>();
        
        if (enableAutoExtinguish)
        {
            Debug.Log("🔥 Auto-apagado activado. Se apagará en " + autoExtinguishTime + " segundos");
            StartCoroutine(AutoExtinguishCoroutine());
        }
    }

    IEnumerator AutoExtinguishCoroutine()
    {
        // Esperar el tiempo configurado
        yield return new WaitForSeconds(autoExtinguishTime);
        
        // Apagar el fuego
        ExtinguishFire();
    }

    void Update()
    {
        // Alternativa con Update
        if (enableAutoExtinguish && !isExtinguishing)
        {
            timer += Time.deltaTime;
            if (timer >= autoExtinguishTime)
            {
                ExtinguishFire();
            }
        }
    }

    void ExtinguishFire()
    {
        if (isExtinguishing) return;
        
        isExtinguishing = true;
        Debug.Log("🔥 AUTO-APAGADO: Extinguiendo fuego después de " + autoExtinguishTime + " segundos");
        
        if (fireBehavior != null)
        {
            // Apagar gradualmente
            StartCoroutine(GradualExtinguish());
        }
        else
        {
            // Destruir directamente si no hay FireBehavior
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator GradualExtinguish()
    {
        float duration = 2f; // Duración del apagado
        float rate = 1f / duration;
        
        while (fireBehavior.currentIntensity > 0)
        {
            fireBehavior.ReduceIntensity(rate * Time.deltaTime);
            yield return null;
        }
        
        Debug.Log("🔥 AUTO-APAGADO: Fuego completamente extinguido");
    }

    // Método para probar manualmente
    [ContextMenu("Apagar Fuego Ahora")]
    public void ExtinguishNow()
    {
        autoExtinguishTime = 0.1f;
        enableAutoExtinguish = true;
    }

    void OnDestroy()
    {
        Debug.Log("🔥 Fuego destruido por auto-apagado");
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ParticleCollisionHandler : MonoBehaviour
{
    [Header("Configuración de Extinción")]
    public string fireTag = "Fire";
    public float extinctionRate = 0.1f;
    public GameObject extinctionEffect;
    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        if (part == null)
        {
            Debug.LogError("⚠️ No se encontró Particle System en el mismo GameObject!");
            return;
        }
        
        collisionEvents = new List<ParticleCollisionEvent>();
        Debug.Log("✅ ParticleCollisionHandler inicializado para: " + gameObject.name);
    }

    void OnParticleCollision(GameObject other)
    {
        if (part == null) return;
        
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        
        Debug.Log($"Partículas colisionaron con: {other.name} ({numCollisionEvents} eventos)");
        
        // Verificar si colisionó con fuego
        if (other.CompareTag(fireTag))
        {
            Debug.Log("🔥 Colisión con FUEGO detectada!");
            ExtinguishFire(other.gameObject, numCollisionEvents);
        }
    }

    void ExtinguishFire(GameObject fire, int collisionIntensity)
    {
        FireBehavior fireBehavior = fire.GetComponent<FireBehavior>();
        if (fireBehavior != null)
        {
            float damage = extinctionRate * collisionIntensity;
            Debug.Log($"Infligiendo daño de extinción: {damage}");
            fireBehavior.ReduceIntensity(damage);
        }
        else
        {
            Debug.LogWarning("El objeto de fuego no tiene componente FireBehavior");
        }
    }

    void Update()
    {
        // Debug visual (sin usar Input.GetKeyDown que requiere Input System configurado)
        // Se puede omitir para producción
    }
}
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtintorController : MonoBehaviour
{
    [SerializeField] private ParticleSystem espumaParticles;
    [SerializeField] private GameObject boquilla;
    [SerializeField] private float damagePerSecond = 30f;
    [SerializeField] private float damageRange = 5f;
    
    [Header("Físicas")]
    [SerializeField] private float respawnDistance = 30f;  // Distancia máxima antes de respawnear
    [SerializeField] private Vector3 respawnPosition;       // Posición inicial para respawn
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rigidbody;
    private bool isHeld = false;
    private bool isFiring = false; // NUEVO: Track si está disparando activamente

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rigidbody = GetComponent<Rigidbody>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("❌ ExtintorController: No encuentro XRGrabInteractable");
            return;
        }
        
        // Configurar Rigidbody si no existe
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.mass = 2f;  // Peso realista
            rigidbody.linearDamping = 0.5f;
            rigidbody.angularDamping = 0.5f;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // No rotar
            Debug.Log("✅ Rigidbody creado automáticamente");
        }
        
        // Guardar posición inicial para respawn
        respawnPosition = transform.position;
        
        // Eventos de agarre
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
        
        // IMPORTANTE: Asegurar que isFiring comienza en FALSE
        isFiring = false;
        if (espumaParticles != null)
            espumaParticles.Stop();
        
        Debug.Log("🔧 Extintor listo - Modo dual-hitbox (Cuerpo + Boquilla) - Sin disparo inicial");
    }
    
    // NUEVO: Update continuo para aplicar daño mientras está disparando
    void Update()
    {
        if (isFiring && isHeld)
        {
            ApplyDamageToFires();
        }
        
        // Detectar si el extintor se cayó muy lejos
        if (!isHeld && Vector3.Distance(transform.position, respawnPosition) > respawnDistance)
        {
            Debug.LogWarning("⚠️ Extintor muy lejos, respawneando...");
            RespawnExtintor();
        }
    }

    // Cuando el usuario agarra el CUERPO
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("🖐️ CUERPO AGARRADO - Espera a que presionen la boquilla con la otra mano");
    }

    // Cuando el usuario suelta el CUERPO
    private void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;
        isFiring = false; // NUEVO: Detener disparo cuando sueltan el cuerpo
        if (espumaParticles != null)
        {
            espumaParticles.Stop();
        }
        Debug.Log("🖐️ CUERPO SOLTADO");
    }

    // Llamado desde BoquillaController cuando presiona
    public void DispararEspuma()
    {
        if (!isHeld)
        {
            Debug.Log("⚠️ Extintor no está agarrado, no puedo disparar");
            return;
        }
        
        if (espumaParticles != null)
        {
            espumaParticles.Play();
        }
        
        isFiring = true; // NUEVO: Marcar como disparando
        Debug.Log("💨 Disparando espuma (daño continuo iniciado)");
    }

    // Llamado desde BoquillaController cuando suelta
    public void DetenerEspuma()
    {
        isFiring = false; // NUEVO: Marcar como no disparando
        if (espumaParticles != null)
        {
            espumaParticles.Stop();
        }
        Debug.Log("🔓 Disparo detenido");
    }

    // Daña solo el fuego más cercano (llamado cada frame mientras está disparando)
    private void ApplyDamageToFires()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        
        FireBehavior closestFire = null;
        float closestDistance = float.MaxValue;
        
        // Encontrar el fuego más cercano
        foreach (Collider col in colliders)
        {
            FireBehavior fire = col.GetComponent<FireBehavior>();
            if (fire != null && !fire.IsExtinguished())
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFire = fire;
                }
            }
        }
        
        // Dañar solo el más cercano
        if (closestFire != null)
        {
            float damageThisFrame = damagePerSecond * Time.deltaTime;
            closestFire.TakeDamage(damageThisFrame);
            Debug.Log($"🔥 Daño al fuego más cercano ({closestFire.gameObject.name}): {damageThisFrame:F2} (Intensidad: {closestFire.currentIntensity:F1})");
        }
    }
    
    // Respawnea el extintor en su posición inicial
    private void RespawnExtintor()
    {
        transform.position = respawnPosition;
        
        if (rigidbody != null)
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        
        Debug.Log($"✅ Extintor respawneado en posición inicial");
    }

    // Para debug: visualizar el rango de daño
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}

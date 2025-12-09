using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleExtinguisher : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject nozzle; // La boquilla
    public ParticleSystem foamParticle;
    public Collider extinguisherCollider; // Collider del cuerpo del extintor
    
    [Header("Configuración")]
    public float particlesPerSecond = 50f;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable nozzleInteractable;
    private bool isHeld = false;
    private bool isNozzlePressed = false;

    void Start()
    {
        Debug.Log("Iniciando extintor...");
        
        // Obtener componente de agarre del EXTINTOR
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            // ✅ IMPORTANTE: Asignar el collider del CUERPO, no de la boquilla
            if (extinguisherCollider != null)
            {
                grabInteractable.colliders.Clear();
                grabInteractable.colliders.Add(extinguisherCollider);
            }
            
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        
        // Configurar boquilla
        if (nozzle != null)
        {
            nozzleInteractable = nozzle.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
            if (nozzleInteractable != null)
            {
                // ✅ Asegurar que la boquilla use su propio collider
                Collider nozzleCollider = nozzle.GetComponent<Collider>();
                if (nozzleCollider != null)
                {
                    nozzleInteractable.colliders.Clear();
                    nozzleInteractable.colliders.Add(nozzleCollider);
                }
                
                nozzleInteractable.selectEntered.AddListener(OnNozzlePressed);
                nozzleInteractable.selectExited.AddListener(OnNozzleReleased);
                nozzleInteractable.enabled = false;
            }
        }
        
        // Configurar partículas
        if (foamParticle != null)
        {
            foamParticle.Stop();
        }
    }

    void Update()
    {
        if (isHeld && isNozzlePressed)
        {
            if (foamParticle != null && !foamParticle.isPlaying)
            {
                foamParticle.Play();
            }
        }
        else
        {
            if (foamParticle != null && foamParticle.isPlaying)
            {
                foamParticle.Stop();
            }
        }
    }

    // Cuando se agarra el EXTINTOR
    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("Extintor agarrado");
        
        // Activar boquilla SOLO cuando el extintor está agarrado
        if (nozzleInteractable != null)
        {
            nozzleInteractable.enabled = true;
        }
    }

    // Cuando se suelta el EXTINTOR
    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        isNozzlePressed = false;
        Debug.Log("Extintor soltado");
        
        // Desactivar boquilla
        if (nozzleInteractable != null)
        {
            nozzleInteractable.enabled = false;
        }
        
        if (foamParticle != null)
        {
            foamParticle.Stop();
        }
    }

    // Cuando se PRESIONA la BOQUILLA
    private void OnNozzlePressed(SelectEnterEventArgs args)
    {
        if (!isHeld) return;
        
        isNozzlePressed = true;
        Debug.Log("Boquilla presionada");
    }

    // Cuando se SUELTA la BOQUILLA
    private void OnNozzleReleased(SelectExitEventArgs args)
    {
        isNozzlePressed = false;
        Debug.Log("Boquilla liberada");
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
        
        if (nozzleInteractable != null)
        {
            nozzleInteractable.selectEntered.RemoveListener(OnNozzlePressed);
            nozzleInteractable.selectExited.RemoveListener(OnNozzleReleased);
        }
    }
}
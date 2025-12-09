using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// Extintor mejorado con detección de fuegos para apagarse
/// Compatible con ambas manos + Input System
/// </summary>
public class WorkingExtinguisher : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private ParticleSystem foamParticle;
    [SerializeField] private InputActionReference gripInputAction; // Grip button (agarre)
    [SerializeField] private InputActionReference triggerInputAction; // Trigger (boquilla)
    
    [Header("Configuración")]
    [SerializeField] private float damagePerSecond = 30f; // Daño que hace el extintor por segundo
    [SerializeField] private float damageRange = 5f; // Rango de efecto
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isHeld = false;
    private bool isTriggerPressed = false;
    private List<FireBehavior> activeFiresInRange = new List<FireBehavior>();

    void Start()
    {
        // Obtener componente de agarre del extintor
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        
        // Configurar partículas
        if (foamParticle != null)
        {
            foamParticle.Stop();
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró ParticleSystem en WorkingExtinguisher");
        }
        
        // Registrar actions de input
        if (triggerInputAction != null)
        {
            triggerInputAction.action.started += OnTriggerPressed;
            triggerInputAction.action.canceled += OnTriggerReleased;
        }
        
        Debug.Log("🔧 Extintor listo con soporte dual-hand");
    }

    void Update()
    {
        // Controlar espuma si está agarrado Y trigger presionado
        if (isHeld && isTriggerPressed)
        {
            if (foamParticle != null && !foamParticle.isPlaying)
            {
                foamParticle.Play();
            }
            
            // Aplicar daño a fuegos cercanos
            ApplyDamageToFires();
        }
        else
        {
            // Detener espuma si no cumple condiciones
            if (foamParticle != null && foamParticle.isPlaying)
            {
                foamParticle.Stop();
            }
        }
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (isHeld)
        {
            isTriggerPressed = true;
            Debug.Log("💨 TRIGGER PRESIONADO");
        }
    }

    private void OnTriggerReleased(InputAction.CallbackContext context)
    {
        isTriggerPressed = false;
        Debug.Log("🔓 TRIGGER SOLTADO");
    }

    /// <summary>
    /// Detectar y dañar fuegos en rango
    /// </summary>
    private void ApplyDamageToFires()
    {
        // Limpiar lista de fuegos fuera de rango
        activeFiresInRange.RemoveAll(f => f == null || Vector3.Distance(transform.position, f.transform.position) > damageRange);

        // Buscar nuevos fuegos cercanos
        FireBehavior[] allFires = FindObjectsByType<FireBehavior>(FindObjectsSortMode.None);
        foreach (FireBehavior fire in allFires)
        {
            if (!activeFiresInRange.Contains(fire) && Vector3.Distance(transform.position, fire.transform.position) <= damageRange)
            {
                activeFiresInRange.Add(fire);
            }
        }

        // Aplicar daño a todos los fuegos activos
        foreach (FireBehavior fire in activeFiresInRange)
        {
            if (fire != null)
            {
                fire.ReduceIntensity(damagePerSecond * Time.deltaTime);
            }
        }
    }

    // Cuando se agarra el EXTINTOR (con cualquier mano)
    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("🖐️ Extintor AGARRADO (ambas manos soportadas)");
    }

    // Cuando se suelta el EXTINTOR
    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        isTriggerPressed = false;
        Debug.Log("🖐️ Extintor SOLTADO");
        
        // Detener espuma
        if (foamParticle != null)
        {
            foamParticle.Stop();
        }
        
        activeFiresInRange.Clear();
    }

    /// <summary>
    /// Obtener cantidad de fuegos en rango
    /// </summary>
    public int GetActiveFiresCount() => activeFiresInRange.Count;

    private void OnDestroy()
    {
        // Limpiar eventos
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
        
        if (triggerInputAction != null)
        {
            triggerInputAction.action.started -= OnTriggerPressed;
            triggerInputAction.action.canceled -= OnTriggerReleased;
        }
    }
}
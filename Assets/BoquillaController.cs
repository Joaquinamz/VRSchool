using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BoquillaController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private ExtintorController extintorPrincipal;
    private bool isPressedDown = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("❌ BoquillaController: No encuentro XRGrabInteractable");
            return;
        }
        
        // Buscar el extintor principal (padre)
        Transform padre = transform.parent;
        if (padre == null)
        {
            Debug.LogError("❌ BoquillaController: La boquilla debe ser hijo de ExtintorPrincipal");
            return;
        }

        // El ExtintorController está en el CuerpoExtintor (hermano)
        // Buscamos en todos los hijos del padre
        foreach (Transform child in padre)
        {
            ExtintorController ec = child.GetComponent<ExtintorController>();
            if (ec != null)
            {
                extintorPrincipal = ec;
                break;
            }
        }
        
        if (extintorPrincipal == null)
        {
            Debug.LogError("❌ BoquillaController: No encontré ExtintorController en los hermanos");
            return;
        }

        // Eventos de presión/interacción
        grabInteractable.selectEntered.AddListener(OnPressed);
        grabInteractable.selectExited.AddListener(OnReleased);
        
        Debug.Log("💨 Boquilla lista para presionar (dual-mano)");
    }

    // Cuando el usuario PRESIONA la boquilla
    private void OnPressed(SelectEnterEventArgs args)
    {
        if (isPressedDown) return; // Evita duplicados
        
        isPressedDown = true;
        Debug.Log("💨 BOQUILLA PRESIONADA - Disparando espuma");
        
        // Llamar al extintor para disparar
        if (extintorPrincipal != null)
        {
            extintorPrincipal.DispararEspuma();
        }
    }

    // Cuando el usuario SUELTA la boquilla
    private void OnReleased(SelectExitEventArgs args)
    {
        isPressedDown = false;
        Debug.Log("🔓 BOQUILLA SOLTADA - Deteniendo espuma");
        
        // Detener espuma
        if (extintorPrincipal != null)
        {
            extintorPrincipal.DetenerEspuma();
        }
    }
}

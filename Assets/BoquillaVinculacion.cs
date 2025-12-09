using UnityEngine;

/// <summary>
/// Script para mantener la boquilla vinculada al cuerpo del extintor
/// Usa LateUpdate para garantizar sincronización después de física
/// </summary>
public class BoquillaVinculacion : MonoBehaviour
{
    private Transform cuerpoTransform;
    private Vector3 offsetPosicion;
    private Quaternion offsetRotacion;
    private bool vinculado = false;

    void Start()
    {
        // Buscar el cuerpo (hermano en la jerarquía)
        Transform padre = transform.parent;
        
        if (padre == null)
        {
            Debug.LogError("❌ BoquillaVinculacion: La boquilla debe estar dentro de ExtintorPrincipal");
            return;
        }

        // Buscar CuerpoExtintor entre los hermanos
        foreach (Transform hermano in padre)
        {
            if (hermano.name.Contains("Cuerpo") || hermano.name.Contains("Body"))
            {
                cuerpoTransform = hermano;
                break;
            }
        }

        // Si no encuentra por nombre, tomar el primer hijo
        if (cuerpoTransform == null && padre.childCount > 0)
        {
            cuerpoTransform = padre.GetChild(0);
        }

        if (cuerpoTransform == null)
        {
            Debug.LogError("❌ BoquillaVinculacion: No encontré CuerpoExtintor");
            return;
        }

        // Guardar la posición y rotación relativa inicial
        offsetPosicion = transform.localPosition;
        offsetRotacion = transform.localRotation;
        vinculado = true;

        Debug.Log($"✅ Boquilla vinculada a: {cuerpoTransform.name}");
    }

    void LateUpdate()
    {
        if (!vinculado || cuerpoTransform == null)
            return;

        // Mantener la posición relativa respecto al cuerpo
        transform.position = cuerpoTransform.position + cuerpoTransform.TransformDirection(offsetPosicion);
        
        // Mantener la rotación relativa respecto al cuerpo
        transform.rotation = cuerpoTransform.rotation * offsetRotacion;
    }

    // Método para debugging
    public void MostrarInfo()
    {
        Debug.Log($"Boquilla vinculada: {vinculado}");
        Debug.Log($"Cuerpo: {cuerpoTransform.name}");
        Debug.Log($"Offset Posición: {offsetPosicion}");
        Debug.Log($"Offset Rotación: {offsetRotacion.eulerAngles}");
    }
}

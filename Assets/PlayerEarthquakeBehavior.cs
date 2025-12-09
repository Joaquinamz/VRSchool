using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controla el comportamiento del jugador durante un sismo
/// - Agacharse bajo mesas
/// - Detectar colisiones con escombros
/// - Mantener posición segura
/// </summary>
public class PlayerEarthquakeBehavior : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float crouchHeight = 0.5f;
    // [SerializeField] private float normalHeight = 1.8f;  // Para futuros usos
    // [SerializeField] private float crouchSpeed = 2f;     // Para futuros usos
    [SerializeField] private InputActionReference crouchInput; // Usar joystick o tecla
    
    [Header("Detección de Seguridad")]
    [SerializeField] private float safeZoneCheckRadius = 1f;
    [SerializeField] private LayerMask tableLayerMask;

    private CharacterController characterController;
    private bool isCrouching = false;
    private bool isUnderTable = false;
    private int debrisHits = 0;
    private int successfulCrouches = 0;
    private float crouchDuration = 0f;

    private Vector3 originalCameraPos;
    private Camera mainCamera;

    public delegate void PlayerEventDelegate();
    public event PlayerEventDelegate OnCrouch;
    public event PlayerEventDelegate OnStand;
    public event PlayerEventDelegate OnDebrisHit;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = GetComponentInChildren<Camera>();
        
        if (mainCamera != null)
        {
            originalCameraPos = mainCamera.transform.localPosition;
        }

        // Configurar input
        if (crouchInput != null)
        {
            crouchInput.action.started += OnCrouchInput;
            crouchInput.action.canceled += OnStandInput;
        }
    }

    private void Update()
    {
        if (isCrouching)
        {
            crouchDuration += Time.deltaTime;
            CheckIfUnderTable();
        }
    }

    /// <summary>
    /// Input para agacharse (joystick derecho o tecla)
    /// </summary>
    private void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (isCrouching) return;

        isCrouching = true;
        OnCrouch?.Invoke();
        successfulCrouches++;

        // Ajustar altura de cámara
        if (mainCamera != null)
        {
            Vector3 newPos = originalCameraPos;
            newPos.y = crouchHeight;
            mainCamera.transform.localPosition = newPos;
        }

        Debug.Log("🛑 AGACHADO - Posición segura");
    }

    private void OnStandInput(InputAction.CallbackContext context)
    {
        if (!isCrouching) return;

        isCrouching = false;
        OnStand?.Invoke();

        // Restaurar altura
        if (mainCamera != null)
        {
            mainCamera.transform.localPosition = originalCameraPos;
        }

        crouchDuration = 0f;
        Debug.Log("🚶 LEVANTADO");
    }

    /// <summary>
    /// Verificar si el jugador está bajo una mesa (zona segura)
    /// </summary>
    private void CheckIfUnderTable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, safeZoneCheckRadius, tableLayerMask);
        isUnderTable = colliders.Length > 0;

        if (isUnderTable)
        {
            Debug.Log("✅ Bajo tabla segura");
        }
    }

    /// <summary>
    /// Detectar colisión con escombros (daño)
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Debris"))
        {
            if (isCrouching && isUnderTable)
            {
                // Protegido bajo la mesa
                Debug.Log("🛡️ Escombro bloqueado - Estás seguro bajo la mesa");
            }
            else
            {
                // Golpeado por escombro
                debrisHits++;
                OnDebrisHit?.Invoke();
                Debug.Log($"💥 ¡GOLPE POR ESCOMBRO! Total: {debrisHits}");
            }
        }
    }

    // Getters
    public bool IsCrouching() => isCrouching;
    public bool IsUnderTable() => isUnderTable;
    public int GetDebrisHits() => debrisHits;
    public int GetSuccessfulCrouches() => successfulCrouches;
    public float GetCrouchDuration() => crouchDuration;

    private void OnDestroy()
    {
        if (crouchInput != null)
        {
            crouchInput.action.started -= OnCrouchInput;
            crouchInput.action.canceled -= OnStandInput;
        }
    }
}

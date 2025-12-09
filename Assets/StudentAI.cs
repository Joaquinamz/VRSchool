using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controla a estudiantes NPC que evacúan ordenadamente
/// - Siguen ruta de evacuación
/// - Evitan colisión con jugador
/// - Mantienen distancia
/// </summary>
public class StudentAI : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform exitPoint; // Punto de salida
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float separationDistance = 1.5f;
    
    private bool isEvacuating = false;
    private int evacuationOrder = 0; // Orden en que sale (0 = primero)

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        agent.stoppingDistance = stoppingDistance;
        agent.enabled = false; // Desactivar hasta que inicie evacuación
    }

    private void Update()
    {
        if (!isEvacuating || !agent.enabled) return;

        // Si llegó al punto de salida, desactivar
        if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                FinishEvacuation();
            }
        }
    }

    /// <summary>
    /// Iniciar evacuación ordenada
    /// </summary>
    public void StartEvacuation(Transform exit, int order)
    {
        exitPoint = exit;
        evacuationOrder = order;
        isEvacuating = true;

        if (agent != null)
        {
            agent.enabled = true;
            agent.SetDestination(exitPoint.position);
            
            // Agregar pequeño delay basado en el orden
            agent.velocity = Vector3.zero;
            
            Debug.Log($"🏃 Estudiante {evacuationOrder} evacuando");
        }
    }

    /// <summary>
    /// Mantener distancia con otros estudiantes
    /// </summary>
    private void MaintainDistance()
    {
        StudentAI[] otherStudents = FindObjectsByType<StudentAI>(FindObjectsSortMode.None);
        
        foreach (StudentAI other in otherStudents)
        {
            if (other == this) continue;

            float distance = Vector3.Distance(transform.position, other.transform.position);
            
            if (distance < separationDistance && distance > 0)
            {
                // Empujar levemente para mantener distancia
                Vector3 separation = (transform.position - other.transform.position).normalized;
                Vector3 newVelocity = agent.velocity + separation * 0.5f;
                agent.velocity = newVelocity;
            }
        }
    }

    /// <summary>
    /// Evitar colisión con jugador
    /// </summary>
    private void AvoidPlayer()
    {
        Collider playerCollider = FindFirstObjectByType<CharacterController>()?.GetComponent<Collider>();
        if (playerCollider == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerCollider.transform.position);
        
        if (distanceToPlayer < 2f)
        {
            // No chocar - ir alrededor
            Vector3 avoidDirection = (transform.position - playerCollider.transform.position).normalized;
            agent.velocity += avoidDirection * 0.3f;
            Debug.Log("➡️ Evitando colisión con jugador");
        }
    }

    /// <summary>
    /// Completar evacuación
    /// </summary>
    private void FinishEvacuation()
    {
        isEvacuating = false;
        agent.enabled = false;
        Debug.Log($"✅ Estudiante {evacuationOrder} evacuado exitosamente");
    }

    // Getters
    public bool IsEvacuating() => isEvacuating;
    public int GetEvacuationOrder() => evacuationOrder;
}

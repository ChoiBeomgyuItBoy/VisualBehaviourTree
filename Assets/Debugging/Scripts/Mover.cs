using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6;
    NavMeshAgent agent;

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        agent.destination = destination;
        agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}

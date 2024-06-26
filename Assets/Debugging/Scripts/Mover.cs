using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] float maxSpeed = 6;
    [SerializeField] float destinationTolerance = 1;
    NavMeshAgent agent;
    Animator animator;

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        agent.destination = destination;
        agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
    }

    public bool AtDestination(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination) < destinationTolerance;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("movementSpeed", GetLocalVelocity(), 0.1f, Time.deltaTime);
    }

    float GetLocalVelocity()
    {
        return transform.InverseTransformDirection(agent.velocity).z;
    }
}

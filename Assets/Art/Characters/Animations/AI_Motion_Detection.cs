using System.Collections;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DirectionalVelocityAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public NavMeshAgent NMA;
    private Vector3 lastPosition; // To calculate velocity for objects without Rigidbody

    void Start()
    {
        // Get the Animator component if not assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Initialize the last position
        lastPosition = transform.position;
    }

    void Update()
    {
        float velocityX, velocityY;
        velocityX = NMA.velocity.x;
        velocityY = NMA.velocity.x;


       


        // Update the Animator parameters
        if (animator != null)
        {
            animator.SetFloat("Velocity X", velocityX);
            animator.SetFloat("Velocity Y", velocityY);
        }
    }
}

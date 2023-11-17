using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float speed = 5f;  // Vitesse de l'ast�ro�de

    private Vector3 direction;  // Direction de l'ast�ro�de

    private void Update()
    {
        // D�placer l'ast�ro�de dans sa direction � la vitesse donn�e
        transform.Translate(direction * speed * Time.deltaTime);

        // V�rifier si l'ast�ro�de s'est trop �loign� du vaisseau et le d�truire si c'est le cas
        if (Vector3.Distance(transform.position, Vector3.zero) > 50f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        // D�finir la direction de l'ast�ro�de
        direction = newDirection;
    }
}
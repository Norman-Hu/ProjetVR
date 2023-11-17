using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float speed = 5f;  // Vitesse de l'astéroïde

    private Vector3 direction;  // Direction de l'astéroïde

    private void Update()
    {
        // Déplacer l'astéroïde dans sa direction à la vitesse donnée
        transform.Translate(direction * speed * Time.deltaTime);

        // Vérifier si l'astéroïde s'est trop éloigné du vaisseau et le détruire si c'est le cas
        if (Vector3.Distance(transform.position, Vector3.zero) > 50f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        // Définir la direction de l'astéroïde
        direction = newDirection;
    }
}
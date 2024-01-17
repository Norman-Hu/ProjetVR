using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashing : MonoBehaviour
{
    public GameObject collisionExplosion;
    public AudioSource explosion_sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid") || other.CompareTag("Enemy"))
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            explosion_sound.Play();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(explosion, 1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public AudioSource explosion_sound;
    public GameObject collisionExplosion;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid"))
        {
            DamageShip();
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(explosion, 1f);
        }
    }

    private void DamageShip()
    {
        explosion_sound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour
{
    public bool is_target_set = false;
    public Vector3 target;
    private GameObject targettedObject;
    public AudioSource explosion_sound;
    public GameObject collisionExplosion;
    public float speed = 0.5f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelayCoroutine());
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (is_target_set)
        {
            if (transform.position == target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        else
        {
            transform.position += transform.forward * step * 0.05f;
        }
    }

    public void setTarget(Vector3 _target, GameObject _targettedObject)
    {
        target = _target;
        targettedObject = _targettedObject;
        is_target_set = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Enemy"))
        {
            explode();
            explosion_sound.Play();
            Destroy(gameObject);
            Destroy(other.gameObject);
            return;
        }
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(targettedObject);
            Destroy(explosion, 1f);
        }
    }

    IEnumerator DestroyAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

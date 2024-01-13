using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour
{
    public Vector3 target;
    public GameObject collisionExplosion;
    public float speed;

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (target != null)
        {
            if (transform.position == target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void setTarget(Vector3 _target)
    {
        target = _target;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}

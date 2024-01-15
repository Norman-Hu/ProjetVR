using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootRate;
    private float shootRateTimeStamp;

    public GameObject laserShotPrefab;

    RaycastHit rc_hit;
    float range = 1000.0f;

    public void Fire()
    {
        if (Time.time > shootRateTimeStamp)
        {
            shootRay();
            shootRateTimeStamp = Time.time + shootRate;
        }
    }

    void shootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        GameObject laser = Instantiate(laserShotPrefab, transform.position + new Vector3(0.0173119f, 0f, 0.153145f), transform.rotation);
        if (Physics.Raycast(ray, out rc_hit, range))
        {
            if (rc_hit.collider.CompareTag("Asteroid"))
            {
                laser.GetComponent<ShotBehavior>().setTarget(rc_hit.point, rc_hit.collider.gameObject);
            }
        }
    }
}

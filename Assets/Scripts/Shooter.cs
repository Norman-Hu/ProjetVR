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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shootRateTimeStamp)
            {
                shootRay();
                shootRateTimeStamp = Time.time + shootRate;
            }
        }
    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray, out rc_hit, range))
        {
            GameObject laser = GameObject.Instantiate(laserShotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(rc_hit.point);
            GameObject.Destroy(laser, 2f);
        }
    }
}

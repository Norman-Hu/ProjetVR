using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public Transform firePoint;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    LineRenderer laserLine;
    float fireTimer;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            Debug.Log("Oui");
            fireTimer = 0f;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = firePoint.position;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, firePoint.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                //Destroy(hit.transform.gameObject);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}

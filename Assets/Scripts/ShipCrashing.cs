using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrashing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}

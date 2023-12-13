using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroidPrefabs;

    [SerializeField]
    private int asteroidCount = 15;
    
    void Start()
    {
        Vector3 min = new Vector3(-50, -50, -50);
        Vector3 max = new Vector3(50, 50, 50);
        for (int i = 0; i < asteroidCount; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            int id = (int)Math.Floor(Random.Range(0, asteroidPrefabs.Length - float.Epsilon));
            Instantiate(asteroidPrefabs[id], transform).transform.position = pos;
        }
    }

    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemiesPrefabs;

    [SerializeField]
    private int enemiesCount = 15;

    void Start()
    {
        Vector3 min = new Vector3(-50, -50, -50);
        Vector3 max = new Vector3(50, 50, 50);
        for (int i = 0; i < enemiesCount; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
            int id = (int)Math.Floor(Random.Range(0, enemiesPrefabs.Length - float.Epsilon));
            Instantiate(enemiesPrefabs[id], transform).transform.position = pos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject player;
    public GameObject asteroid;

    public float maxRange = 10f;
    public float minRange = 5f;
    public float maximumScale = 10f;
    public float minimumScale = 5f;
    public float spawnInterval = 3f;

    public Vector3 screenCenter;

    bool isPlayerAlive;
    float time = 0.0f;
    float minimumY;
    float maximumY;
    float minimumX;
    float maximumX;

    void Start()
    {

        // Instantiating player
        player = Instantiate(player, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        this.minimumY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).y;
        this.maximumY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -Camera.main.transform.position.z)).y;
        this.minimumX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).x;
        this.maximumX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -Camera.main.transform.position.z)).x;


    }

    public Vector3 GetNewPosition(Vector3 position)
    {

        return new Vector3(screenCenter.x - position.x, screenCenter.y - position.y, 0);

    }

    bool FindPlayer()
    {

        Collider[] colliders = player.GetComponents<Collider>();

        Collider collider;

        if (colliders[0].isTrigger)
        {
            collider = colliders[0];
        }
        else
        {
            collider = colliders[1];
        }


        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            return true;
        else
            return false;

    }

    void InstantiateRandomAsteroid()
    {


        bool targetPending = true;

        float spawnX = 0;
        float spawnY = 0;

        while (targetPending)
        {

            if (UnityEngine.Random.value > 0.5f)
            {

                Range[] rangesX = new Range[] { new Range(minimumX - maxRange, minimumX - minRange), new Range(maximumX + minRange, maximumX + maxRange) };
                spawnX = RandomValueFromRanges(rangesX);
                spawnY = UnityEngine.Random.Range(minimumY - maxRange, maximumY + maxRange);
            }
            else
            {

                Range[] rangesY = new Range[] { new Range(minimumY - maxRange, minimumY - minRange), new Range(maximumY + minRange, maximumY + maxRange) };
                spawnX = UnityEngine.Random.Range(minimumX - maxRange, maximumX + maxRange);
                spawnY = RandomValueFromRanges(rangesY);
            }

            // Avoiding spawning 2 asteroids ont op of each other
            Collider[] colliders = Physics.OverlapBox(new Vector3(spawnX, spawnY, 0), new Vector3(1, 1, 1));

            targetPending = colliders.Length > 0;

        }

        GameObject asteroidObject = Instantiate(asteroid, new Vector3(spawnX, spawnY, 0), Quaternion.Euler(0, 0, 0));

        asteroidObject.transform.LookAt(screenCenter);
        float scale = UnityEngine.Random.Range(minimumScale, maximumScale);

        asteroidObject.transform.localScale = new Vector3(scale, scale, scale);

    }

    // Update is called once per frame
    void Update()
    {



        if (!FindPlayer())
        {

            player.transform.position = GetNewPosition(player.transform.position);

        }


        time += Time.deltaTime;

        if (time >= spawnInterval)
        {
            time = time - spawnInterval;

            InstantiateRandomAsteroid();
        }

    }

    public static float RandomValueFromRanges(Range[] ranges)
    {
        if (ranges.Length == 0)
            return 0;
        float count = 0;
        foreach (Range r in ranges)
            count += r.range;
        float sel = UnityEngine.Random.Range(0, count);
        foreach (Range r in ranges)
        {
            if (sel < r.range)
            {
                return r.min + sel;
            }
            sel -= r.range;
        }
        throw new Exception("This should never happen");
    }
}
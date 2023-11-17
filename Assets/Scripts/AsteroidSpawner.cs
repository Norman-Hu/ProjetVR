using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;  // R�f�rence au prefab de l'ast�ro�de
    public int maxAsteroids = 10;       // Nombre maximum d'ast�ro�des
    public float spawnInterval = 2f;   // Intervalle entre chaque spawn en secondes

    private void Start()
    {
        // Lancer la coroutine de spawn � l'initialisation
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            // Spawn un ast�ro�de si le nombre actuel est inf�rieur au maximum
            if (GameObject.FindGameObjectsWithTag("Asteroid").Length < maxAsteroids)
            {
                SpawnAsteroid();
            }

            // Attendre avant le prochain spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnAsteroid()
    {
        // Instancier un nouvel ast�ro�de
        GameObject asteroid = Instantiate(asteroidPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // D�finir la direction de l'ast�ro�de (vers le vaisseau)
        Vector3 direction = transform.position - asteroid.transform.position;
        asteroid.GetComponent<AsteroidBehavior>().SetDirection(direction.normalized);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // G�n�rer une position al�atoire devant le vaisseau (dans l'espace vision de la cam�ra)
        // Vous pouvez personnaliser cela en fonction de votre sc�ne
        float spawnDistance = 10f;  // Distance de spawn devant le vaisseau
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0;  // Pour rester dans le plan horizontal
        return transform.position + randomDirection.normalized * spawnDistance;
    }
}
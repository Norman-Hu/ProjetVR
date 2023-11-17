using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;  // Référence au prefab de l'astéroïde
    public int maxAsteroids = 10;       // Nombre maximum d'astéroïdes
    public float spawnInterval = 2f;   // Intervalle entre chaque spawn en secondes

    private void Start()
    {
        // Lancer la coroutine de spawn à l'initialisation
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            // Spawn un astéroïde si le nombre actuel est inférieur au maximum
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
        // Instancier un nouvel astéroïde
        GameObject asteroid = Instantiate(asteroidPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // Définir la direction de l'astéroïde (vers le vaisseau)
        Vector3 direction = transform.position - asteroid.transform.position;
        asteroid.GetComponent<AsteroidBehavior>().SetDirection(direction.normalized);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Générer une position aléatoire devant le vaisseau (dans l'espace vision de la caméra)
        // Vous pouvez personnaliser cela en fonction de votre scène
        float spawnDistance = 10f;  // Distance de spawn devant le vaisseau
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0;  // Pour rester dans le plan horizontal
        return transform.position + randomDirection.normalized * spawnDistance;
    }
}
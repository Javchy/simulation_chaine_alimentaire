using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    public GameObject plantPrefab; // Préfabriqué de la plante
    public int minPlants = 15;    // Nombre minimum de plantes
    public int maxPlants = 20;    // Nombre maximum de plantes
    private float spawnInterval = 1f; // Intervalle initial plus court
    private float _spawnCooldown = 0f;
    public float spawnIntervalReductionDuration = 60f;
    public float minSpawnInterval = .2f; // Intervalle minimum
    public float maxSpawnInterval = 1f; // Intervalle maximum

    void Start()
    {
        startingplants();
    }

    private void Update()
    {
        if (_spawnCooldown <= 0f)
        {
            SpawnPlants(); // Spawner les plantes à chaque cooldown
            float timeRatio = Time.timeSinceLevelLoad > 0 ? Mathf.Clamp01(spawnIntervalReductionDuration / Time.timeSinceLevelLoad) : 0;
            _spawnCooldown = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, timeRatio); // Réduction plus marquée
        }
        else
        {
            _spawnCooldown -= Time.deltaTime; // Réduction du cooldown
        }
    }

    // Méthode pour spawner plusieurs plantes à la fois
    private void SpawnPlants()
    {
        int plantCounter = Random.Range(5, 10); // Spawner entre 5 et 10 plantes par fois
        for (int i = 0; i < plantCounter; i++)
        {
            Vector3 spawnPosition = SpawnPosition();
            Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Méthode pour générer un nombre aléatoire de plantes au début
    void startingplants()
    {
        int numberOfPlants = Random.Range(minPlants, maxPlants + 1);

        for (int i = 0; i < numberOfPlants; i++)
        {
            Vector3 spawnPosition = SpawnPosition();
            Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Méthode pour générer une position aléatoire pour le spawn de la plante
    Vector3 SpawnPosition()
    {
        float x = Random.Range(0f, 299f); // Position X aléatoire
        float z = Random.Range(0f, 299f); // Position Z aléatoire

        return new Vector3(x, 2f, z); // Retourne une position au sol (Y = 2)
    }
}

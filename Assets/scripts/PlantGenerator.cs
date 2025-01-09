using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    public GameObject plantPrefab; // Pr�fabriqu� de la plante
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
            SpawnPlants(); // Spawner les plantes � chaque cooldown
            float timeRatio = Time.timeSinceLevelLoad > 0 ? Mathf.Clamp01(spawnIntervalReductionDuration / Time.timeSinceLevelLoad) : 0;
            _spawnCooldown = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, timeRatio); // R�duction plus marqu�e
        }
        else
        {
            _spawnCooldown -= Time.deltaTime; // R�duction du cooldown
        }
    }

    // M�thode pour spawner plusieurs plantes � la fois
    private void SpawnPlants()
    {
        int plantCounter = Random.Range(5, 10); // Spawner entre 5 et 10 plantes par fois
        for (int i = 0; i < plantCounter; i++)
        {
            Vector3 spawnPosition = SpawnPosition();
            Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // M�thode pour g�n�rer un nombre al�atoire de plantes au d�but
    void startingplants()
    {
        int numberOfPlants = Random.Range(minPlants, maxPlants + 1);

        for (int i = 0; i < numberOfPlants; i++)
        {
            Vector3 spawnPosition = SpawnPosition();
            Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // M�thode pour g�n�rer une position al�atoire pour le spawn de la plante
    Vector3 SpawnPosition()
    {
        float x = Random.Range(0f, 299f); // Position X al�atoire
        float z = Random.Range(0f, 299f); // Position Z al�atoire

        return new Vector3(x, 2f, z); // Retourne une position au sol (Y = 2)
    }
}

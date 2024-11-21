using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    public GameObject plantPrefab; // Préfabriqué de la plante
    public int minPlants = 10;    // Nombre minimum de plantes
    public int maxPlants = 20;    // Nombre maximum de plantes
    public float spawnRadius = 10f; // Rayon autour de l'origine pour positionner les plantes

    void Start()
    {
        // Génère un nombre aléatoire de plantes
        int plantCount = Random.Range(minPlants, maxPlants + 1);
        for (int i = 0; i < plantCount; i++)
        {
            SpawnPlant();
        }
    }

    void SpawnPlant()
    {
        // Position aléatoire dans un rayon autour de l'origine
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 0; // Place les plantes au niveau du sol

        // Instancier le préfabriqué
        Instantiate(plantPrefab, randomPosition, Quaternion.identity);
    }
}
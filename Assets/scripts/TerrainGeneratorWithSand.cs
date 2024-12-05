using UnityEngine;

public class TerrainGeneratorWithSand : MonoBehaviour
{
    //Header c'est comme une étiquette ca creer une box avec ce qu'il y a en dessous
    [Header("Terrain Settings")] // box settings du terrain modifiable

    public int gridHeight = 10; // Nombre de plaque sur la hauteur
    public int gridWidth = 10;  // Nombre de plaque sur la largeur de la map
    public float plateSize = 5f; // taille des plaque

    [Header("Materials")] // box des materiaux pour le moment eau et sol

    public Material GroundMaterial;
    public Material WaterMaterial;
    public Material SandMaterial;

    //box pour parametre du bruit de perlin
    [Header("Perlin Noise Settings")]
    public float noiseScale = 10f; // Echelle du bruit (logiquement plus c'est petit plus les zonne son grande)
    [Range(0, 1)]
    public float WaterMaxThreshold = 0.4f; // seuil pour définir une plaque comme eau
    [Range (0, 1)]
    public float SandMaxThreshold = 0.5f;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        for (int z = 0; z < gridHeight; z++) // tant que la hauteur est plus bas que "gridHeight" la valeur de z est augmenter de 1
        {
            for (int x = 0; x < gridWidth; x++) // a chaque itération x est incrémenté de 1
            {


                // On créer un plaque
                GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Cube);

                // on positionne la plaque sur la grille 
                plate.transform.position = new Vector3(x * plateSize, 0, z * plateSize); //détermine la position de l'objet 
                plate.transform.localScale = new Vector3(plateSize, 1, plateSize); // definit la taille/echelle de l'objet

                float noiseValue = Mathf.PerlinNoise(x / noiseScale, z / noiseScale);
                bool isWater = noiseValue < WaterMaxThreshold;
                bool isSand = noiseValue < SandMaxThreshold;

                AssignPlateType(plate, isWater, isSand);
            }

        }

    }

    void AssignPlateType(GameObject plate, bool isWater, bool isSand)
    {
        if (isWater)
        {
            plate.GetComponent<Renderer>().material = WaterMaterial;
            plate.transform.position += Vector3.down * 0.5f; // baisser la plaque si c'est de l'eau
        }
        else if (isSand)
        {
            plate.GetComponent<Renderer>().material = SandMaterial;
        }
                   else
        {
            plate.GetComponent<Renderer>().material = GroundMaterial;
        }


    }

}

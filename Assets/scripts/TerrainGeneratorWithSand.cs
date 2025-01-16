using UnityEngine;

public class TerrainGeneratorWithSand : MonoBehaviour
{
    private const float NoiseLimit = 1000000;

    //Header c'est comme une �tiquette ca creer une box avec ce qu'il y a en dessous
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
    public float WaterMaxThreshold = 0.4f; // seuil pour d�finir une plaque comme eau
    [Range (0, 1)]
    public float SandMaxThreshold = 0.5f;

   
    public void GenerateTerrain()
    {
        Vector2 noiseOffset = new Vector2(Random.Range(0, NoiseLimit), Random.Range(0, NoiseLimit));

        for (int z = 0; z < gridHeight; z++) // tant que la hauteur est plus bas que "gridHeight" la valeur de z est augmenter de 1
        {
            for (int x = 0; x < gridWidth; x++) // a chaque it�ration x est incr�ment� de 1
            {
                // On cr�er un plaque
                GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                plate.name = $"Cube ({x}; {z})";
                plate.transform.SetParent(transform);

                // on positionne la plaque sur la grille 
                plate.transform.position = new Vector3(x * plateSize, 0, z * plateSize); //d�termine la position de l'objet 
                plate.transform.localScale = new Vector3(plateSize, 1, plateSize); // definit la taille/echelle de l'objet
                
                float noiseValue = Mathf.PerlinNoise(noiseOffset.x + (float)x / gridWidth * noiseScale, noiseOffset.y + (float)z / gridHeight * noiseScale);
                //float noiseValue = Mathf.PerlinNoise(x / noiseScale + Random.Range(noiseScale, noiseScale), z / noiseScale + Random.Range(noiseScale, noiseScale));
                bool isWater = noiseValue < WaterMaxThreshold;
                bool isSand = noiseValue < SandMaxThreshold;

                AssignPlateType(plate, isWater, isSand);
            }

        }

        Debug.Log($"Generated terrain: child count = {transform.childCount}");
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

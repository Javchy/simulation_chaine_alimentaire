using UnityEngine;

public class TerrainGeneratorWithSand : MonoBehaviour
{
    private const float NoiseLimit = 1000000;

    //Header c'est comme une étiquette ca creer une box avec ce qu'il y a en dessous
    [Header("Terrain Settings")] // box settings du terrain modifiable

    public int gridHeight = 10; // Nombre de plaque sur la hauteur
    public int gridWidth = 10;  // Nombre de plaque sur la largeur de la map
    public float plateSize = 1f; // taille des plaque

    [Header("Materials")] // box des materiaux pour le moment eau et sol

    public Material GroundMaterial;
    public Material WaterMaterial;
    public Material SandMaterial;
    public Material GrassSandMaterial;
    public Material LightGrassMaterial;

    //box pour parametre du bruit de perlin
    [Header("Perlin Noise Settings")]
    public float noiseScale = 10f; // Echelle du bruit (logiquement plus c'est petit plus les zonne son grande)
    [Range(0, 1)]
    public float WaterMaxThreshold = 0.4f; // seuil pour définir une plaque comme eau
    [Range(0, 1)]
    public float SandMaxThreshold = 0.5f;
    [Range(0, 1)]
    public float GrassSandThreshold = 0.5f;
    [Range(0, 1)]
    public float LightGrassThreshold = 0.5f;


    //private LandType landType = LandType.None;

    public void GenerateTerrain()
    {
        Vector2 noiseOffset = new Vector2(Random.Range(0, NoiseLimit), Random.Range(0, NoiseLimit));

        for (int z = 0; z < gridHeight; z++) // tant que la hauteur est plus bas que "gridHeight" la valeur de z est augmenter de 1
        {
            for (int x = 0; x < gridWidth; x++) // a chaque itération x est incrémenté de 1
            {
                // On créer un plaque
                GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                plate.name = $"Cube ({x}; {z})";
                plate.transform.SetParent(transform);

                // on positionne la plaque sur la grille 
                plate.transform.position = new Vector3(x * plateSize, 0, z * plateSize); //détermine la position de l'objet 
                plate.transform.localScale = new Vector3(plateSize, 1, plateSize); // definit la taille/echelle de l'objet

                // Application du Perlin Noise pour déterminer le type de terrain
                float noiseValue = Mathf.PerlinNoise(noiseOffset.x + (float)x / gridWidth * noiseScale, noiseOffset.y + (float)z / gridHeight * noiseScale);

                // Définition du type de terrain selon le bruit
                bool isWater = noiseValue < WaterMaxThreshold;
                bool isSand = noiseValue < SandMaxThreshold;
                bool isGrassSand = noiseValue < GrassSandThreshold;
                bool isLightGrass = noiseValue < LightGrassThreshold;

                // Assignation du type de terrain à la plaque
                AssignPlateType(plate, isWater, isSand, isGrassSand, isLightGrass);
            }
        }

        Debug.Log($"Generated terrain: child count = {transform.childCount}");
    }

    void AssignPlateType(GameObject plate, bool isWater, bool isSand, bool isGrassSand, bool isLightGrass)
    {
        if (isWater)
        {
            plate.GetComponent<Renderer>().material = WaterMaterial;
            plate.transform.position += Vector3.down * 0.3f; // baisser la plaque si c'est de l'eau
            plate.tag = "Water";
        }
        else if (isSand)
        {
            plate.GetComponent<Renderer>().material = SandMaterial;
            plate.tag = "Sand";
        }
        else if (isGrassSand)
        {
            plate.GetComponent<Renderer>().material = GrassSandMaterial;
            plate.tag = "Sand";
        }
        else if (isLightGrass)
        {
            plate.GetComponent<Renderer>().material = LightGrassMaterial;
            plate.tag = "Grass";
        }
        else
        {
            plate.GetComponent<Renderer>().material = GroundMaterial;
            plate.tag = "Grass";
        }
    }
}




















//{
//    public enum LandType
//    {
//        None,

//        Sand,
//        Grass,
//        GrassSand,
//        LightGrass,
//        Water,
//        DeepWater
//    }

//    private const float NoiseLimit = 1000000;

//    Header c'est comme une étiquette ca creer une box avec ce qu'il y a en dessous
//    [Header("Terrain Settings")] // box settings du terrain modifiable

//    public int gridHeight = 10; // Nombre de plaque sur la hauteur
//    public int gridWidth = 10;  // Nombre de plaque sur la largeur de la map
//    public float plateSize = 1f; // taille des plaque

//    [Header("Materials")] // box des materiaux pour le moment eau et sol

//    public Material GroundMaterial;
//    public Material WaterMaterial;
//    public Material SandMaterial;
//    public Material GrassSandMaterial;
//    public Material LightGrassMaterial;

//    box pour parametre du bruit de perlin
//    [Header("Perlin Noise Settings")]
//    public float noiseScale = 10f; // Echelle du bruit (logiquement plus c'est petit plus les zonne son grande)
//    [Range(0, 1)]
//    public float WaterMaxThreshold = 0.4f; // seuil pour définir une plaque comme eau
//    [Range(0, 1)]
//    public float SandMaxThreshold = 0.5f;
//    [Range(0, 1)]
//    public float GrassSandThreshold = 0.5f;
//    [Range(0, 1)]
//    public float LightGrassThreshold = 0.5f;


//    private LandType landType = LandType.None;

//    public void GenerateTerrain()
//    {
//        Vector2 noiseOffset = new Vector2(Random.Range(0, NoiseLimit), Random.Range(0, NoiseLimit));

//        for (int z = 0; z < gridHeight; z++) // tant que la hauteur est plus bas que "gridHeight" la valeur de z est augmenter de 1
//        {
//            for (int x = 0; x < gridWidth; x++) // a chaque itération x est incrémenté de 1
//            {
//                On créer un plaque
//                GameObject plate = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                plate.name = $"Cube ({x}; {z})";
//                plate.transform.SetParent(transform);

//                on positionne la plaque sur la grille
//                plate.transform.position = new Vector3(x * plateSize, 0, z * plateSize); //détermine la position de l'objet 
//                plate.transform.localScale = new Vector3(plateSize, 1, plateSize); // definit la taille/echelle de l'objet

//                float noiseValue = Mathf.PerlinNoise(noiseOffset.x + (float)x / gridWidth * noiseScale, noiseOffset.y + (float)z / gridHeight * noiseScale);
//                float noisevalue = mathf.perlinnoise(x / noisescale + random.range(noisescale, noisescale), z / noisescale + random.range(noisescale, noisescale));
//                bool iswater = noisevalue < watermaxthreshold;
//                bool issand = noisevalue < sandmaxthreshold;
//                bool isgrasssand = noisevalue < grasssandthreshold;
//                bool islightgrass = noisevalue < lightgrassthreshold;

//                assignplatetype(plate, iswater, issand, isgrasssand, islightgrass);





//                if (noiseValue < WaterMaxThreshold)
//                    landType = LandType.Water;

//                if (noiseValue < SandMaxThreshold)
//                    landType = LandType.Sand;

//                if (noiseValue < GrassSandThreshold)
//                    landType = LandType.GrassSand;

//                if (noiseValue < LightGrassThreshold)
//                    landType = LandType.LightGrass;

//                bool isWater = noiseValue < WaterMaxThreshold;
//                bool isSand = noiseValue < SandMaxThreshold;
//                bool isGrassSand = noiseValue < GrassSandThreshold;
//                bool isLightGrass = noiseValue < LightGrassThreshold;


//                AssignLandType(landType);
//            }

//        }

//        Debug.Log($"Generated terrain: child count = {transform.childCount}");
//    }

//    void AssignPlateType(GameObject plate, bool isWater, bool isSand, bool isGrassSand, bool isLightGrass, bool isDeepWater)
//    {
//        if (isWater)
//        {
//            plate.GetComponent<Renderer>().material = WaterMaterial;
//            plate.transform.position += Vector3.down * 0.3f; // baisser la plaque si c'est de l'eau
//            plate.tag = "Water";
//        }

//        else if (isSand)
//        {
//            plate.GetComponent<Renderer>().material = SandMaterial;
//            plate.tag = "Sand";
//        }

//        else if (isGrassSand)
//        {
//            plate.GetComponent<Renderer>().material = GrassSandMaterial;
//            plate.tag = "Sand";
//        }

//        else if (isLightGrass)
//        {
//            plate.GetComponent<Renderer>().material = LightGrassMaterial;
//            plate.tag = "Grass";
//        }

//        else
//        {
//            plate.GetComponent<Renderer>().material = GroundMaterial;

//            plate.tag = "Grass";
//        }
//    }
//}
//private void AssignLandType(LandType type)
//{

//    switch (type)
//    {
//        case LandType.Sand:
//            Debug.Log("truc");
//            break;

//        case LandType.GrassSand:
//            break;

//        case LandType.Grass:
//            break;

//        default:
//            break;
//    }
//}

//}

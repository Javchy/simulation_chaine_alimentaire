using System.Collections.Generic;
using UnityEngine;

public class YackGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Yack
    {
        public string Name;
        public string Gender;
        public GameObject Model;

        public Yack(string name, string gender, GameObject model)
        {
            Name = name;
            Gender = gender;
            Model = model;
        }
    }

    public List<Yack> yacks = new List<Yack>(); // Liste des yacks générés

    // Préfabriqués des yacks (assignés depuis l'éditeur ou via Resources)
    public GameObject maleYackPrefab;
    public GameObject femaleYackPrefab;

    // Start est appelé au lancement
    void Start()
    {
        GenerateYacks();
    }

    void GenerateYacks()
    {
        int numberOfYacks = Random.Range(3, 21); // Nombre aléatoire de yacks entre 3 et 20
        for (int i = 0; i < numberOfYacks; i++)
        {
            string gender = Random.value > 0.5f ? "Female" : "Male"; // Sexe aléatoire
            GameObject prefab = gender == "Female" ? femaleYackPrefab : maleYackPrefab;

            // Instancier le modèle 3D
            GameObject yackModel = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
            yackModel.name = $"Yack_{i + 1}";

            // Ajouter le yack à la liste
            yacks.Add(new Yack(yackModel.name, gender, yackModel));
        }
    }

    // Génère une position aléatoire dans la scène
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, 0, z); // Position au sol
    }
}
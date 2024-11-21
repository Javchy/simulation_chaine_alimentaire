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

    public List<Yack> yacks = new List<Yack>(); // Liste des yacks g�n�r�s

    // Pr�fabriqu�s des yacks (assign�s depuis l'�diteur ou via Resources)
    public GameObject maleYackPrefab;
    public GameObject femaleYackPrefab;

    // Start est appel� au lancement
    void Start()
    {
        GenerateYacks();
    }

    void GenerateYacks()
    {
        int numberOfYacks = Random.Range(3, 21); // Nombre al�atoire de yacks entre 3 et 20
        for (int i = 0; i < numberOfYacks; i++)
        {
            string gender = Random.value > 0.5f ? "Female" : "Male"; // Sexe al�atoire
            GameObject prefab = gender == "Female" ? femaleYackPrefab : maleYackPrefab;

            // Instancier le mod�le 3D
            GameObject yackModel = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
            yackModel.name = $"Yack_{i + 1}";

            // Ajouter le yack � la liste
            yacks.Add(new Yack(yackModel.name, gender, yackModel));
        }
    }

    // G�n�re une position al�atoire dans la sc�ne
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, 0, z); // Position au sol
    }
}
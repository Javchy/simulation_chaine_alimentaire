using System.Collections.Generic;
using UnityEngine;



public class PanthereGenerator : MonoBehaviour
{
    [System.Serializable]
    public class panthere
    {
        public string Name;
        public string Gender;
        public GameObject Model;

        public panthere(string name, string gender, GameObject model)
        {
            Name = name;
            Gender = gender;
            Model = model;
        }

    }

    public List<panthere> pantheres = new List<panthere>();
    public GameObject FemalePantherePrefab;
    public GameObject MalePantherePrefab;

    void Start()
    {
        startingpantheres();
    }

    void startingpantheres()
    {
        int numberOfPanther = Random.Range(2, 4);
        for (int i = 0; i < numberOfPanther; i++)
        {
            string gender = Random.value > 0.5f ? "Female" : "Male";
            GameObject prefab = gender == "Female" ? FemalePantherePrefab : MalePantherePrefab;

            GameObject pantheremodel = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
            pantheremodel.name = $"panthere{i + 1}";

            pantheres.Add(new panthere(pantheremodel.name, gender, pantheremodel));
        }
        


    }

    public void spwan1panthere()
    {
        string gender = Random.value > 0.5f ? "Female" : "Male";
        GameObject prefab = gender == "Female" ? FemalePantherePrefab : MalePantherePrefab;

        GameObject pantheremodel = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        pantheremodel.name = $"panthere";

        pantheres.Add(new panthere(pantheremodel.name, gender, pantheremodel));
    }

    
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(-20f, 20f);
        return new Vector3(x, 0, z);
    }


}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGeneratorWithSand))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        if (GUILayout.Button("Generate"))
        {
            TerrainGeneratorWithSand generator = target as TerrainGeneratorWithSand;
            for (int i = generator.transform.childCount; i > 0; i--)
            {
                DestroyImmediate(generator.transform.GetChild(0).gameObject);
            }

            generator.GenerateTerrain();
        }
           
    }
}

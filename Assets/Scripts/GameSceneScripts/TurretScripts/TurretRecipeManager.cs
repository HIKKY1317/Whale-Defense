using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TurretRecipeManager : MonoBehaviour
{
    private Dictionary<string, string> recipeDict;

    void Start()
    {
        LoadRecipes();
    }

    void LoadRecipes()
    {
        recipeDict = new Dictionary<string, string>();
        TextAsset recipeFile = Resources.Load<TextAsset>("recipe");

        if (recipeFile == null)
        {
            Debug.LogError("Recipe file not found.");
            return;
        }

        string[] lines = recipeFile.text.Split('\n');

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine) || !trimmedLine.Contains("="))
                continue;

            string[] parts = trimmedLine.Split('=');
            if (parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                recipeDict[key] = value;
            }
        }
    }

    public GameObject GetMergedTurret(GameObject selectedTurret, GameObject existingTurret)
    {
        if (selectedTurret == null || existingTurret == null)
            return null;

        string selectedName = selectedTurret.name.Replace("(Clone)", "").Trim();
        string existingName = existingTurret.name.Replace("(Clone)", "").Trim();

        string key1 = selectedName + "+" + existingName;
        string key2 = existingName + "+" + selectedName;

        if (recipeDict.TryGetValue(key1, out string mergedTurretName) ||
            recipeDict.TryGetValue(key2, out mergedTurretName))
        {
            GameObject mergedPrefab = Resources.Load<GameObject>($"Turret/{mergedTurretName}");
            if (mergedPrefab != null)
                return mergedPrefab;
            else
                Debug.LogError($"Prefab Turret/{mergedTurretName} not found in Resources.");
        }

        return null;
    }
}

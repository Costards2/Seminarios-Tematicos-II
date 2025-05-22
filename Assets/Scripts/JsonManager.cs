using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonManager
{
    // Store the questionnaire in the persistent data path (ideal for saved user data)
    private static string jsonFilePath = Path.Combine(Application.persistentDataPath, "questionnaire.json");

    // Save questionnaire to JSON file
    public static void SaveQuestionnaireData(Questionnaire questionnaire)
    {
        string jsonData = JsonUtility.ToJson(questionnaire, true);
        File.WriteAllText(jsonFilePath, jsonData);
        Debug.Log("Questionnaire data saved to: " + jsonFilePath);
    }

    // Load questionnaire from JSON file
    public static Questionnaire LoadQuestionnaireData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            Questionnaire questionnaire = JsonUtility.FromJson<Questionnaire>(jsonData);
            Debug.Log("Questionnaire data loaded from: " + jsonFilePath);
            return questionnaire;
        }
        else
        {
            Debug.LogWarning("File not found: " + jsonFilePath);
            return new Questionnaire();
        }
    }
}

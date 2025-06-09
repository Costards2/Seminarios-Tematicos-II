using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "questionnaire.json");

    public static void SaveQuestionnaireData(Questionnaire questionnaire)
    {
        string json = JsonUtility.ToJson(questionnaire, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Questionnaire saved to persistentDataPath.");
    }

    public static Questionnaire LoadQuestionnaireData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Persistent questionnaire not found, syncing from Resources...");
            SyncQuestionnaireFromResources(true);
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Questionnaire>(json);
    }

    public static void SyncQuestionnaireFromResources(bool force = false)
    {
        if (File.Exists(filePath) && !force)
        {
            Debug.Log("Persistent questionnaire already exists. Skipping sync.");
            return;
        }

        TextAsset resourceFile = Resources.Load<TextAsset>("questionnaire");
        if (resourceFile == null)
        {
            Debug.LogError("questionnaire.json not found in Resources!");
            return;
        }

        File.WriteAllText(filePath, resourceFile.text);
        Debug.Log("Questionnaire synced from Resources to persistent path.");
    }

    public static void SyncIfQuestionnaireOutdated()
    {
        TextAsset resourceFile = Resources.Load<TextAsset>("questionnaire");
        if (resourceFile == null)
        {
            Debug.LogError("questionnaire.json not found in Resources!");
            return;
        }

        Questionnaire resourceQuestionnaire = JsonUtility.FromJson<Questionnaire>(resourceFile.text);

        Questionnaire persistentQuestionnaire = null;
        if (File.Exists(filePath))
        {
            string persistentJson = File.ReadAllText(filePath);
            persistentQuestionnaire = JsonUtility.FromJson<Questionnaire>(persistentJson);
        }

        int lastResourceID = GetLastQuestionID(resourceQuestionnaire);
        int lastPersistentID = GetLastQuestionID(persistentQuestionnaire);

        if (lastResourceID != lastPersistentID)
        {
            Debug.LogWarning($"Outdated questionnaire detected. Resources (lastID={lastResourceID}) ≠ Persistent (lastID={lastPersistentID}). Syncing...");
            SyncQuestionnaireFromResources(true);
        }
        else
        {
            Debug.Log("Questionnaire is up to date.");
        }
    }

    private static int GetLastQuestionID(Questionnaire questionnaire)
    {
        if (questionnaire == null || questionnaire.questions == null || questionnaire.questions.Count == 0)
            return -1;

        questionnaire.questions.Sort((a, b) => a.questionID.CompareTo(b.questionID));
        return questionnaire.questions[^1].questionID;
    }
}

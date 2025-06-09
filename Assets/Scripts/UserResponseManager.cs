using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UserResponseManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "userResponses.json");

    public static UserResponseList LoadAllUserResponses()
    {
        if (!File.Exists(filePath))
            return new UserResponseList();

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<UserResponseList>(json);
    }

    public static void SaveUserResponse(UserResponse newResponse)
    {
        UserResponseList allResponses = LoadAllUserResponses();

        allResponses.userResponses.Add(newResponse);

        string updatedJson = JsonUtility.ToJson(allResponses, true);
        File.WriteAllText(filePath, updatedJson);
        Debug.Log("User response saved.");
    }
}

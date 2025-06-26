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

    /*public static void SaveUserResponse(UserResponse newResponse)
    {
        UserResponseList allResponses = LoadAllUserResponses();

        allResponses.userResponses.Add(newResponse);

        string updatedJson = JsonUtility.ToJson(allResponses, true);
        File.WriteAllText(filePath, updatedJson);
        Debug.Log("User response saved.");
    }*/

    public static void SaveUserResponse(UserResponse newResponse)
    {
        UserResponseList allResponses = LoadAllUserResponses();

        // Try to find an existing user with the same ID
        UserResponse existingUser = allResponses.userResponses.Find(u => u.userID == newResponse.userID);

        if (existingUser != null)
        {
            foreach (var newAnswer in newResponse.answers)
            {
                // Check if this question has already been answered
                var existingAnswer = existingUser.answers.Find(a => a.questionID == newAnswer.questionID);
                if (existingAnswer != null)
                {
                    // Update the existing answer
                    existingAnswer.response = newAnswer.response;
                }
                else
                {
                    // Add new answer
                    existingUser.answers.Add(newAnswer);
                }
            }
        }
        else
        {
            // User doesn't exist, so add new entry
            allResponses.userResponses.Add(newResponse);
        }

        // Write back to the JSON file
        string updatedJson = JsonUtility.ToJson(allResponses, true);
        File.WriteAllText(filePath, updatedJson);
        Debug.Log("User response saved or updated.");
    }

}

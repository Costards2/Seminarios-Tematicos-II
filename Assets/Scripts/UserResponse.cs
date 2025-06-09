using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserResponse
{
    public string userID;
    public List<Answer> answers = new List<Answer>();

    public UserResponse(string userID)
    {
        this.userID = userID;
    }
}

[System.Serializable]
public class UserResponseList
{
    public List<UserResponse> userResponses = new List<UserResponse>();
}
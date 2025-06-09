using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    //so pra compilar de novo
    public int questionID;
    public string response;

    public Answer(int questionID, string response)
    {
        this.questionID = questionID;
        this.response = response;
    }
}

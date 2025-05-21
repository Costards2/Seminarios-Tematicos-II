using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questionnaire
{
    public List<Question> questions = new List<Question>();

    public void AddQuestion(int id, string text, List<string> opts, string questionType)
    {
        questions.Add(new Question(id, text, opts, questionType));
    }
}
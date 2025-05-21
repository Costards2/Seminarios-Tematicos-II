using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    public int questionID;
    public string questionText;
    public List<string> options;
    public string type; //input de texto ou múltipla escolha
    
    public Question(int id, string text, List<string> opts, string questionType)
    {
        questionID = id;
        questionText = text;
        options = opts;
        type = questionType;
    }
}

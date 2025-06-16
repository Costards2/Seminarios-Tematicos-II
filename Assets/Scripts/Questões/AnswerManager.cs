using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] private UserResponseList respostas;
    public UserResponse newUser;
    int questionID;

    void Start()
    {
        respostas = new UserResponseList();
        newUser = new UserResponse("user001");
        respostas.userResponses.Add(newUser);
    }

    public void ChangeOptions(int answerID)
    {
        Questionnaire questionnaire = GetComponent<GameManager>().questionnaire;
        Transform answers = GetComponent<QuestionManager>().answers.transform;
        int questionID = GetComponent<QuestionManager>().currentQuestionID;
        
        string responseText = questionnaire.questions.Find(q => q.questionID == questionID).options[answerID];
        
        Answer answer = new Answer(questionID, responseText);

        switch (answers.GetChild(answerID).GetComponent<Toggle>().isOn)
        {
            case true: 
                newUser.answers.Add(answer);
                break;
            case false:
                newUser.answers.RemoveAll(q => q.response == responseText && q.questionID == questionID);
                break;
        }
    }
}

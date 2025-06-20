using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public int currentQuestionID;
    string baseQuestionTitle = "PERGUNTA #";
    
    [SerializeField] TextMeshProUGUI questionTitle; //Título
    [SerializeField] TextMeshProUGUI questionText; //Enunciado
    [SerializeField] public GameObject answers; //Alternativas
    
    void Start()
    {
        currentQuestionID = 6; //Começa no 6, para pular placeholders
        ShowQuestion(currentQuestionID);
    }

    public void Continue() //Escuta o botão Continuar
    {
        if (GetComponent<AnswerManager>().selectedAnswers == 0) return;// Verificar se há respostas selecionadas
        
        //Checar o número limite de questões
        if (currentQuestionID < this.GetComponent<GameManager>().questionnaire.questions.Count - 1)
        {
            currentQuestionID++;
            ShowQuestion(currentQuestionID);
            GetComponent<AnswerManager>().selectedAnswers = 0;
        }
        else if (currentQuestionID >= this.GetComponent<GameManager>().questionnaire.questions.Count - 1)
        {
            SceneManager.LoadScene("Scenes/Tela_Menu");
            UserResponseManager.SaveUserResponse(GetComponent<AnswerManager>().newUser);
        }
    }

    public void Back() //Escuta o botão Voltar
    {
        if (currentQuestionID > 6)
        {
            currentQuestionID--;
            ShowQuestion(currentQuestionID);
        }
    }
    
    void ShowQuestion(int ID) //Muda o texto do conteúdo da UI
    {
        //Pega as perguntas
        Questionnaire questionnaire = this.GetComponent<GameManager>().questionnaire;
        Question question = questionnaire.questions[ID];

        int questionID = question.questionID - 5;
        
        questionTitle.text = baseQuestionTitle + questionID.ToString(); //Define o texto do título com o número
        questionText.text = question.questionText; //Define texto do enunciado

        foreach (Transform answer in answers.transform) //Pega todas as alternativas
        {
            //Texto de cada alternativa
            answer.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.options[answer.transform.GetSiblingIndex()];
            
            //Ao continuar, desativa os Toggles
            answer.GetComponent<Toggle>().isOn = false;
        }
    }
}
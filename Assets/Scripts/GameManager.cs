using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Questionnaire questionnaire;

    void Start()
    {
        // Load from JSON file
        questionnaire = JsonManager.LoadQuestionnaireData();

        // Only add if it's empty (avoids duplicating on every launch)
        if (questionnaire.questions == null || questionnaire.questions.Count == 0)
        {
            AddSampleQuestions();
            SaveQuestionnaireData();
        }

        DisplayQuestions();
    }

    void AddSampleQuestions()
    {
        questionnaire = new Questionnaire(); // Ensure it's initialized

        questionnaire.AddQuestion(1, "Qual o seu nome?", new List<string>(), "text");
        questionnaire.AddQuestion(2, "Qual a sua idade?", new List<string> { "13 a 15 anos", "16 a 18 anos" }, "multipleChoice");
        questionnaire.AddQuestion(3, "Qual é a sua identidade de gênero?", new List<string>
        {
            "Agênero", "Feminino", "Gênero-fluido", "Homem trans", "Intersexo", "Masculino", "Mulher trans",
            "Não binário", "Prefiro não responder", "Outro (identidade diferente das opções acima)"
        }, "multipleChoice");
        questionnaire.AddQuestion(4, "Qual sua etnia?", new List<string>
        {
            "Amarela", "Branca", "Indígena", "Parda", "Preta"
        }, "multipleChoice");
        questionnaire.AddQuestion(5, "Qual dispositivo você usa para jogar?", new List<string>
        {
            "Dispositivos móveis (como celulares e tablets)", "Computador", "Console", "Outro"
        }, "multipleChoice");
        questionnaire.AddQuestion(6, "Quanto tempo você joga por semana?", new List<string>
        {
            "Menos de 3 horas por semana", "Entre 3 e 7 horas por semana", "Entre 8 e 14 horas por semana",
            "Mais de 14 horas por semana", "Prefiro não responder"
        }, "multipleChoice");
    }

    void DisplayQuestions()
    {
        foreach (var question in questionnaire.questions)
        {
            Debug.Log($"Question: {question.questionText}");
            if (question.options != null && question.options.Count > 0)
            {
                foreach (var option in question.options)
                {
                    Debug.Log($"Option: {option}");
                }
            }
            else
            {
                Debug.Log("Open-ended question.");
            }
        }
    }

    void SaveQuestionnaireData()
    {
        JsonManager.SaveQuestionnaireData(questionnaire);
        Debug.Log("Questionnaire saved.");
    }
}

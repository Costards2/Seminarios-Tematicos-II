using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Questionnaire questionnaire;

    void Start()
    {
        // Carrega os dados do questionário do arquivo JSON
        JsonManager.SyncIfQuestionnaireOutdated();
        questionnaire = JsonManager.LoadQuestionnaireData();

        // Cria um questionário com perguntas de exemplo se não houver perguntas carregadas
        if (questionnaire.questions == null || questionnaire.questions.Count == 0)
        {
            AddSampleQuestions();
            SaveQuestionnaireData();
        }

        Questionnaire all = DisplayQuestions();
        Questionnaire single = DisplayQuestions(5);
        Questionnaire range = DisplayQuestions(4, 7);

        SaveSampleUserAnswers();
    }

    void AddSampleQuestions()
    {
        questionnaire = new Questionnaire();

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

    // 1. Pega todas as perguntas
    Questionnaire DisplayQuestions()
    {
        Questionnaire result = new Questionnaire();

        foreach (var question in questionnaire.questions)
        {
            DisplaySingleQuestion(question);
            result.questions.Add(question);
        }

        return result;
    }

    // 2. Pega uma pergunta específica por ID
    Questionnaire DisplayQuestions(int questionID)
    {
        Questionnaire result = new Questionnaire();

        var question = questionnaire.questions.Find(q => q.questionID == questionID);
        if (question != null)
        {
            DisplaySingleQuestion(question);
            result.questions.Add(question);
        }
        else
        {
            Debug.LogWarning($"Question with ID {questionID} not found.");
        }

        return result;
    }

    // 3. Pega um intervalo de perguntas por ID
    Questionnaire DisplayQuestions(int fromID, int toID)
    {
        Questionnaire result = new Questionnaire();

        if (fromID > toID)
        {
            Debug.LogWarning("Invalid range: fromID is greater than toID.");
            return result;
        }

        var range = questionnaire.questions.FindAll(q => q.questionID >= fromID && q.questionID <= toID);

        if (range.Count == 0)
        {
            Debug.LogWarning($"No questions found in range {fromID} to {toID}.");
            return result;
        }

        foreach (var question in range)
        {
            DisplaySingleQuestion(question);
            result.questions.Add(question);
        }

        return result;
    }

    //Esse método aqui é só pra não repetir código nos três de cima
    void DisplaySingleQuestion(Question question)
    {
        Debug.Log($"Question {question.questionID}: {question.questionText}");

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

    void SaveQuestionnaireData()
    {
        JsonManager.SaveQuestionnaireData(questionnaire);
        Debug.Log("Questionnaire saved.");
    }

    void SaveSampleUserAnswers()
    {
        UserResponse user = new UserResponse("user003");

        user.answers.Add(new Answer(0, "0"));
        user.answers.Add(new Answer(1, "0"));
        user.answers.Add(new Answer(2, "Marcela"));
        user.answers.Add(new Answer(3, "13 a 15 anos"));
        user.answers.Add(new Answer(4, "Feminino"));
        user.answers.Add(new Answer(5, "Branca"));
        user.answers.Add(new Answer(6, "Console"));
        user.answers.Add(new Answer(7, "Menos de 3 horas por semana"));

        UserResponseManager.SaveUserResponse(user);
    }
}

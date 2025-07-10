using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;



public class Manager : MonoBehaviour
{
    [SerializeField] private int actualQuest;
    private int questnumber;
    
    [Header("Botões")]
    [SerializeField] private Button proximo;
    [SerializeField] private Button anterior;

    public SendResponseEvent SendResponse;
    public QuestEvent SendQuest;

    private FilterManager filter;
    public int idadeMin;
    public int idadeMax;
    public bool isWoman;
    public bool isMan;
    [Tooltip("Aqui você coloca o id da primeira pergunta que você quer exibir nas respostas")][SerializeField] private int idInitial;
    [HideInInspector] public questionnaire actualQuestionnaire;
    [HideInInspector] public UserResponseList responses;           // Lista contendo as respostas
     public UserResponseList filteredResponse; // Lista contendo as respostas filtradas pela idade e sexo 
     public UserResponseList fullFilteredList; // Lista contendo somente as questões relativas ao index atual (já está filtrada pela idade e sexo)
    [SerializeField] private string jsonName;
    [SerializeField] private string jsonResponseName;
    QuestManager questManager;
    public bool eventActived;

    void Start()
    {
        eventActived = false;
        // Lê as quests
        ReadQuests();

        // Setando valores iniciais de variaveis
        actualQuest = idInitial;
        questnumber = actualQuestionnaire.questions.Count - idInitial;
        filter = FindAnyObjectByType<FilterManager>(); // Pegando as informações de filtro do objeto FilterManage da cena Tela_filter
        if (filter != null)
        {
            idadeMin = filter.idadeMin; idadeMax = filter.idadeMax; isWoman = filter.isWoman; isMan = filter.isMan;
            Destroy(filter.gameObject);
        }

        ReadAndFilterAwnsers();

        // Eventos iniciais
        SendQuest.Invoke(actualQuestionnaire.questions[idInitial]);
        SendResponse.Invoke(actualAnswerList(filteredResponse, actualQuest));

    }


    void Update()
    {
        // Ativador dos botões anterior e próximo
        if (actualQuest == idInitial)
        {
            anterior.gameObject.SetActive(false);
        }else
        {
            anterior.gameObject.SetActive(true);

            if (actualQuest == questnumber + idInitial - 1) 
            {
                proximo.gameObject.SetActive(false);
            }
            else 
            {
                proximo.gameObject.SetActive(true);
            }
        }
    }

    private void ReadQuests() 
    {
 
        actualQuestionnaire = JsonUtility.FromJson<questionnaire>(LeitorDeJSON(jsonName));
        questnumber = actualQuestionnaire.questions.Count - idInitial;

        /* Versão 1.0 do código de leitura ( lê o arquivo json da pasta ressources)
          
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);
        if (File.Exists(caminho)) 
           
                //actualQuestionnaire = JsonUtility.FromJson<questionnaire>(jsonFile.text);
                //Debug.Log("Quantidade de perguntas: " + questnumber);
                //Debug.Log("Primeira pergunta: " + actualQuestionnaire.questions[idInitial].questionText); 
         */

    }

    private void ReadAndFilterAwnsers()
    {
        responses = JsonUtility.FromJson<UserResponseList>(LeitorDeJSON(jsonResponseName));

        filteredResponse = new UserResponseList();

        // olho todos os entrevistados
        foreach(UserResponse response in responses.userResponses) 
        {
            //olho todas as perguntas de cada entrevistado
            foreach (Answer answer in response.answers) 
            {
                /*if (this.idadeMin <= 13 && this.idadeMax >= 18)
                {
                    for (int i = 0; i < response.answers.Count; i++)
                    {
                        if (response.answers[i].response == "Feminino" && isWoman)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                        if (response.answers[i].response == "Masculino" && isMan)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                    }
                }*/
                if (answer.response == "13 a 15" && this.idadeMin >= 13 && this.idadeMax <=15) 
                {
                    for (int i = 0; i < response.answers.Count; i++)
                    {
                        if (response.answers[i].response == "Feminino" && isWoman)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                        if (response.answers[i].response == "Masculino" && isMan)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                    }
                }
                
                if (answer.response == "16 a 18" && this.idadeMin >= 16 && this.idadeMax <= 18)
                {
                    Debug.Log("tem 16");
                    for (int i = 0; i < response.answers.Count; i++) 
                    {
                        if (response.answers[i].response == "Feminino" && isWoman) 
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                        if (response.answers[i].response == "Masculino" && isMan)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                    }
                }

                if (this.idadeMin <= 13 && this.idadeMax >= 17)
                {
                    for (int i = 0; i < response.answers.Count; i++)
                    {
                        if (response.answers[i].response == "Feminino" && isWoman)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                        if (response.answers[i].response == "Masculino" && isMan)
                        {
                            filteredResponse.userResponses.Add(response);
                        }
                    }
                }
            }
        }
    }

    private string LeitorDeJSON(string fileName) 
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return json;
        }
        else
        {
            Debug.Log("Arquivo não encontrado");
            return null;
        }
    }

    public void NextButtom() 
    {
        actualQuest++;
        SendQuest.Invoke(actualQuestionnaire.questions[actualQuest]);
        SendResponse.Invoke(actualAnswerList(filteredResponse, actualQuest));
        /*for (int i = 0; i < filteredResponse.userResponses.Count; i++) 
        {
            for (int j = 0; j < filteredResponse.userResponses[j].answers.Count; j++) 
            {
                if (actualQuest == filteredResponse.userResponses[i].answers[j].questionID)
                {
                    SendResponse.Invoke(actualAnswerList(filteredResponse, actualQuest));
                    eventActived = true;
                }
                j++;
            }
            i++;
        }
        if (eventActived == false) 
        {
            Debug.Log("os index da questão e da resposta não são compatíveis");
        }else eventActived = false;*/
    }

    public void PreviewButtom()
    {
        if (actualQuest > idInitial) 
        {
            actualQuest--;
            SendQuest.Invoke(actualQuestionnaire.questions[actualQuest]);
            SendResponse.Invoke(actualAnswerList(filteredResponse, actualQuest));
            /*for (int i = 0; i < filteredResponse.userResponses.Count; i++)
            {
                for (int j = 0; j < filteredResponse.userResponses[j].answers.Count; j++)
                {
                    if (actualQuest == filteredResponse.userResponses[i].answers[j].questionID)
                    {
                        SendResponse.Invoke(actualAnswerList(filteredResponse, actualQuest));
                        eventActived = true;
                    }
                    j++;
                }
                i++;
            }
            if (eventActived == false)
            {
                Debug.Log("os index da questão e da resposta não são compatíveis");
            }
            else eventActived = false;*/
        }
    }
    public void Scene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public IdexableAnswers actualAnswerList(UserResponseList filteredList, int index) 
    {
        IdexableAnswers sendableAnswersFiltered = new IdexableAnswers();
        sendableAnswersFiltered.index = index;
        sendableAnswersFiltered.sendableAnswers = new List<Answer>();

        //Olho todos os entrevistados e pego as questões do index correto assim preenchendo o IdexableAnswers
        
        // Entrevistados
        for (int i = 0; i < filteredList.userResponses.Count; i++)
        {
            //Questões das entrevistas
            if (index < filteredList.userResponses[i].answers.Count)
            {
                sendableAnswersFiltered.sendableAnswers.Add(filteredList.userResponses[i].answers[index]);
            }else return null;
        }
        return sendableAnswersFiltered;
    }

    //---------------------------------------------------------------------------------------------------------


    // Eventos personalidzdos
    [System.Serializable]
    public class QuestEvent : UnityEvent<Quest> { }

    [System.Serializable]
    public class SendResponseEvent : UnityEvent<IdexableAnswers> { }


    // Classes
    [System.Serializable]
    public class questionnaire 
    {
        public List<Quest> questions = new List<Quest>();
    }

 
}

[System.Serializable]
public class IdexableAnswers
{
    public int index;
    public List<Answer> sendableAnswers = new List<Answer>();
}

[System.Serializable]
public class Quest
{
    public int questionID;
    public string questionText;
    public List<string> options;
    public string type;
}

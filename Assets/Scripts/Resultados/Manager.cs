using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class Manager : MonoBehaviour
{
    [SerializeField] private int actualQuest;
    private int questnumber;
    
    [Header("Botões")]
    [SerializeField] private Button proximo;
    [SerializeField] private Button anterior;

    public UnityEvent OnChangeIndex;
    public QuestEvent SendQuest;

    private FilterManager filter;
    public int idadeMin;
    public int idadeMax;
    public bool isWoman;
    public bool isMan;
    [Tooltip("Aqui você coloca o id da primeira pergunta que você quer exibir nas respostas")][SerializeField] private int idInitial;
    [HideInInspector] public questionnaire actualQuestionnaire;
    [SerializeField] private string jsonName;
    QuestManager questManager;

    void Start()
    {
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



        // Evento inicial
        SendQuest.Invoke(actualQuestionnaire.questions[idInitial]);

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
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);
        if (jsonFile != null) 
        {
            actualQuestionnaire = JsonUtility.FromJson<questionnaire>(jsonFile.text);
            Debug.Log("Quantidade de perguntas: " + questnumber);
            Debug.Log("Primeira pergunta: " + actualQuestionnaire.questions[idInitial].questionText);

        }
        else 
        {
            Debug.Log("Voce nao colocou o arquivo json na pasta Ressources ou nao colocou o nome do arquivo (sem a extensao) na variavel jsonfile");
        }  
    }

    public void NextButtom() 
    {
        actualQuest++;
        SendQuest.Invoke(actualQuestionnaire.questions[actualQuest]);
        OnChangeIndex.Invoke();
    }

    public void PreviewButtom()
    {
        if (actualQuest > idInitial) 
        {
            actualQuest--;
            SendQuest.Invoke(actualQuestionnaire.questions[actualQuest]);
            OnChangeIndex.Invoke();
        }
    }
    public void Scene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }


    //---------------------------------------------------------------------------------------------------------


    // Eventos personalidzdos
    [System.Serializable]
    public class QuestEvent : UnityEvent<Quest> { }


    // Classes
    [System.Serializable]
    public class questionnaire 
    {
        public List<Quest> questions = new List<Quest>();
    }

 
}

[System.Serializable]
public class Quest
{
    public int questionID;
    public string questionText;
    public List<string> options;
    public string type;
}

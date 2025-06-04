using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class Manager : MonoBehaviour
{
    [SerializeField] private int questID;
    private int questnumber;
    
    [Header("Botões")]
    [SerializeField] private Button proximo;
    [SerializeField] private Button anterior;

    public UnityEvent OnChangeIndex;

    private FilterManager filter;
    public int idadeMin;
    public int idadeMax;
    public bool isWoman;
    public bool isMan;

    void Start()
    {
        // Iniciando o controlador dos botões
        questID = 0;
        questnumber = 5;

        // Pegando as informações de filtro do objeto FilterManage da cena Tela_filter
        filter = FindAnyObjectByType<FilterManager>();

        if (filter != null) 
        {
            idadeMin = filter.idadeMin; idadeMax = filter.idadeMax; isWoman = filter.isWoman; isMan = filter.isMan;
            Destroy(filter.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ativador dos botões anterior e próximo
        if (questID == 0)
        {
            anterior.gameObject.SetActive(false);
        }else
        {
            anterior.gameObject.SetActive(true);

            if (questID == questnumber - 1) 
            {
                proximo.gameObject.SetActive(false);
            }
            else 
            {
                proximo.gameObject.SetActive(true);
            }
        }
    }

    public void NextButtom() 
    {
        questID++;
        OnChangeIndex.Invoke();
    }
    public void PreviewButtom()
    {
        questID--;
        OnChangeIndex.Invoke();
    }
    public void Scene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}

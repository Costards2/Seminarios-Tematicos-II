using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [SerializeField] private int questID;
    private int questnumber;
    
    [Header("Botões")]
    [SerializeField] private Button proximo;
    [SerializeField] private Button anterior;

    [Header("CenaMenu")]
    [SerializeField] private string menuNameScene;

    void Start()
    {
        // Iniciando o controlador dos botões
        questID = 0;
        questnumber = 5;


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
    }
    public void PreviewButtom()
    {
        questID--;
    }
    public void ExitButtom()
    {
        SceneManager.LoadScene(menuNameScene);
    }
}

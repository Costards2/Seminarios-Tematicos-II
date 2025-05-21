using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TermoManager : MonoBehaviour
{
    public Toggle aceito;
    public TMP_Text erro;

    public void Continuar()
    {
        bool aceito = this.aceito.isOn;
        
        if (aceito)
        {
            SceneManager.LoadScene("Tela_Questionario");
        }
        else
        {
            Debug.Log("Não Aceitou os Termo");
            erro.text = "Não Aceitou o Termo";
        }
    }
    
    public void Voltar()
    {
        SceneManager.LoadScene("Tela_Menu");
    }
    
}

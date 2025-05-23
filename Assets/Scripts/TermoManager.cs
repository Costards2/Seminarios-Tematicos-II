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
    public string cena;

    public void Continuar()
    {
        bool aceito = this.aceito.isOn;
        
        if (aceito)
        {
            AppManager.instance.CenaParaLoad(cena);
        }
        else
        {
            Debug.Log("Não Aceitou os Termo");
            erro.text = "Não Aceitou o Termo";
        }
    }
    
}

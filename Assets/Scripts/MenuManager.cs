using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void CenaParaLoad(string cena)
    {
        Debug.Log("Loading scene: " + cena); 
        
        SceneManager.LoadScene(cena);
    }

    public void Sair()
    {
        Application.Quit();
    }
}

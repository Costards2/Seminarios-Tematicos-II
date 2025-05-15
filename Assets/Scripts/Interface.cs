using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Interface : MonoBehaviour
{
    public Text timeText;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        timeText.text = DateTime.Now.ToString("HH:mm");
    }

    public void LoadScenes(string cena) {
        SceneManager.LoadScene(cena);
    }

}

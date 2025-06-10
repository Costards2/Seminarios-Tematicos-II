using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FilterManager : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string nextSceneName;

    [Header("Age Things")]
    [SerializeField] TMP_Text textMin;
    [SerializeField] TMP_Text textMax;

    [SerializeField] Slider sliderMin;
    [SerializeField] Slider sliderMax;

    public int idadeMin;
    public int idadeMax;


    [Header("Gender Things")]
    [SerializeField] TMP_Dropdown GenderOption;

    public bool isWoman;
    public bool isMan;


    /*[Header("Filtros")]
    [Tooltip("GameObject que carregará as informações de filtro para a cena seguinte")]
    [SerializeField] GameObject filters;*/

    private void Awake()
    {
        Debug.Log("FilterManager instanciado");
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        GenderOption.onValueChanged.AddListener(indexDropdown);
        isWoman = false;
        isMan = false;
        idadeMin = 13;
        idadeMax = 15;

    }

    public void indexDropdown(int index) 
    {
        switch (index) 
        {
            case 0:
                isWoman = false;
                isMan = false;
                break;
            case 1:
                isWoman = true;
                isMan = false;
                break;
            case 2:
                isWoman = false;
                isMan = true;
                break;
            case 3:
                isWoman = true;
                isMan = true;
                break;
            default:
                isWoman = false;
                isMan = false;
                break;

        }
    }

    public void ChangeMinValue() 
    {
        textMin.text = sliderMin.value.ToString();
        idadeMin = Mathf.RoundToInt(sliderMin.value);
    }

    public void ChangeMaxValue()
    {
        textMax.text = sliderMax.value.ToString();
        idadeMax = Mathf.RoundToInt(sliderMax.value);
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Next()
    {
        if (idadeMin <= idadeMax && (isWoman || isMan) == true) 
        {
            SceneManager.LoadScene(nextSceneName);
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

    public void toggleAge(bool age1) 
    {
        if (age1)
        {
            idadeMin = 13;
            idadeMax = 15;
        }
        else 
        {
            idadeMin = 16;
            idadeMax = 18;
        }
    }
}

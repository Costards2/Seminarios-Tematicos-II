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

    // Start is called before the first frame update
    void Start()
    {
        GenderOption.onValueChanged.AddListener(indexDropdown);
        isWoman = false;
        isMan = false;
        idadeMin = 0;
        idadeMax = 99;
    }

    public void indexDropdown(int index) 
    {
        if (index == 1) 
        {
            isWoman = true;
        }
        else if (index == 2) 
        {
            isMan = true;
        }
        else
        {
            isWoman = true;
            isMan = true;
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
        }
        
    }
}

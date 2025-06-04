using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [Header("Quest Thing")]
    [SerializeField] protected RectTransform locationQuest;
    [SerializeField] protected GameObject prefabTextQuest;
    protected GameObject actualQuest;
    protected TextMeshProUGUI actualQuestText;

    [Header("Alternactive Thing")]
    [SerializeField] protected GameObject prefabTextAlternactives;
    protected RectTransform locationAlternactives;
    private List<GameObject> optionObjects = new List<GameObject>();
    int countColor;
    public List<Color> ColorList = new List<Color>();




    void Start()
    {
        actualQuest = Instantiate(prefabTextQuest, locationQuest.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        actualQuestText = actualQuest.GetComponent<TextMeshProUGUI>();
    }

    public void ExibeQuest(Quest quest) 
    {
        locationAlternactives = actualQuest.transform.GetChild(0).GetComponent<RectTransform>();

        actualQuestText.text = quest.questionText;
        
        Transform alternativesLocation = GameObject.Find(actualQuest.gameObject.name).transform.GetChild(0).transform;
        
        // Limpa opções antigas
        foreach (var obj in optionObjects)
            Destroy(obj);
            countColor = 0;
        optionObjects.Clear();

        // Instancia novas
        foreach (string option in quest.options)
        {
            GameObject newOption = Instantiate(prefabTextAlternactives, alternativesLocation.position, Quaternion.identity, alternativesLocation);
            newOption.GetComponent<TextMeshProUGUI>().text = option;
            if (countColor < ColorList.Count) 
            {
                newOption.GetComponentInChildren<Image>().color = ColorList[countColor];
            }
            else 
            {
                newOption.GetComponentInChildren<Image>().color = new Color(Random.value, Random.value, Random.value);
            }
            optionObjects.Add(newOption);
            countColor++;

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(locationAlternactives);
        
    }
}

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

    [Header("coisas")]
    protected int totalAnswers;
    protected Quest quest;
    [SerializeField] private RectTransform pieChartContainer;
    [SerializeField] private GameObject pieSlicePrefab;

    void Start()
    {
        actualQuest = Instantiate(prefabTextQuest, locationQuest.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        actualQuestText = actualQuest.GetComponent<TextMeshProUGUI>();
    }

    public void ExibeQuest(Quest _quest) 
    {
        this.quest = _quest;

        locationAlternactives = actualQuest.transform.GetChild(0).GetComponent<RectTransform>();

        actualQuestText.text = _quest.questionText;
        
        Transform alternativesLocation = GameObject.Find(actualQuest.gameObject.name).transform.GetChild(0).transform;
        
        // Limpa opções antigas
        foreach (var obj in optionObjects)
            Destroy(obj);
            countColor = 0;
        optionObjects.Clear();

        // Instancia novas
        foreach (string option in _quest.options)
        {
            GameObject newOption = Instantiate(prefabTextAlternactives, alternativesLocation.position, Quaternion.identity, alternativesLocation);
            newOption.GetComponent<TextMeshProUGUI>().text = option;
            if (countColor < ColorList.Count) 
            {
                newOption.GetComponentInChildren<Image>().color = ColorList[countColor];
            }
            else 
            {
                Color newColor = new Color(Random.value, Random.value, Random.value);
                ColorList.Add(newColor);
                newOption.GetComponentInChildren<Image>().color = newColor;
            }
            optionObjects.Add(newOption);
            countColor++;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(locationAlternactives);
    }

    // Aqui já tenho as respostas convertidas para a matriz, só falta exibi-la
    public void ExibeAwnser(IdexableAnswers answerFullFiltered)
    {
        totalAnswers = answerFullFiltered.sendableAnswers.Count;

        int[] numberResponses = new int[quest.options.Count];

        float[] percentResponses = new float[quest.options.Count];

        int totalValue = 0;

        foreach (Answer response in answerFullFiltered.sendableAnswers)
        {
            for (int i = 0; i < numberResponses.Length; i++)
            {
                if (response.response == quest.options[i])
                {
                    numberResponses[i] += 1;
                    totalValue += 1;
                }
            }
        }

        for (int i = 0; i < numberResponses.Length; i++)
        {
            if (totalValue != 0) 
            {
                percentResponses[i] = (float)numberResponses[i] / totalValue;
            }
            else 
            {
                percentResponses[i] = 0;
            }

            Debug.Log(percentResponses[i]);

            if (i < optionObjects.Count)
            {
                Transform option = optionObjects[i].transform;

                // Procura o filho que vai exibir o texto de porcentagem.
                TextMeshProUGUI percentText = option.Find("PercentText").GetComponent<TextMeshProUGUI>();

                percentText.text = Mathf.RoundToInt(percentResponses[i] * 100f) + "%";
            }
        }

        DrawPieChart(numberResponses, ColorList);
    }

    private void DrawPieChart(int[] values, List<Color> colors)
    {
        // Limpar fatias antigas
        foreach (Transform child in pieChartContainer)
            Destroy(child.gameObject);

        int total = 0;
        foreach (int v in values)
            total += v;

        if (total == 0) return;

        float zRotation = 0f;

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == 0) continue;

            float fillAmount = (float)values[i] / total;

            GameObject slice = Instantiate(pieSlicePrefab, pieChartContainer);
            Image img = slice.GetComponent<Image>();

            img.fillAmount = fillAmount;
            img.color = (i < colors.Count) ? colors[i] : new Color(Random.value, Random.value, Random.value);
            slice.transform.localRotation = Quaternion.Euler(0f, 0f, -zRotation * 360f);

            zRotation += fillAmount;
        }
    }
}

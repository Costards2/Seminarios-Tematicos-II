using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestionarioLogger : MonoBehaviour
{
    [Header("Campos do formulário")]
    public InputField nomeInput;
    public TMP_Dropdown generoDropdown;

    [System.Serializable]
    public class PerguntaUnica
    {
        public string titulo;
        public Toggle[] opcoes;
    }

    [System.Serializable]
    public class PerguntaMultipla
    {
        public string titulo;
        public Toggle[] opcoes;
    }

    [Header("Perguntas de escolha única")]
    public List<PerguntaUnica> perguntasUnicas;

    [Header("Pergunta de múltipla escolha")]
    public PerguntaMultipla perguntaMultiplaFinal;

    public Button botaoEnviar;

    void Start()
    {
        botaoEnviar.onClick.AddListener(SalvarRespostas);
    }

    void SalvarRespostas()
    {
        string path = Application.persistentDataPath + "/respostas_questionario.txt";
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("----- NOVO QUESTIONÁRIO -----");
        writer.WriteLine("Nome: " + nomeInput.text);
        writer.WriteLine("Identidade de Gênero: " + generoDropdown.options[generoDropdown.value].text);
        writer.WriteLine();

        foreach (var pergunta in perguntasUnicas)
        {
            writer.WriteLine(pergunta.titulo);
            foreach (var toggle in pergunta.opcoes)
            {
                if (toggle.isOn)
                {
                    writer.WriteLine("✓ " + toggle.GetComponentInChildren<Text>().text);
                    break; // só uma resposta esperada
                }
            }
            writer.WriteLine();
        }

        writer.WriteLine(perguntaMultiplaFinal.titulo);
        foreach (var toggle in perguntaMultiplaFinal.opcoes)
        {
            if (toggle.isOn)
            {
                writer.WriteLine("✓ " + toggle.GetComponentInChildren<Text>().text);
            }
        }

        writer.WriteLine("----- FIM -----\n");
        writer.Close();

        Debug.Log("Respostas salvas em: " + path);
    }
}

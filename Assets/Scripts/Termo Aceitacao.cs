using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using static TermoAceitacao;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class TermoAceitacao : MonoBehaviour
{
    public static string cpfAdultoStatic;
    public static string cpfMenorStatic;
    public string idade;
    public string sexo;


    public TMP_InputField inputCpfAdulto;
    public TMP_InputField inputCpfMenor;
    public TMP_InputField inputNome;

    public Toggle idade13a15;
    public Toggle idade16a18;
    public Toggle sexoM;
    public Toggle sexoF;
    public Toggle sexoOutros;


    public GameObject mensagemCpfInvalido;

    private string caminhoArquivo;  //caminho do arquivo json do termo

    public int iDTelaInicial;

    public void Start()
    {
        caminhoArquivo = Path.Combine(Application.persistentDataPath, "Lista Termo de Aceitacao.json");
        mensagemCpfInvalido.SetActive(false);
    }

    [System.Serializable]
    public class DetalhesUsuariosTermo  //informações do adulto e do menor que serão salvas no json do termo 
    {
        public string cpfAdulto;
        public string cpfMenor;
        public string nome;
        public string idade;
        public string sexo;
        public string data;
        public string hora;

    }

    [System.Serializable]
    public class FormDataCollection
    {
        public List<DetalhesUsuariosTermo> usuarios = new List<DetalhesUsuariosTermo>();
    }



    public void RegistroTermo()
    {

        if (inputCpfMenor.text.Length == 11 && inputCpfAdulto.text.Length == 11)
        {
            if (idade13a15.isOn)
            {
                idade = "13 a 15";
            }else if (idade16a18.isOn)
            {
                idade = "16 a 18";
            }

            if (sexoM.isOn)
            {
                sexo = "M";
            }else if (sexoF.isOn)
            {
                sexo = "F";
            }else if (sexoOutros.isOn)
            {
                sexo = "Outros";
            }

            cpfMenorStatic = inputCpfMenor.text;
            cpfAdultoStatic = inputCpfAdulto.text;
            DetalhesUsuariosTermo novoDado = new DetalhesUsuariosTermo();
            novoDado.nome = inputNome.text;
            novoDado.idade = idade;
            novoDado.sexo = sexo;
            novoDado.cpfAdulto = cpfAdultoStatic;
            novoDado.cpfMenor = cpfMenorStatic;
            DateTime dateNow = DateTime.Now;
            novoDado.hora = dateNow.ToString("HH:mm:ss");
            novoDado.data = dateNow.ToString("dd/MM/yyyy");


            FormDataCollection colecao;

           // Se o arquivo já existe, carregue os dados já salvos
            if (File.Exists(caminhoArquivo))
            {
                string jsonAntigo = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrEmpty(jsonAntigo))
                {
                   colecao = JsonUtility.FromJson<FormDataCollection>(jsonAntigo);
                   // Caso a desserialização falhe, inicialize uma nova coleção
                   if (colecao == null)
                        colecao = new FormDataCollection();
               }
                else
                {
                    colecao = new FormDataCollection();
                }
            }
            else
            {
                colecao = new FormDataCollection();
            }

            // Adiciona o novo registro à coleção
            colecao.usuarios.Add(novoDado);
        
            // Converte a coleção para uma string JSON formatada (o 'true' para formatação legível)
            string novoJson = JsonUtility.ToJson(colecao, true);

            // Salva o JSON no arquivo
            File.WriteAllText(caminhoArquivo, novoJson);

            ProximaTela();
        }
        else
        {
            StartCoroutine(MensagemCpfInvalido());
        }


    }

    IEnumerator MensagemCpfInvalido()
    {
        mensagemCpfInvalido.SetActive(true);
        yield return new WaitForSeconds(3f);
        mensagemCpfInvalido.SetActive(false);
    }

    
    public void ProximaTela()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CancelarTermo()
    {
        SceneManager.LoadScene(iDTelaInicial);
    }


}

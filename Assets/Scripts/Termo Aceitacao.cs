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
    public class DetalhesUsuariosTermo  //informa��es do adulto e do menor que ser�o salvas no json do termo 
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
                sexo = "Masculino";
            }else if (sexoF.isOn)
            {
                sexo = "Feminino";
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

           // Se o arquivo j� existe, carregue os dados j� salvos
            if (File.Exists(caminhoArquivo))
            {
                string jsonAntigo = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrEmpty(jsonAntigo))
                {
                   colecao = JsonUtility.FromJson<FormDataCollection>(jsonAntigo);
                   // Caso a desserializa��o falhe, inicialize uma nova cole��o
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

            // Adiciona o novo registro � cole��o
            colecao.usuarios.Add(novoDado);
        
            // Converte a cole��o para uma string JSON formatada (o 'true' para formata��o leg�vel)
            string novoJson = JsonUtility.ToJson(colecao, true);

            // Salva o JSON no arquivo
            File.WriteAllText(caminhoArquivo, novoJson);

            // Create a new UserResponse with the cpfMenor as userID
            UserResponse newResponse = new UserResponse(cpfMenorStatic);

            // Add answers with the respective question IDs and values
            newResponse.answers.Add(new Answer(0, cpfAdultoStatic));  // cpfAdulto to question ID 0
            newResponse.answers.Add(new Answer(1, cpfMenorStatic));   // cpfMenor to question ID 1
            newResponse.answers.Add(new Answer(2, inputNome.text));   // nome to question ID 2
            newResponse.answers.Add(new Answer(3, idade));            // idade to question ID 3
            newResponse.answers.Add(new Answer(4, sexo));             // sexo to question ID 4
            newResponse.answers.Add(new Answer(5, "Parda"));

            // Save using the UserResponseManager static method
            UserResponseManager.SaveUserResponse(newResponse);

            Debug.Log("User responses saved to userResponses.json");

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

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tela_cadastro : MonoBehaviour
{
    public Text nomeCompleto;
    public Text email;
    public Text senha;
    public Text repetirSenha;
    public GameObject byebye;
    public Text ncadas;

    public static List<string> ListaNomes = new List<string>();
    public static List<string> ListaEmails = new List<string>();
    public static List<string> ListaSenhas = new List<string>();

    public void Cadastro(string cena)
    {
        try
        {
            string nome = nomeCompleto.text.Trim();
            string mail = email.text.Trim();
            string pass = senha.text.Trim();
            string passConfirm = repetirSenha.text.Trim();

            // Verifica se os campos estão vazios
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(passConfirm))
            {
                ExibirErro("Preencha todos os campos corretamente.");
                return;
            }

            // Verifica se o e-mail é válido
            if (!mail.Contains("@") || !mail.Contains("."))
            {
                ExibirErro("E-mail inválido! Certifique-se de incluir '@' e um domínio válido.");
                return;
            }

            // Verifica se a senha tem pelo menos 6 caracteres
            if (pass.Length < 6)
            {
                ExibirErro("A senha deve conter pelo menos 6 caracteres.");
                return;
            }

            // Verifica se as senhas coincidem
            if (!pass.Equals(passConfirm))
            {
                ExibirErro("As senhas não coincidem. Tente novamente.");
                return;
            }

            // Verifica se o e-mail já está cadastrado
            if (ListaEmails.Contains(mail))
            {
                ExibirErro("E-mail já cadastrado. Tente outro.");
                return;
            }

            // Salva os dados do usuário
            ListaNomes.Add(nome);
            ListaEmails.Add(mail);
            ListaSenhas.Add(pass);

            Debug.Log("Cadastro efetuado com sucesso!");
            SceneManager.LoadScene(cena);
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro no Cadastro: " + ex.Message);
        }
    }

    private void ExibirErro(string mensagem)
    {
        byebye.SetActive(true);
        ncadas.text = mensagem;
        Debug.Log(mensagem);
    }
}

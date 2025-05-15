using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tela_login : MonoBehaviour
{
    public Text entradaemail;
    public Text entradasenha;
    public GameObject byebye;
    public Text nautenticou;

    private string email;
    private string senha;
    private int Idu;

    void Start()
    {
        byebye.SetActive(false);
    }

    public void Autentica(string cena)
    {
        try
        {
            email = entradaemail.text.Trim();
            senha = entradasenha.text.Trim();

            // Verifica se os campos estão vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ExibirErro("Preencha corretamente ambos os campos para fazer login.");
                return;
            }

            // Verifica se o e-mail existe
            if (!Tela_cadastro.ListaEmails.Contains(email))
            {
                ExibirErro("E-mail não encontrado. Verifique se digitou corretamente.");
                return;
            }

            // Obtém o índice do e-mail para buscar a senha correta
            Idu = Tela_cadastro.ListaEmails.IndexOf(email);

            // Autentica a senha
            if (senha.Equals(Tela_cadastro.ListaSenhas[Idu]))
            {
                string login = "Login feito: " + DateTime.Now + "\n";
                Debug.Log("Você está autenticado.");
                SceneManager.LoadScene(cena);
            }
            else
            {
                ExibirErro("Senha incorreta. Tente novamente.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro na autenticação: " + ex.Message);
        }
    }

    private void ExibirErro(string mensagem)
    {
        byebye.SetActive(true);
        nautenticou.text = mensagem;
        Debug.Log(mensagem);
    }
}

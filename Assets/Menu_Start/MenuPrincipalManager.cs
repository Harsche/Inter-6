using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{

    [SerializeField] private string levelDoJogo;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelMenuInicial;

    public void Jogar()
    {
        SceneManager.LoadScene(levelDoJogo);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void SairJogo()

    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
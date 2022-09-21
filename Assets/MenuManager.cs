using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour


{
    [SerializeField] private string LeveldoJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;

    public void Start()
    {
        painelOpcoes.SetActive(false);

    }

    public void Jogar()
    {
        
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


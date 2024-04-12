using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
///     Gerencia todos os registros do jogo, como a quantidade de dinheiro, clicadores e multiplicadores
/// </summary>
public class GameManager : MonoBehaviour
{
    // Dinheiro
    public static int dinheiro = 0;
    static TextMeshProUGUI textoDinheiro;

    // Clicadores
    public static int clicadores = 0;
    public static int custoClicador = 25;

    // Multiplicadores
    public static int multiplicadores = 1;
    public static int custoMultiplicador = 90;

    // Está sendo usado Awake invés do Start porque a função AddDinheiro é estática e depende que o
    // texto do dinheiro exista para funcionar. Então deve ser iniciado com certeza antes de todos
    private void Awake()
    {
        CarregarJogo();
        if (ExisteSave() == true) 
        {
            GameManager.custoClicador = (int)Math.Floor(GameManager.custoClicador * 1.25f);
            GameManager.custoMultiplicador = (int)Math.Floor(GameManager.custoMultiplicador * 1.25f); 
        }
        // Pega o texto de dinheiro que está na tela
        textoDinheiro = GameObject.Find("Canvas").transform.Find("Dinheiro").GetComponent<TextMeshProUGUI>();
        AutoSave();
    }

    // A variável de dinheiro poderia ser alterada diretamente, porém ao usar esta função,
    // o texto do Canvas já é atualizado ao mesmo tempo
    public static void AddDinheiro(int valor)
    {
        GameManager.dinheiro += valor;
        textoDinheiro.text = "$ " + GameManager.dinheiro.ToString();
    }

    //----------------------------------------------------------
    public static void SalvarJogo() 
    {
        PlayerPrefs.SetInt("dinheiro",dinheiro);
        PlayerPrefs.SetInt("multiplicador", multiplicadores);
        PlayerPrefs.SetInt("clicadores", clicadores);
        PlayerPrefs.Save();
        Debug.Log("Jogo salvo com sucesso!!");
    }
    public void CarregarJogo() 
    {
        if (ExisteSave() == false) 
        {
            return;
        }
        GameManager.dinheiro = PlayerPrefs.GetInt("dinheiro");
        GameManager.multiplicadores = PlayerPrefs.GetInt("multiplicador");
        GameManager.clicadores = PlayerPrefs.GetInt("clicadores");
        Debug.Log("Jogo carregado com sucesso!!");

    }
    bool ExisteSave() 
    {
        if (PlayerPrefs.HasKey("dinheiro") == true) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AutoSave() 
    {
        SalvarJogo();
        Invoke("AutoSave", 5);
    }

}

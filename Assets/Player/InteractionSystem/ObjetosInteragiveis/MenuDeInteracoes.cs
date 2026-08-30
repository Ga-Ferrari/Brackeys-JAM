using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuDeInteracoes : ObjetoInteragivel, INavegarMenu
{
    [Header("UI do Menu Dinâmico")]
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private Transform containerOpcoes; // Ex: Um objeto com componente VerticalLayoutGroup
    [SerializeField] private GameObject prefabOpcaoUI; // Um prefab que tenha Image (fundo) e Text filho
    private CanvasGroup canvasGroup;
    [SerializeField] private float tempoAparicao = 1f;

    [SerializeField] private float tempoDesaparicao = 0.5f;

    [Header("Cores da Seleção")]
    [SerializeField] private Color corSelecionada = Color.yellow;
    [SerializeField] private Color corNormal = Color.white;

    private List<ObjetoInteragivel> interacoesDisponiveis = new List<ObjetoInteragivel>();
    private List<Image> fundosUI = new List<Image>();
    private int indiceSelecionado = 0;
    private bool menuAberto = false;
    private GameObject playerAtual; // Salva o player para repassar para a interação final



    protected override void Start()
    {
        base.Start();
        if (painelMenu != null) painelMenu.SetActive(true);
        canvasGroup = painelMenu.GetComponent<CanvasGroup>();
        CarregarInteracoes();
    }

    void Update()
    {
        if (canvasGroup)
        {
            if (menuAberto)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, Time.deltaTime / tempoAparicao);
            }
            else if (!menuAberto)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, Time.deltaTime / tempoDesaparicao); ;
            }
        }
    }


    public override void AtivarContorno(bool ativar)
    {
        base.AtivarContorno(ativar);
        if (!ativar) FecharMenu();
    }

    private void CarregarInteracoes()
    {
        // Pega TODOS os scripts do tipo ObjetoInteragivel neste GameObject
        ObjetoInteragivel[] todasInteracoes = GetComponents<ObjetoInteragivel>();

        foreach (var interacao in todasInteracoes)
        {
            // Adiciona na lista, desde que não seja este próprio menu
            if (interacao != this)
            {
                interacoesDisponiveis.Add(interacao);
            }
        }

        CriarUIDinamica();
    }

    private void CriarUIDinamica()
    {
        // Limpa tudo que estiver dentro do container primeiro
        foreach (Transform child in containerOpcoes)
        {
            Destroy(child.gameObject);
        }
        fundosUI.Clear();

        // Instancia um prefab para cada interação que encontramos
        foreach (var interacao in interacoesDisponiveis)
        {
            GameObject novaOpcao = Instantiate(prefabOpcaoUI, containerOpcoes);

            TMP_Text textoDaOpcao = novaOpcao.GetComponentInChildren<TMP_Text>();

            // 1. Busca o objeto filho primeiro com segurança
            Transform filhoTextoCusto = novaOpcao.transform.Find("Horizontal/Custo/TextoCusto");
            TMP_Text textoCusto = null;

            // Só tenta pegar o componente se o objeto existir
            if (filhoTextoCusto != null)
            {
                textoCusto = filhoTextoCusto.GetComponent<TMP_Text>();
            }

            if (textoDaOpcao != null)
            {
                textoDaOpcao.text = interacao.NomeDaInteracao;
            }

            if (textoCusto != null)
            {
                // 2. Usa o 'as' para fazer uma conversão segura
                AcaoComCusto acao = interacao as AcaoComCusto;

                if (acao == null)
                {
                    textoCusto.text = "0";
                }
                else
                {
                    Debug.Log(acao.Custo);
                    textoCusto.text = acao.Custo.ToString();
                }
            }

            Image fundo = novaOpcao.GetComponent<Image>();
            if (fundo != null)
            {
                fundosUI.Add(fundo);
            }
        }
    }

    public override bool Interagir(GameObject gameObjectOrigem)
    {
        if (interacoesDisponiveis.Count == 0) return false; // Se não tem interação, nem abre o menu
        playerAtual = gameObjectOrigem; // Salva quem está interagindo
        base.Interagir(gameObjectOrigem); // Executa a lógica de contorno/texto do pai

        if (!menuAberto)
        {
            AbrirMenu();
        }
        else
        {
            ConfirmarSelecao();
        }

        return true;
    }

    private void AbrirMenu()
    {
        menuAberto = true;
        indiceSelecionado = 0;
        AtualizarVisualSelecao();
    }

    private void FecharMenu()
    {
        menuAberto = false;
    }

    public void OnNavegarMenu(InputAction.CallbackContext context)
    {
        if (!menuAberto || !context.performed || fundosUI.Count == 0) return;

        Vector2 inputNavegacao = context.ReadValue<Vector2>();

        if (inputNavegacao.y > 0.5f) // Cima
        {
            indiceSelecionado--;
            if (indiceSelecionado < 0) indiceSelecionado = fundosUI.Count - 1;
            AtualizarVisualSelecao();
        }
        else if (inputNavegacao.y < -0.5f) // Baixo
        {
            indiceSelecionado++;
            if (indiceSelecionado >= fundosUI.Count) indiceSelecionado = 0;
            AtualizarVisualSelecao();
        }
    }

    private void AtualizarVisualSelecao()
    {
        for (int i = 0; i < fundosUI.Count; i++)
        {
            fundosUI[i].color = (i == indiceSelecionado) ? corSelecionada : corNormal;
        }
    }

    private void ConfirmarSelecao()
    {
        // Repassa a chamada para o script correto da lista!
        interacoesDisponiveis[indiceSelecionado].Interagir(playerAtual);

        FecharMenu();
    }
}
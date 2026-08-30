using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    //gerenciador de ap
    private bool isDay;
    private int actionPoints;
    private const int MAX_AP = 3;

    //gerenciador de turnos
    public event Action daylightTurn; //evento que fala que o dia começou
    public event Action nightfallTurn; //evento que fala que a noite começou

    void Start()
    {
        EventBus.OnDormirCama += IniciarNoite;
        EventBus.OnInteragir += OnInteracoes;
        EventBus.iniciarNoite += IniciarNoite;
    }
    public void systemConector(DeathSystem death) //função que "conecta" os eventos
    {
        death.OnFimRotinaMorte += IniciarDia;
    }

    public void IniciarDia()
    {
        isDay = true;
        actionPoints = MAX_AP;
        daylightTurn?.Invoke();
        EventBus.IniciarManha();
    }

    public void IniciarNoite()
    {
        isDay = false;
        Debug.Log("Iniciando noite");
        nightfallTurn?.Invoke();
    }

    public void OnInteracoes(tiposDeAcao tipoAcao, ObjetoInteragivel objeto, int custo = 0)
    {
        Debug.Log("Entrou on interacao");
        if (tipoAcao == tiposDeAcao.InteracaoNPC)
        {
            trySpendAP(custo);
        }
    }

    public bool trySpendAP(int custo)
    {
        if (!isDay)
        {
            Debug.Log("Tentativa de gastar AP durante a noite bloqueada");
            return false; //evitar bugs de gastar de noite
        }

        if (actionPoints >= custo)
        {
            Debug.Log("Debitado AP");
            actionPoints -= custo;
            return true;
        }

        Debug.Log("AP insuficiente");
        return false;
    }
}
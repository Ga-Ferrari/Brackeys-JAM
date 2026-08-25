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
    public void systemConector(InteractionSystem interaction, DeathSystem death) //função que "conecta" os eventos
    {
        interaction.OnDormirCama += IniciarNoite;
        death.OnFimRotinaMorte += IniciarDia;
    }
    
    public void IniciarDia()
    {
        isDay = true;
        actionPoints = MAX_AP;
        daylightTurn?.Invoke();
    }

    public void IniciarNoite()
    {
        isDay = false;
        nightfallTurn?.Invoke();
    }

    public bool trySpendAP(int custo)
    {
        if(!isDay) {
            Debug.Log("Tentativa de gastar AP durante a noite bloqueada");
            return false; //evitar bugs de gastar de noite
        }

        if(actionPoints >= custo)
        {
            actionPoints -= custo;
            return true;
        }
        
        Debug.Log("AP insuficiente");
        return false;
    }
}
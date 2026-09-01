using System;
using System.Data;
using UnityEngine;

public static class EventBus
{

    public static event Action OnDormirCama;
    public static event Action ativarPainel;

    public static event Action<tiposDeAcao, ObjetoInteragivel, int> OnInteragir;

    public static event Action<NPCAtributos> atirar;
    public static event Action<NPCLogica> trocarSprite;

    public static event Action travarControles;
    public static event Action iniciarManha;
    public static event Action iniciarNoite;

    public static void AtivarPainel()
    {
        ativarPainel?.Invoke();
    }

    public static void TravarControles()
    {
        travarControles?.Invoke();
    }

    public static void IniciarManha()
    {
        iniciarManha?.Invoke();
        foreach (NPCLogica npc in GameManager.Instancia.npcsAMorrer)
        {
            trocarSprite?.Invoke(npc);
        }
        GameManager.Instancia.npcsAMorrer.Clear();
    }

    public static void DispararOnDormirCama()
    {
        OnDormirCama?.Invoke();
    }

    public static void IniciarNoite()
    {
        Debug.Log("Iniciou a noite");
        iniciarNoite?.Invoke();
    }

    public static void Atirar(NPCAtributos npcAMorrer)
    {
        NPCLogica impostor = npcAMorrer.GetComponent<NPCLogica>();
        if (GameManager.Instancia.impostor == impostor)
        {
            GameManager.Instancia.estado = 3;
        }
        atirar?.Invoke(npcAMorrer);
    }

    public static void TrocarSprite()
    {

    }

    public static void DispararAcaoFeita(tiposDeAcao tipoAcao, ObjetoInteragivel objeto, int custo = 0)
    {
        OnInteragir?.Invoke(tipoAcao, objeto, custo);
    }

    public static void LimparEventos()
    {
        OnDormirCama = null;
        ativarPainel = null;
        OnInteragir = null;
        atirar = null;
        trocarSprite = null;
        travarControles = null;
        iniciarManha = null;
        iniciarNoite = null;
    }
}
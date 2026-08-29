using System;
using System.Data;
using UnityEngine;

public static class EventBus
{

    public static event Action OnDormirCama;
    public static event Action<tiposDeAcao, ObjetoInteragivel, int> OnInteragir;

    public static event Action<NPCAtributos> atirar;
    public static event Action trocarSprite;

    public static void DispararOnDormirCama()
    {
        OnDormirCama?.Invoke();
    }

    public static void Atirar(NPCAtributos npcAMorrer)
    {
        atirar?.Invoke(npcAMorrer);
    }

    public static void DispararAcaoFeita(tiposDeAcao tipoAcao, ObjetoInteragivel objeto, int custo = 0)
    {
        OnInteragir?.Invoke(tipoAcao, objeto, custo);
    }

}
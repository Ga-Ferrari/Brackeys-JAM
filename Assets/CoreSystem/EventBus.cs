using System;
using UnityEngine;

public static class EventBus
{

    public static event Action OnDormirCama;
    public static event Action<tiposDeAcao, ObjetoInteragivel, int> OnInteragir;


    public static void DispararOnDormirCama()
    {
        OnDormirCama?.Invoke();
    }

    public static void DispararAcaoFeita(tiposDeAcao tipoAcao, ObjetoInteragivel objeto, int custo = 0)
    {
        Debug.Log("EventBus");
        OnInteragir?.Invoke(tipoAcao, objeto, custo);
    }

}
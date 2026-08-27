using System;
using UnityEngine;

public static class EventBus
{

    public static event Action OnDormirCama;
    public static event Action<tiposDeAcao, ObjetoInteragivel> OnInteragir;


    public static void DispararOnDormirCama()
    {
        OnDormirCama?.Invoke();
    }

    public static void DispararAcaoFeita(tiposDeAcao tipoAcao, ObjetoInteragivel objeto)
    {
        OnInteragir?.Invoke(tipoAcao, objeto);
    }

}
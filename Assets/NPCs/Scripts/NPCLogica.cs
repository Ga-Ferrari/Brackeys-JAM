using System;
using UnityEngine;
using UnityEngine.Events;

public class NPCLogica : MonoBehaviour
{
    public event Action<NPCLogica> faleceuEvent;

    public void Morrer()
    {
        faleceuEvent?.Invoke(this);
        GameManager.Instancia.npcList.Remove(this);
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class FalasNPC : ObjetoInteragivel
{
    private int npcID;

    [SerializeField] private DialogoEmWorldSpace dialogo;
    private bool interagido = false;

    [SerializeField] private List<string> falas = new List<string>();
    private int falaAtual = 0;

    public UnityEvent<int> npcInteragido;
    public event Action<tiposDeAcao> AcaoInteragida;


    protected override void setTipo()
    {
        tipoAcao = tiposDeAcao.InteracaoNPC;
    }

    public override bool Interagir(GameObject gameObject)
    {

        if (falas.Count > 0)
        {
            if (falaAtual < falas.Count) dialogo.SoltarDialogo(falas[falaAtual++]);
            if (falaAtual == falas.Count) falaAtual = 0;
        }

        npcInteragido?.Invoke(npcID);

        interagido = true;
        return true;
    }

    public bool setFalas(List<string> novasFalas)
    {
        falas = novasFalas;
        return true;
    }



}

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class FalasNPC : ObjetoInteragivel
{
    private int npcID;

    [SerializeField] private DialogoEmWorldSpace dialogo;
    [SerializeField] private int custo = 1;
    private bool interagido = false;

    [SerializeField] private List<string> falas = new List<string>();
    private int falaAtual = 0;

    public event Action<tiposDeAcao> AcaoInteragida;

    protected override void DispararAcao()
    {
        if (!interagido)
            EventBus.DispararAcaoFeita(tipoAcao, this, custo);
    }

    protected override void setTipo()
    {
        tipoAcao = tiposDeAcao.InteracaoNPC;
    }

    public override bool Interagir(GameObject gameObject)
    {
        base.Interagir(gameObject);
        if (falas.Count > 0)
        {
            if (falaAtual < falas.Count) dialogo.SoltarDialogo(falas[falaAtual++]);
            if (falaAtual == falas.Count) falaAtual = 0;
        }

        interagido = true;
        return true;
    }

    public bool setFalas(List<string> novasFalas)
    {
        falas = novasFalas;
        return true;
    }

    public override void AtivarContorno(bool ativar)
    {
        base.AtivarContorno(ativar);
        if (!ativar)
        {
            dialogo.DesativarDialogo();
        }
    }



}
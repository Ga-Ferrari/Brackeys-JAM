
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class FalasNPC : AcaoComCusto
{
    private int npcID;

    [SerializeField] private DialogoEmWorldSpace dialogo;

    [SerializeField] private List<string> falas = new List<string>();
    private int falaAtual = 0;

    void Awake()
    {
        custo = 1;
    }

    protected override void setTipo()
    {
        tipoAcao = tiposDeAcao.InteracaoNPC;
    }

    public override bool Interagir(GameObject gameObject)
    {
        base.Interagir(gameObject);

        DetectorDeInteracoes.interacaoBloqueada = true;
        DetectorDeInteracoes.alvoTravado = this;
        if (falas.Count > 0)
        {
            if (GameManager.Instancia.posPrimeiraFala == GameManager.Instancia.primeiraFala.Count) GameManager.Instancia.primeiraInteracao = false;
            if (GameManager.Instancia.primeiraInteracao)
            {
                dialogo.SoltarDialogo(GameManager.Instancia.primeiraFala[GameManager.Instancia.posPrimeiraFala++]);
                GameManager.Instancia.primeiraInteracao = false;
            }
            else
            {
                if (falaAtual == falas.Count)
                {
                    dialogo.DesativarDialogo();
                    falaAtual = 0;
                    DetectorDeInteracoes.interacaoBloqueada = false;
                }
                else if (falaAtual < falas.Count) dialogo.SoltarDialogo(falas[falaAtual++]);
            }

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
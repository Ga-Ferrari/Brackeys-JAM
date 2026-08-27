using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Outline2D))]
public abstract class ObjetoInteragivel : MonoBehaviour, IInteractable
{

    protected tiposDeAcao tipoAcao = tiposDeAcao.Indefinida;

    [SerializeField] private string nomeInteracao;
    private Outline2D outliner;
    [SerializeField] private AnimarTextoDialogo textoAcao;

    protected virtual void Start()
    {
        gameObject.GetComponent<Outline2D>();
        gameObject.layer = LayerMask.NameToLayer("Interagivel");
        AtivarContorno(false);
        EventBus.DispararAcaoFeita(tipoAcao, this);
        setTipo();
    }

    protected virtual void setTipo()
    {

    }

    public abstract bool Interagir(GameObject gameObject);
    public void AtivarContorno(bool ativar)
    {
        if (outliner != null)
        {
            outliner.AtivarContorno(ativar);
        }
        if (textoAcao != null)
        {
            if (ativar) textoAcao.IniciarAnimacao(nomeInteracao);
            else textoAcao.DesativarAnimacao();
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Outline2D))]
public abstract class ObjetoInteragivel : MonoBehaviour, IInteractable
{
    protected tiposDeAcao tipoAcao = tiposDeAcao.Indefinida;

    [SerializeField] protected string nomeInteracao;
    private Outline2D outliner;
    [SerializeField] private AnimarTexto textoAcao;

    private bool interagidoEEmRange = false;
    protected bool interagido = false;

    public bool Interagido => interagido;

    // Adicione isso no ObjetoInteragivel.cs
    public string NomeDaInteracao => nomeInteracao;

    protected virtual void Start()
    {
        gameObject.GetComponent<Outline2D>();
        gameObject.layer = LayerMask.NameToLayer("Interagivel");
        AtivarContorno(false);
        EventBus.iniciarManha += Desinteragir;
        setTipo();
    }

    private void Desinteragir()
    {
        interagido = false;
    }

    protected virtual void DispararAcao()
    {
        EventBus.DispararAcaoFeita(tipoAcao, this);
    }

    protected virtual void setTipo()
    {

    }

    public virtual bool Interagir(GameObject gameObject)
    {
        if (textoAcao != null) textoAcao.DesativarAnimacao();
        DispararAcao();
        interagido = true;
        interagidoEEmRange = true;
        return true;
    }

    public virtual void AtivarContorno(bool ativar)
    {
        if (outliner != null)
        {
            outliner.AtivarContorno(ativar);
        }
        if (textoAcao != null)
        {
            if (ativar && !interagidoEEmRange) textoAcao.IniciarAnimacao(nomeInteracao);
            else
            {
                interagidoEEmRange = false;
                textoAcao.DesativarAnimacao();
            }
        }
    }
}
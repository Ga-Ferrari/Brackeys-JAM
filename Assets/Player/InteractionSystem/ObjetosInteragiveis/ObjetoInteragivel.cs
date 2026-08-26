using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Outline2D))]
public abstract class ObjetoInteragivel : MonoBehaviour, IInteractable
{
    private Outline2D outliner;
    [SerializeField] private CanvasGroup textoAcao;

    protected virtual void Start()
    {
        gameObject.GetComponent<Outline2D>();
        gameObject.layer = LayerMask.NameToLayer("Interagivel");
        AtivarContorno(false);
        textoAcao.alpha = 0;

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
            textoAcao.alpha = ativar ? 1 : 0;
        }
    }
}
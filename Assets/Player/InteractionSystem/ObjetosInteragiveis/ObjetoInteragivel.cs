using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Outline2D))]
public abstract class ObjetoInteragivel : MonoBehaviour, IInteractable
{
    private Outline2D outliner;
    protected virtual void Start()
    {
        gameObject.GetComponent<Outline2D>();
        gameObject.layer = LayerMask.NameToLayer("Interagivel");
        AtivarContorno(false);
    }

    public abstract bool Interagir(GameObject gameObject);
    public void AtivarContorno(bool ativar)
    {
        if (outliner != null)
        {
            outliner.AtivarContorno(ativar);
        }
    }
}
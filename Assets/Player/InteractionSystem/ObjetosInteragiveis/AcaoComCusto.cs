
using System;
using UnityEngine;
public abstract class AcaoComCusto : ObjetoInteragivel
{

    [SerializeField] protected int custo;
    public int Custo => custo;

    public event Action<tiposDeAcao> AcaoInteragida;

    protected override void DispararAcao()
    {
        if (!interagido)
            EventBus.DispararAcaoFeita(tipoAcao, this, custo);
    }

}
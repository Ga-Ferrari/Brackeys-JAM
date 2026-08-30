


using System;
using System.Collections;
using UnityEngine;

public class NPCMatar : AcaoComCusto
{

    void Awake()
    {
        custo = 2;
    }



    protected override void Start()
    {
        base.Start();
        custo = 2;
        EventBus.trocarSprite += trocarSprite;
    }

    public override bool Interagir(GameObject gameObject)
    {
        base.Interagir(gameObject);
        GetComponent<NPCLogica>().Morrer();
        EventBus.Atirar(GetComponent<NPCAtributos>());

        return true;
    }

    private void trocarSprite(NPCLogica NpcLogica)
    {
        if (NpcLogica == GetComponent<NPCLogica>())
        {
            GetComponent<SpriteRenderer>().sprite = GameManager.Instancia.spriteMorte;
        }
    }

}
using UnityEngine;

public class ClueInteractable : ObjetoInteragivel
{
    protected override void setTipo(){
        tipoAcao = tiposDeAcao.ColetarPista;
    }

    public override bool Interagir(GameObject jogador)
    {
        DispararAcao();

        string textoDaDica = ClueTextGenerator.GerarDica();
        Debug.Log($"'[PISTA ENCONTRADA]:{textoDaDica}'");

        Destroy(gameObject);

        return true;
    }
}


using UnityEngine;

public class ClueInteractable : ObjetoInteragivel
{
    protected override void Start()
    {
        base.Start();
        textoDaDica = ClueTextGenerator.GerarDica();
    }

    public string textoDaDica;

    protected override void setTipo(){
        tipoAcao = tiposDeAcao.ColetarPista;
    }

    public override void AtivarContorno(bool ativar)
    {
        base.AtivarContorno(ativar);

        if(!ativar)
        {
            dialogo.DesativarDialogo();
        }
    }

    public DialogoEmWorldSpace dialogo;

    public override bool Interagir(GameObject jogador)
    {
        DispararAcao();

        Debug.Log($"'[PISTA ENCONTRADA]:{textoDaDica}'");

        dialogo.SoltarDialogo(textoDaDica);

        return true;
    }
}
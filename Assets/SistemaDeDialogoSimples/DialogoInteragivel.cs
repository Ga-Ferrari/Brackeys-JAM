using UnityEngine;

public class DialogoInteragivel : ObjetoInteragivel
{

    private string dialogo = "Dialogo de teste";



    private void SoltarDialogo(string texto)
    {
        Debug.Log(texto);
    }

    public override bool Interagir(GameObject gameObject)
    {
        SoltarDialogo(dialogo);
        return true;
    }

}

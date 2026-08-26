using System.Collections;
using UnityEngine;

public class AnimacaoTextoAparecendo : AnimarTextoDialogo
{
    protected override IEnumerator Animar(string mensagem)
    {
        textoUI.text = "";

        foreach (char letra in mensagem.ToCharArray())
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }
}
using System.Collections;
using System.Linq;
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

    protected override IEnumerator Desanimar()
    {
        // Continua rodando enquanto o texto for maior que zero
        while (textoUI.text.Length > 0)
        {
            // Pega o texto atual e corta a última letra
            textoUI.text = textoUI.text.Substring(0, textoUI.text.Length - 1);

            // Espera o tempo configurado antes de apagar a próxima
            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }
}
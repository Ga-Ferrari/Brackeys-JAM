using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PainelSequencia : MonoBehaviour
{
    [Header("Image")]
    public Image imagemDoPainel;

    [Header("Lista de paineis")]
    public Sprite[] sequenciaDeImagens;

    [Header("Segundos que a imagem fica na tela.")]
    public float tempoPorImagem = 2f;
    
    public UnityEvent eventoFinal;

    private int indiceAtual = 0;

    private void OnEnable()
    {
        indiceAtual = 0;

        if(sequenciaDeImagens.Length > 0)
        {
            imagemDoPainel.sprite = sequenciaDeImagens[0];
        }

        if(tempoPorImagem > 0)
        {
            StartCoroutine(PassarImagensSozinho());
        }
    }

    private void AvancarParaProxima()
    {
        indiceAtual++;

        if(indiceAtual < sequenciaDeImagens.Length)
        {
            imagemDoPainel.sprite = sequenciaDeImagens[indiceAtual];
        }
        else
        {
            eventoFinal?.Invoke();
        }
    }

    private IEnumerator PassarImagensSozinho()
    {
        while(indiceAtual < sequenciaDeImagens.Length)
        {
            yield return new WaitForSeconds(tempoPorImagem);
            AvancarParaProxima();
        }
    }
}

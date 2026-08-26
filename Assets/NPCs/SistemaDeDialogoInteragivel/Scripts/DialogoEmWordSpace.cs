using TMPro;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(AnimarTextoDialogo))]
public class DialogoEmWorldSpace : MonoBehaviour
{

    private CanvasGroup canvasDialogo;

    private AnimarTextoDialogo animadorTexto;
    private string dialogo = "Dialogo de teste";

    [SerializeField] private float tempoAparicao = 0.5f;
    private float timerAparicao = 0f;

    [SerializeField] private float tempoDesaparicao = 0.25f;

    private float timerDesaparicao;
    private bool ativado = false;

    void Start()
    {
        canvasDialogo = gameObject.GetComponent<CanvasGroup>();
        animadorTexto = gameObject.GetComponent<AnimacaoTextoAparecendo>();
        canvasDialogo.alpha = 0f;
    }

    void Update()
    {
        if (ativado)
        {
            canvasDialogo.alpha = math.clamp(math.lerp(0f, 1f, timerAparicao / tempoAparicao), 0, 1);
            timerAparicao += Time.deltaTime;
        }
        else
        {
            canvasDialogo.alpha = math.clamp(math.lerp(0, 1, timerDesaparicao / tempoDesaparicao), 0, 1);
        }


    }

    public void SoltarDialogo(string texto)
    {
        Aparecer();
        animadorTexto.IniciarAnimacao(texto);
    }

    public void Aparecer()
    {
        ativado = true;
    }

    public void DesativarDialogo()
    {
        ativado = false;
        timerAparicao = 0;
    }


}

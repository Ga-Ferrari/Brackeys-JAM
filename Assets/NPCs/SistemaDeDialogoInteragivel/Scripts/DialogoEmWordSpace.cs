using TMPro;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(AnimarTexto))]
public class DialogoEmWorldSpace : MonoBehaviour
{

    private CanvasGroup canvasDialogo;

    private AnimarTexto animadorTexto;
    private string dialogo = "Dialogo de teste";

    [SerializeField] private float tempoAparicao = 0.5f;
    private float timerAparicao = 0f;

    [SerializeField] private float tempoDesaparicao = 0.25f;

    private float timerDesaparicao;
    private bool ativado = false;

    void Start()
    {
        canvasDialogo = gameObject.GetComponent<CanvasGroup>();
        animadorTexto = gameObject.GetComponent<AnimarTexto>();
        canvasDialogo.alpha = 0f;
    }

    void Update()
    {
        if (ativado)
        {
            canvasDialogo.alpha = math.clamp(math.lerp(canvasDialogo.alpha, 1f, timerAparicao / tempoAparicao), 0, 1);
            timerAparicao += Time.deltaTime;
        }
        else
        {
            canvasDialogo.alpha = math.clamp(math.lerp(canvasDialogo.alpha, 0f, timerDesaparicao / tempoDesaparicao), 0, 1);
            timerDesaparicao += Time.deltaTime;
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
        timerAparicao = 0;
    }

    public void DesativarDialogo()
    {
        ativado = false;
        timerDesaparicao = 0;
    }


}

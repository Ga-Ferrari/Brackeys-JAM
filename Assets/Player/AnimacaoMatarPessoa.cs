using Unity.Cinemachine;
using UnityEngine;

public class AnimacaoMatarPessoa : MonoBehaviour
{
    [Header("Configurações de Tela")]
    [SerializeField] private CanvasGroup canvasGroup;
    [Tooltip("Tempo em segundos para a tela escurecer totalmente")]
    [SerializeField] private float tempoApagar = 3f;
    [Tooltip("Tempo em segundos para a tela clarear totalmente")]
    [SerializeField] private float tempoDesapagar = 1f;

    [Header("Configurações de Câmera")]
    [SerializeField] private CinemachineCamera cameraCine;
    [Tooltip("Tamanho da câmera quando estiver no zoom máximo (menor valor = mais perto)")]
    [SerializeField] private float zoomAlvo = 3f;

    private float zoomOriginal;
    private bool animandoNoite = false;

    void Start()
    {
        EventBus.travarControles += IniciarAnimacaoIniciarNoite;
        EventBus.iniciarManha += FinalizarAnimacaoNoite;
        // Salva o tamanho original da câmera para podermos voltar ao normal depois
        zoomOriginal = cameraCine.Lens.OrthographicSize;
    }

    private void OnDestroy()
    {
        // Sempre lembre de remover a inscrição do evento ao destruir o objeto
        EventBus.travarControles -= IniciarAnimacaoIniciarNoite;
    }

    void Update()
    {
        // Como a 'Lens' é uma struct no C#, precisamos extrair, modificar e devolver
        var lente = cameraCine.Lens;

        if (animandoNoite)
        {
            // 1. Escurece a tela
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, Time.deltaTime / tempoApagar);

            // 2. Aproxima a câmera sincronizada com o tempo de apagar
            float velocidadeZoom = Mathf.Abs(zoomOriginal - zoomAlvo) / tempoApagar;
            lente.OrthographicSize = Mathf.MoveTowards(lente.OrthographicSize, zoomAlvo, velocidadeZoom * Time.deltaTime);
            if (canvasGroup.alpha >= 1f)
            {
                EventBus.IniciarNoite();
            }
        }
        else
        {
            // 1. Clareia a tela
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, Time.deltaTime / tempoDesapagar);

            // 2. Afasta a câmera sincronizada com o tempo de desapagar
            float velocidadeZoom = Mathf.Abs(zoomOriginal - zoomAlvo) / tempoDesapagar;
            lente.OrthographicSize = Mathf.MoveTowards(lente.OrthographicSize, zoomOriginal, velocidadeZoom * Time.deltaTime);
        }

        // Aplica as modificações de volta na câmera
        cameraCine.Lens = lente;
    }

    public void IniciarAnimacaoIniciarNoite()
    {
        animandoNoite = true;
    }

    public void FinalizarAnimacaoNoite()
    {
        animandoNoite = false;
    }
}
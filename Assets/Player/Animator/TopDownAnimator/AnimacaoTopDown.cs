using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class AnimacaoTopDown : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animador;
    private bool isFlipped = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        EventBus.atirar += Atirar;
    }

    void Update()
    {
        // Vira o personagem baseado na direção da velocidade
        if (rb.linearVelocityX > 0) animador.SetBool("EstaDireita", true);
        else if (rb.linearVelocityX < 0) animador.SetBool("EstaDireita", false);

        // Usa Mathf.Abs para garantir que o Animator receba sempre um valor positivo
        animador.SetFloat("velocidade", Mathf.Abs(rb.linearVelocity.magnitude));
    }

    private void Atirar(GameObject alvo)
    {
        float direcaoX = alvo.transform.position.x - transform.position.x;

        // Pega a escala atual
        Vector3 escala = transform.localScale;

        // Aplica o sinal da direção sobre o valor absoluto da escala
        escala.x = Mathf.Abs(escala.x) * Mathf.Sign(direcaoX);

        // Devolve o Vector3 completo
        transform.localScale = escala;

        animador.SetTrigger("Atirar");
    }

}

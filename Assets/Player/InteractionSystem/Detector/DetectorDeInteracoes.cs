using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectorDeInteracoes : MonoBehaviour
{
    [SerializeField] private float raioDeInteracao = 2f;
    [SerializeField] private LayerMask layerInteragivel; // Para o radar ignorar paredes e chão

    private IInteractable alvoAtual;

    public static bool interacaoBloqueada = false;
    public static IInteractable alvoTravado = null;


    void FixedUpdate()
    {
        BuscarAlvoMaisProximo();
    }

    private void BuscarAlvoMaisProximo()
    {
        Collider2D[] encontrados = Physics2D.OverlapCircleAll(transform.position, raioDeInteracao, layerInteragivel);

        IInteractable maisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (Collider2D col in encontrados)
        {
            // Primeiro tenta pegar o Menu, se não achar, pega qualquer outra interação padrão
            IInteractable interagivel = col.GetComponent<MenuDeInteracoes>();
            if (interagivel == null)
            {
                interagivel = col.GetComponent<IInteractable>();
            }
            if (interagivel != null)
            {
                float distancia = Vector2.Distance(transform.position, col.transform.position);
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    maisProximo = interagivel;
                }
            }
        }

        if (maisProximo != alvoAtual)
        {
            if (alvoAtual != null)
            {
                alvoAtual.AtivarContorno(false);
            }

            alvoAtual = maisProximo;

            if (alvoAtual != null)
            {
                alvoAtual.AtivarContorno(true);
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Se estiver lendo diálogo, ignora a busca por interações
        if (context.performed && interacaoBloqueada)
        {
            alvoTravado.Interagir(gameObject);
            return;
        }

        if (context.performed && alvoAtual != null)
        {
            alvoAtual.Interagir(gameObject);
        }
    }

    public void OnMoverUi(InputAction.CallbackContext context)
    {
        INavegarMenu menu = (INavegarMenu)alvoAtual;
        if (menu != null)
        {
            menu.OnNavegarMenu(context);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioDeInteracao);
    }




}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectorDeInteracoes : MonoBehaviour
{

    [SerializeField] private float raioDeInteracao = 2f;
    [SerializeField] private LayerMask layerInteragivel; // Para o radar ignorar paredes e chão

    private IInteractable alvoAtual;

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
            IInteractable interagivel = col.GetComponent<IInteractable>();
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
        if (context.performed && alvoAtual != null)
        {
            alvoAtual.Interagir(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioDeInteracao);
    }




}

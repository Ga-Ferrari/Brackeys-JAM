using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class TopMovementController : MonoBehaviour, IMovement
{

    [SerializeField] private float velocidadeMax = 5f;
    [SerializeField] private float aceleracao = 20f;     // Quão rápido ele chega na vel. máx.
    [SerializeField] private float desaceleracao = 15f;  // Quão rápido ele freia e escorrega

    private Vector2 direcaoInput;
    public float velocidadeX = 0;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector2 velocidadeAlvo = direcaoInput * velocidadeMax;

        float taxaMudanca = (direcaoInput.magnitude > 0) ? aceleracao : desaceleracao;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, velocidadeAlvo, taxaMudanca * Time.fixedDeltaTime);
    }
    public bool Mover(Vector2 _direcao)
    {
        direcaoInput = _direcao.normalized;
        return true;
    }

}

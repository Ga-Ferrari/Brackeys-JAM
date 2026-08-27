using System.Collections;
using TMPro;
using UnityEngine;

public abstract class AnimarTextoDialogo : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textoUI;
    [SerializeField] protected float tempoEntreLetras = 0.05f;

    // Essa é a função que o NPC vai chamar de forma simples!
    public void IniciarAnimacao(string mensagem)
    {
        // Interrompe qualquer texto antigo que ainda esteja sendo digitado
        StopAllCoroutines();

        // Inicia a corotina aqui dentro da própria classe
        StartCoroutine(Animar(mensagem));
    }

    public void DesativarAnimacao()
    {
        StopAllCoroutines();
        StartCoroutine(Desanimar());
    }

    // A corotina abstrata fica protegida, só os scripts filhos mexem nela
    protected abstract IEnumerator Animar(string mensagem);
    protected abstract IEnumerator Desanimar();
}
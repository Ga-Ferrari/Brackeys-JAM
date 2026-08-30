using UnityEngine;

public class UIsManager : MonoBehaviour
{
    [Header("Arraste os paineis desativados aqui")]

    public GameObject painelDormir;
    public GameObject painelMorte;
    public GameObject painelMatarImpostor;

    private void OnEnable()
    {
        EventBus.ativarPainel += LigarPainelDormir;
        EventBus.ativarPainel += LigarPainelMorte;
        EventBus.ativarPainel += LigarPainelMatarImpostor;
    }

    private void OnDisable()
    {
        EventBus.ativarPainel -= LigarPainelDormir;
        EventBus.ativarPainel -= LigarPainelMorte;
        EventBus.ativarPainel -= LigarPainelMatarImpostor;
    }

    private void LigarPainelDormir() {
        if(GameManager.Instancia.estado == 1)
        {
            Debug.Log($"'{GameManager.Instancia.estado}, dormir");
            painelDormir.SetActive(true);
        }
    }

    private void LigarPainelMorte()
    {
        if(GameManager.Instancia.estado == 2)
        {
            Debug.Log($"'{GameManager.Instancia.estado}, morrer");
            painelMorte.SetActive(true);
        }
    }
    private void LigarPainelMatarImpostor()
    {
        if(GameManager.Instancia.estado == 3)
        {
            Debug.Log($"'{GameManager.Instancia.estado}, matarimpostor");
            painelMatarImpostor.SetActive(true);
        }
    }
}
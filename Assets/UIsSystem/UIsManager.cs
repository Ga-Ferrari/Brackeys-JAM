using UnityEngine;

public class UIsManager : MonoBehaviour
{
    [Header("Arraste os paineis desativados aqui")]

    public GameObject painelDormir;
    public GameObject painelMorte;
    public GameObject painelMatarImpostor;

    private void OnEnable()
    {
        EventBus.OnDormirCama += LigarPainelDormir;
        EventBus.OnMortePlayer += LigarPainelMorte;
        EventBus.OnMatarImpostor += LigarPainelMatarImpostor;
    }

    private void OnDisable()
    {
        EventBus.OnDormirCama -= LigarPainelDormir;
        EventBus.OnMortePlayer -= LigarPainelMorte;
        EventBus.OnMatarImpostor -= LigarPainelMatarImpostor;
    }

    private void LigarPainelDormir() { painelDormir.SetActive(true); }
    private void LigarPainelMorte() { painelMorte.SetActive(true); }
    private void LigarPainelMatarImpostor() { painelMatarImpostor.SetActive(true); }
}
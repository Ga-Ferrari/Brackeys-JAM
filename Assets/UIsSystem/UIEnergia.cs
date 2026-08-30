using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergia : MonoBehaviour
{
    [SerializeField] private List<Image> energias;

    public void atualizarEnergia(int valor)
    {
        // Percorre a lista completa de imagens
        for (int i = 0; i < energias.Count; i++)
        {
            // Se o índice for menor que o valor atual, ativa a imagem.
            // Se for maior ou igual, desativa.
            energias[i].enabled = (i < valor);
        }
    }
}
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //scripts
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private InteractionSystem interaction;
    [SerializeField] private DeathSystem death;

    //lista de npcs
    private List<npcScript> npcList;
    //variavel para armazenar o npc que será o impostor
    private npcScript impostor;

    private void Awake()
    {
        //funcao que retorna um vetor com todos os npcs na cena
        npcScript[] npcArray = FindAnyObjectsOfType<npcScript>(FindObjectsSortMode.None);
        //muda para lista para que possa ser atualizada
        npcList = new List<npcScript>(npcArray);
    }

    private void Start()
    {
        sortImpostor();
        turnManager.systemConector(interaction, death);
        turnManager.IniciarDia();
    }

    private void sortImpostor()
    {
        if(npcList.Count == 0) return;

        int iSort = UnityEngine.Random.Range(0, npcList.Count); //sorteia o indice do npc será o impostor
        impostor = npcList[iSort]; //atribui o npc sorteado para impostor
        impostor.tornarImpostor();//executa a funcao do script sorteado
        Debug.Log("Impostor sorteado");
        
        npcList.Remove(impostor);
        Debug.Log("Impostor removido");
    }
}

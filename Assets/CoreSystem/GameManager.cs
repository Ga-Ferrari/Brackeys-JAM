using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;
    //scripts
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private DeathSystem death;

    public Sprite spriteMorte;

    //lista de npcs
    public List<NPCLogica> npcList;
    //variavel para armazenar o npc que será o impostor
    public NPCLogica impostor;

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);

        //funcao que retorna um vetor com todos os npcs na cena
        NPCLogica[] npcArray = FindObjectsByType<NPCLogica>();
        //muda para lista para que possa ser atualizada
        npcList = new List<NPCLogica>(npcArray);
    }

    private void Start()
    {
        sortImpostor();
        turnManager.systemConector(death);
        turnManager.IniciarDia();

        death.npcAdmin(npcList);
    }

    private void OnDestroy()
    {
        if (Instancia == this)
        {
            Instancia = null;
        }
    }

    private void sortImpostor()
    {
        if (npcList.Count == 0) return;

        int iSort = UnityEngine.Random.Range(0, npcList.Count); //sorteia o indice do npc será o impostor
        impostor = npcList[iSort]; //atribui o npc sorteado para impostor
        Debug.Log("Impostor sorteado");

        npcList.Remove(impostor);
        Debug.Log("Impostor removido");
    }

    public void CarregarMenu()
    {
        SceneManager.LoadScene(0);
    }
}

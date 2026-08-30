using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;
    //scripts
    [SerializeField] public TurnManager turnManager;
    [SerializeField] private DeathSystem death;
    public List<NPCLogica> npcsAMorrer = new List<NPCLogica>();

    public Sprite spriteMorte;

    public bool primeiraInteracao = true;
    public List<string> primeiraFala = new List<string>();
    public int posPrimeiraFala = 0;
    //lista de npcs
    public List<NPCLogica> npcList;
    //variavel para armazenar o npc que será o impostor
    public NPCLogica impostor;

    public List<List<string>> falasNpcs = new List<List<string>>();

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
        EventBus.iniciarManha += SetarFalasDosNPCs;
    }

    private void OnDestroy()
    {
        if (Instancia == this)
        {
            Instancia = null;
        }
    }

    private void SetarFalasDosNPCs()
    {
        foreach (NPCLogica npc in death.npcsVivos)
        {
            int indiceAleatorio = UnityEngine.Random.Range(0, falasNpcs.Count);
            npc.GetComponent<FalasNPC>().setFalas(falasNpcs[indiceAleatorio]);
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
}

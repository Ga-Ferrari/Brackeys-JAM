using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ConjuntoDeFalas
{
    public List<string> falas = new List<string>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;
    //scripts
    [SerializeField] public TurnManager turnManager;
    [SerializeField] public DeathSystem death;
    public List<NPCLogica> npcsAMorrer = new List<NPCLogica>();

    public Sprite spriteMorte;

    public bool primeiraInteracao = true;
    public List<string> primeiraFala = new List<string>();
    public int posPrimeiraFala = 0;
    //lista de npcs
    public List<NPCLogica> npcList;
    //variavel para armazenar o npc que será o impostor
    public NPCLogica impostor;

    public int estado = 1;

    public List<ConjuntoDeFalas> falasNpcs = new List<ConjuntoDeFalas>();

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
        EventBus.IniciarManha();
        StartCoroutine(correcaoPrimeiraExecucao());
        EventBus.iniciarManha += SetarFalasDosNPCs;
    }

    private IEnumerator correcaoPrimeiraExecucao()
    {
        yield return new WaitForSeconds(0.5f);

        SetarFalasDosNPCs();
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
            npc.GetComponent<FalasNPC>().setFalas(falasNpcs[indiceAleatorio].falas);
            Debug.Log("Setando falas dos NPCs");
        }
        int indice = UnityEngine.Random.Range(0, falasNpcs.Count);
        impostor.GetComponent<FalasNPC>().setFalas(falasNpcs[indice].falas);
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

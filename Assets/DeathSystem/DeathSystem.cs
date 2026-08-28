using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeathSystem : MonoBehaviour
{
    public event Action OnFimRotinaMorte;
    [SerializeField] TurnManager turnManager;
    private List<NPCLogica> npcsVivos = new List<NPCLogica>();
    private void Start()
    {
        turnManager.nightfallTurn += iniciarRotinaNoiteMorte;
    }

    public void npcAdmin(List<NPCLogica> targets)
    {
        npcsVivos = targets;

        //ativar o evento para cada script de npc
        foreach (NPCLogica npc in npcsVivos)
        {
            npc.faleceuEvent += removerNpc;
        }
    }

    public void removerNpc(NPCLogica npcMorto)
    {
        if (npcsVivos.Contains(npcMorto))
        {
            npcsVivos.Remove(npcMorto);

            npcMorto.faleceuEvent -= removerNpc;

            Debug.Log("Vitima removida da lista com sucesso");
        }
    }

    private void iniciarRotinaNoiteMorte()
    {
        StartCoroutine(RotinaNoiteMorte());
    }

    private IEnumerator RotinaNoiteMorte()
    {
        Debug.Log("Iniciando a noite...");

        yield return new WaitForSeconds(3f); //tempo para uma animação ou cutscene

        Debug.Log("Iniciando funcao para morte aleatoria");
        executarMorteAleatoria();

        Debug.Log("Cutscene e morte terminou. Retornando ao dia");

        OnFimRotinaMorte?.Invoke();
    }

    private void executarMorteAleatoria()
    {
        int quantidadeMortes = UnityEngine.Random.Range(1, 3);
        Debug.Log($"O impostor matara {quantidadeMortes}");

        for (int i = 0; i < quantidadeMortes; i++)
        {
            if (npcsVivos.Count == 0) break;

            int iAlvo = UnityEngine.Random.Range(0, npcsVivos.Count);
            NPCLogica vitima = npcsVivos[iAlvo];

            vitima.Morrer(); //essa funcao vai lancar o evento faleceuEvent que vai chamar a funcao para remover da lista
        }
    }
}

//resumindo o deathsystem: ele recebe a lista de npcs ja sem o impostor. 
//Quando o evento da noite é lancado ele capta e inicia a rotina de morte, que chama a funcao para decidir qual ou quais nps vão morrer
//Essa funcao ativa no script do npc sorteado a funcao que mata ele e que lanca um evento faleceuEvent, que é captado pelo deathsystem e atualiza a lista de npcs vivos
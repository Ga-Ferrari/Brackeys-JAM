
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class FalasNPC : AcaoComCusto
{
    private int npcID;

    [SerializeField] private DialogoEmWorldSpace dialogo;

    [SerializeField] private List<string> falas = new List<string>();
    private int falaAtual = 0;

    void Awake()
    {
        custo = 1;
    }

    protected override void setTipo()
    {
        tipoAcao = tiposDeAcao.InteracaoNPC;
    }

    public override bool Interagir(GameObject gameObject)
    {
        base.Interagir(gameObject);
        DetectorDeInteracoes.interacaoBloqueada = true;
        DetectorDeInteracoes.alvoTravado = this;
        if (falas.Count > 0)
        {
            if (GameManager.Instancia.posPrimeiraFala == GameManager.Instancia.primeiraFala.Count) GameManager.Instancia.primeiraInteracao = false;
            if (GameManager.Instancia.primeiraInteracao)
            {
                dialogo.SoltarDialogo(GameManager.Instancia.primeiraFala[GameManager.Instancia.posPrimeiraFala++]);
            }
            else
            {
                if (falaAtual == falas.Count)
                {
                    dialogo.DesativarDialogo();
                    falaAtual = 0;
                    DetectorDeInteracoes.interacaoBloqueada = false;
                }
                else if (falaAtual < falas.Count) dialogo.SoltarDialogo(falas[falaAtual++]);
            }

        }
        interagido = true;

        return true;
    }

    public bool setFalas(List<string> novasFalas)
    {
        List<string> falasTratadas = new List<string>();
        string meuNome = GetComponent<NPCAtributos>().name; // Salva fora do loop para otimizar

        foreach (string fala in novasFalas)
        {
            if (fala.Contains("<name>"))
            {
                NPCAtributos npcAtt = null;

                // Trava de segurança: evita um loop infinito se este for o único NPC vivo
                if (GameManager.Instancia.death.npcsVivos.Count > 1)
                {
                    do
                    {
                        NPCLogica npc = GameManager.Instancia.death.npcsVivos[UnityEngine.Random.Range(0, GameManager.Instancia.death.npcsVivos.Count)];
                        npcAtt = npc.GetComponent<NPCAtributos>();
                    } while (npcAtt.name == meuNome);

                    // Strings em C# são imutáveis. O Replace retorna uma NOVA string, não altera a original.
                    string falaSubstituida = fala.Replace("<name>", npcAtt.name);
                    falasTratadas.Add(falaSubstituida);
                }
                else
                {
                    // Fallback caso ele seja o último sobrevivente
                    falasTratadas.Add(fala.Replace("<name>", "alguém"));
                }
            }
            else
            {
                // Se a fala não tiver <name>, ela precisa ser adicionada mesmo assim!
                falasTratadas.Add(fala);
            }
        }

        falas = falasTratadas;
        return true;
    }

    public override void AtivarContorno(bool ativar)
    {
        base.AtivarContorno(ativar);
        if (!ativar)
        {
            dialogo.DesativarDialogo();
        }
    }



}
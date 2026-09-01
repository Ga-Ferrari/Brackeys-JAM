
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class FalasNPC : AcaoComCusto
{
    private int npcID;

    [SerializeField] private DialogoEmWorldSpace dialogo;

    [SerializeField] private List<string> falas = new List<string>();
    private int falaAtual = 0;

    private bool falandoVerdade = false;

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
        if (falandoVerdade)
            Debug.Log("Falando a verdade");
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

        int verdade = Random.Range(0, 2);
        Debug.Log("Verdade: " + verdade);
        foreach (string fala in novasFalas)
        {
            if (fala.Contains("<name>"))
            {
                NPCAtributos npcAtt = null;

                // Trava de segurança: evita um loop infinito se este for o único NPC vivo
                if (GameManager.Instancia.death.npcsVivos.Count > 1)
                {

                    string nome = "";
                    if (verdade == 1)
                    {
                        falandoVerdade = true;
                        if (fala.Contains("I spent the day with <name>."))
                        {
                            Debug.Log(meuNome + " estou defendendo x");
                            do
                            {

                                NPCLogica npc = GameManager.Instancia.death.npcsVivos[UnityEngine.Random.Range(0, GameManager.Instancia.death.npcsVivos.Count)];
                                npcAtt = npc.GetComponent<NPCAtributos>();
                                nome = npcAtt.nome;
                            }
                            while (nome == meuNome && nome == GameManager.Instancia.impostor.GetComponent<NPCAtributos>().nome);
                        }
                        else
                        {
                            nome = GameManager.Instancia.impostor.GetComponent<NPCAtributos>().nome;
                            if (nome == meuNome)
                            {
                                do
                                {
                                    NPCLogica npc = GameManager.Instancia.death.npcsVivos[UnityEngine.Random.Range(0, GameManager.Instancia.death.npcsVivos.Count)];
                                    npcAtt = npc.GetComponent<NPCAtributos>();
                                    nome = npcAtt.nome;
                                } while (npcAtt.name == meuNome);
                            }
                        }
                    }
                    else
                    {
                        falandoVerdade = false;
                        do
                        {
                            NPCLogica npc = GameManager.Instancia.death.npcsVivos[UnityEngine.Random.Range(0, GameManager.Instancia.death.npcsVivos.Count)];
                            npcAtt = npc.GetComponent<NPCAtributos>();
                            nome = npcAtt.nome;
                        } while (npcAtt.name == meuNome);
                    }


                    // Strings em C# são imutáveis. O Replace retorna uma NOVA string, não altera a original.
                    string falaSubstituida = fala.Replace("<name>", nome);
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
using UnityEngine;
using System.Collections.Generic;

public static class ClueTextGenerator
{
    //enum dos tipos de dica do jogo
    private enum TipoDica {Suspeitos, Confiavel, Idade, Genero}

    //lista dos tipos de dica
    private static List<TipoDica> dicasDisponiveis = new List<TipoDica>
    {
        TipoDica.Suspeitos,
        TipoDica.Confiavel,
        TipoDica.Idade,
        TipoDica.Genero
    };

    //funcao para caso a partida reiniciar
    public static void ResetarListaDicas()
    {
        dicasDisponiveis = new List<TipoDica>
        {
            TipoDica.Suspeitos,
            TipoDica.Confiavel,
            TipoDica.Idade,
            TipoDica.Genero
        };
    }

    public static string GerarDica()
    {
        if(dicasDisponiveis.Count == 0)
        {
            Debug.Log("Nao ha mais dicas");
            return "...";
        }

        //sorteio da dica
        int iSort = UnityEngine.Random.Range(0, dicasDisponiveis.Count);
        TipoDica dicaSorteada = dicasDisponiveis[iSort];


        if(dicaSorteada != TipoDica.Confiavel)
        {
            dicasDisponiveis.RemoveAt(iSort);
        }

        //obetem impostor e lista de npcs
        NPCLogica assassino = GameManager.Instancia.impostor;
        if(assassino == null)
        {
            Debug.Log("Impostor nulo");
            return "...";
        }

        NPCAtributos atributos = assassino.GetComponent<NPCAtributos>();
        if(atributos == null)
        {      
            Debug.Log("Impostor sem atributos");
            return "...";
        }

        List<NPCLogica> inocentes = GameManager.Instancia.npcList;
        if(dicaSorteada == TipoDica.Suspeitos && inocentes.Count <= 2)
        {
            dicaSorteada = TipoDica.Confiavel;
        }

        inocentes.RemoveAll(npc => npc == null);
        if(inocentes.Count == 0)//verifica se todos os inocentes já morreram
        {
            return "CUIDADO!!";
        }

        switch(dicaSorteada) {
            case TipoDica.Suspeitos:
                return GerarDicaSuspeitos(atributos.nome, inocentes);
            case TipoDica.Confiavel:
                int inocenteConfiavel = UnityEngine.Random.Range(0, inocentes.Count);
                string nomeInocente = inocentes[inocenteConfiavel].GetComponent<NPCAtributos>().nome;
                return $"'{nomeInocente} is trustworthy.'";
            case TipoDica.Idade:
                string textoIdade = (atributos.idade == Idade.Adulto) ? "an adult" : "not an adult";
                return $"'I saw that it was {textoIdade}.'";
            case TipoDica.Genero:
                string textoGenero = (atributos.sexo == Sexo.Masculino) ? "a man" : "a woman";
                return $"'It was definitely {textoGenero}.'";
            default:
                Debug.Log("Pista de tipo desconhecido");
                return "...";
        }
    }

    private static string GerarDicaSuspeitos(string nomeImpostor, List<NPCLogica> inocentes)
    {
        //sorteia 2 inocentes e garante que não seja o mesmo
        int i1 = UnityEngine.Random.Range(0, inocentes.Count);
        int i2 = UnityEngine.Random.Range(0, inocentes.Count);
        while (i1 == i2)
        {
            i2 = UnityEngine.Random.Range(0, inocentes.Count);
        }

        string inocente1 = inocentes[i1].GetComponent<NPCAtributos>().nome;
        string inocente2 = inocentes[i2].GetComponent<NPCAtributos>().nome;
        
        //coloca os inocentes e o impostor em uma lista
        List<string> nomes = new List<string>{ nomeImpostor, inocente1, inocente2};
        //embaralha a lista
        nomes.Sort((a,b) => UnityEngine.Random.value.CompareTo(0.5f));

        return $"'I'm suspicious of {nomes[0]}, {nomes[1]} and {nomes[2]}'";
    }
}
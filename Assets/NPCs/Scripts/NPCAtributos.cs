using UnityEngine;

public enum Sexo {Masculino, Feminino}
public enum Idade {Jovem, Adulto}

public class NPCAtributos : MonoBehaviour
{
    [Header("Identidade do NPC")]
    public string nome = "Desconhecido";
    public Sexo sexo;
    public Idade idade;
}

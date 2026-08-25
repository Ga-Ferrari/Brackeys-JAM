using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private InteractionSystem interaction;
    [SerializeField] private DeathSystem death;

    private void Start()
    {
        turnManager.systemConector(interaction, death);
        turnManager.IniciarDia();
    }

    private void sortImpostor()
    {
        
    }
}

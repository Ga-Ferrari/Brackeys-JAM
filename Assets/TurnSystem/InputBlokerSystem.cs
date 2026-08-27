using UnityEngine;
using UnityEngine.InputSystem;
public class InputBlokerSystem : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private PlayerInput playerInput;
    void Start()
    {
        turnManager.daylightTurn += destravarControles;
        turnManager.nightfallTurn += travarControles;
    }
    
    private void travarControles() //desativa o playerInput se receber o evento de troca de turno para noite
    {
        if(playerInput != null)
        {
            playerInput.enabled = false;
        }
    } 

    private void destravarControles() //reativa o playerInput quando troca de turno para dia
    {
        if(playerInput != null)
        {
            playerInput.enabled = true;
        }
    } 
}

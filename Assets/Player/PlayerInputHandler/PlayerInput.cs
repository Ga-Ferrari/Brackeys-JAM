using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandler : MonoBehaviour
{

    private IMovement scriptMovimento;


    void Start()
    {
        scriptMovimento = gameObject.GetComponent<IMovement>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        scriptMovimento.Mover(context.ReadValue<Vector2>());
    }

}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandler : MonoBehaviour
{

    private IMovement scriptMovimento;
    private DetectorDeInteracoes detector;

    void Start()
    {
        scriptMovimento = gameObject.GetComponent<IMovement>();
        detector = gameObject.GetComponent<DetectorDeInteracoes>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        scriptMovimento.Mover(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        detector.OnInteract(context);
    }

    public void OnMoverUI(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            detector.OnMoverUi(context);
        }
    }

}

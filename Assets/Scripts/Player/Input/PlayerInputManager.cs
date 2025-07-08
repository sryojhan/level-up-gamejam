using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/Input controller")]
public class PlayerInputManager : MonoBehaviour
{

    PlayerInputMap inputMap;

    private void Awake()
    {
        inputMap = new PlayerInputMap();    
    }

    private void OnEnable()
    {
        inputMap.Enable();
    }

    private void OnDisable()
    {
        inputMap.Disable();
    }

    public Vector2 GetLeftStick()
    {
        return inputMap.Player.Move.ReadValue<Vector2>();
    }

    public bool WantsToShoot()
    {
        return inputMap.Player.LeftButton.WasPressedThisFrame();
    }

    public bool WantsToInteract()
    {
        return inputMap.Player.DownButton.WasPressedThisFrame();
    }

    public bool WantsToAttack()
    {
        return inputMap.Player.RightButton.WasPressedThisFrame();
    }
}

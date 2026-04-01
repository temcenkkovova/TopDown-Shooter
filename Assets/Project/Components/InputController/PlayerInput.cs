using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
  private PlayerController input;
  private PlayerMovement movement;
  private PlayerWeaponController weaponController;
  private CharacterController controller;
  private PlayerAnimationController playerAnimationController;

  private void Awake()
  {
    input = new PlayerController();
    movement = GetComponent<PlayerMovement>();
    weaponController = GetComponent<PlayerWeaponController>();
    controller = GetComponent<CharacterController>();
    playerAnimationController = GetComponent<PlayerAnimationController>();

  }

  void Update()
  {

    Vector3 inputVector = input.Player.Move.ReadValue<Vector3>();
    movement.TryMove(controller, inputVector);

  }

  private void OnEnable()
  {
    input.Enable();


    input.Player.Fire.performed += OnShoot;
    input.Player.Reload.performed += OnReload;
    input.Game.Pause.performed += OnPause;

  }

  private void OnDisable()
  {


    input.Player.Fire.performed -= OnShoot;
    input.Player.Reload.performed -= OnReload;
    input.Game.Pause.performed -= OnPause;
    input.Disable();
  }



  public void OnShoot(InputAction.CallbackContext context)
  {
    weaponController.HandleFire();
  }
  public void OnReload(InputAction.CallbackContext context)
  {
    weaponController.HandleReload();
  }

  public void OnPause(InputAction.CallbackContext context)
  {

    GameStateController.Instance.SetPause(!GameStateController.Instance.IsPause);
  }
}
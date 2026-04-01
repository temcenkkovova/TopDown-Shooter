using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public float Speed;
  public float RotateSpeed;
  [NonSerialized] public CharacterController controller;
  public bool isMoving;
  public event Action<float> OnSpeedChanged;

  float gravity = -9.81f;
  float verticalVelocity;
  [NonSerialized] public float velocity;

  void Awake()
  {
    // controller = GetComponent<CharacterController>();
  }
  void Start()
  {

    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;

  }
  void Update()
  {
    RotateToMouse();
    if (!controller) return;
    velocity = controller.velocity.magnitude;
  }
  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }
  public void Init(PlayerStats stats)
  {
    Speed = stats.MoveSpeed;
    stats.OnMoveSpeedChanged += OnMaxSpeedChanged;
    RotateSpeed = stats.RotateSpeed;
  }
  public void OnMaxSpeedChanged(float maxSpeed)
  {
    Speed = maxSpeed;
    OnSpeedChanged?.Invoke(Speed);

  }
  public void TryMove(CharacterController controller, Vector3 input)
  {
    this.controller = controller;
    if (verticalVelocity < 0)
      verticalVelocity = -2f;

    verticalVelocity += gravity * Time.deltaTime;

    Vector3 move = transform.forward * input.y + transform.right * input.x;
    move *= Speed;
    move.y = verticalVelocity;

    controller.Move(move * Time.deltaTime);

  }

  private void RotateToMouse()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, transform.position);

    if (groundPlane.Raycast(ray, out float enter))
    {
      Vector3 hitPoint = ray.GetPoint(enter);
      Vector3 dir = hitPoint - transform.position;
      dir.y = 0f;

      if (dir.sqrMagnitude < 0.001f) return;

      Quaternion targetRotation = Quaternion.LookRotation(dir);
      transform.rotation = Quaternion.Slerp(
          transform.rotation,
          targetRotation,
          RotateSpeed * Time.deltaTime);
    }
  }

  private void OnGameStateChanged(GameState state)
  {
    enabled = state == GameState.Gameplay;

  }
}
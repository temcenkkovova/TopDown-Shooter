using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
  private PlayerMovement movement;
  [NonSerialized] public Animator animator;

  void Awake()
  {
    movement = GetComponent<PlayerMovement>();
    animator = GetComponent<Animator>();

  }

  void Update()
  {
    if (!movement || !animator) return;
    float horizonalVelocity = movement.velocity;


    float speed = horizonalVelocity;
    animator.SetFloat("Speed", speed);
  }
  void Start()
  {

    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;

  }
  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }
  private void OnGameStateChanged(GameState state)
  {
    enabled = state == GameState.Gameplay;

  }
}
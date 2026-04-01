using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Transform target;
  [SerializeField] private float followSpeed = 10f;
  private Vector3 offset = new Vector3(0, 30f, 0);

  private void LateUpdate()
  {
    if (target == null) return;

    Vector3 desiredPosition = target.position + offset;

    transform.position = Vector3.Lerp(
           transform.position, desiredPosition,
           followSpeed * Time.deltaTime
       );
  }
  void OnEnable()
  {
    if (GameStateController.Instance != null)
      GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;

  }
  void OnDisable()
  {
    if (GameStateController.Instance != null)
      GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }
  private void OnGameStateChanged(GameState state)
  {
    enabled = state == GameState.Gameplay;

  }
}
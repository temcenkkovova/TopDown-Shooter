using UnityEngine;

public class GameOverManagerUI : MonoBehaviour
{
  public GameObject GameOverPanel;

  void Start()
  {
    GameStateController.Instance.OnGameStateChanged += OpenPanel;
  }

  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OpenPanel;
  }

  private void OpenPanel(GameState state)
  {
    GameOverPanel.SetActive(state == GameState.GameOver);
  }
}
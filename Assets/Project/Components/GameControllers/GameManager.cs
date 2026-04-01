using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public GameFlowController flow;
  public GameEconomy economy;

  void Start()
  {
    if (!economy) return;
    economy.InitProvider(flow);
    PlayerHealth.OnPlayerDied += SetGameOver;
  }
  void OnDestroy()
  {

    PlayerHealth.OnPlayerDied -= SetGameOver;
  }
  private void SetGameOver()
  {

    GameStateController.Instance.SetState(GameState.GameOver);
  }
  public void LoadMenuScene()
  {
    GameStateController.Instance.SetState(GameState.Menu);
    SceneManager.LoadScene("Menu");
  }
  public void RestartGame()
  {
    GameStateController.Instance.SetState(GameState.Gameplay);
    SceneManager.LoadScene("Main");
  }

  public void LoadGameScene()
  {


    SceneManager.LoadScene("Main");
    GameStateController.Instance.SetState(GameState.Gameplay);
    GameStateController.Instance.SetPause(false);

  }
}
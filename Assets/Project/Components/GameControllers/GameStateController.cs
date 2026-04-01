using System;
using UnityEngine;
public enum GameState { Gameplay, Menu, GameOver }
public class GameStateController : MonoBehaviour
{


  public static GameStateController Instance;

  public GameState CurrentState;
  public event Action<GameState> OnGameStateChanged;
  public event Action<bool> OnPauseChanged;
  public bool IsPause;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
  public void SetState(GameState state)
  {
    if (CurrentState == state) return;

    CurrentState = state;
    OnGameStateChanged?.Invoke(CurrentState);

  }

  public void SetPause(bool pause)
  {
    if (IsPause == pause) return;
    IsPause = pause;
    Time.timeScale = IsPause ? 0f : 1f;
    OnPauseChanged?.Invoke(IsPause);
  }
}
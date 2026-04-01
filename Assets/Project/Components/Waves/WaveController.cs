using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

  public enum WaveState { Preparing, Running, Finished };

  public event Action<WaveState> OnWaveStateChanged;
  public event Action<float> OnWaveTimeChanged;
  public event Action<float> OnPreparingTimeChanged;

  [SerializeField] public WaveState CurrentWaveState;
  public IWaveState currentState;
  public event Action OnWaveStarted;
  public event Action OnWavePreparing;
  public event Action OnWaveFinished;

  private WaveRunningState runningState;
  private WavePreparingState preparingState;
  private WaveFinishedState finishedState;
  public WaveRunningState RunningState => runningState;
  public WavePreparingState PreparingState => preparingState;
  public WaveFinishedState FinishedState => finishedState;
  private float timeRemaining;
  public float TimeRemaining => timeRemaining;

  private EnemyWaveController enemyWaveController;
  private WaveMenuControllerUI waveMenuControllerUI;


  [NonSerialized] public float maxAmountTimer = 3;
  void Awake()
  {
    enemyWaveController = GetComponent<EnemyWaveController>();
    waveMenuControllerUI = GetComponent<WaveMenuControllerUI>();
    runningState = new WaveRunningState(this, enemyWaveController);
    preparingState = new WavePreparingState(this, waveMenuControllerUI);
    finishedState = new WaveFinishedState(this);
  }

  void Start()
  {
    SwitchState(RunningState);
    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;

  }
  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }
  public void InvokeWaveStarted()
  {
    OnWaveStarted?.Invoke();
  }
  public void InvokeWaveFinished()
  {
    OnWaveFinished?.Invoke();
  }
  public void InvokeWavePreparing()
  {
    OnWavePreparing?.Invoke();
  }
  public void InvokeWaveTimeChanged(float time)
  {
    OnWaveTimeChanged?.Invoke(time);
  }
  public void InvokePreparingTimeChanged(float time)
  {
    OnPreparingTimeChanged?.Invoke(time);
  }

  public void SetTime(float time)
  {
    timeRemaining = time;
  }


  public void ChangeWaveState(WaveState newState)
  {
    if (CurrentWaveState == newState) return;
    CurrentWaveState = newState;
    OnWaveStateChanged?.Invoke(CurrentWaveState);
  }

  public void SwitchState(IWaveState newState)
  {
    currentState?.Exit();
    currentState = newState;
    currentState.Enter();
  }
  void Update()
  {

    currentState?.Update();
  }

  private void OnGameStateChanged(GameState state)
  {
    enabled = state == GameState.Gameplay;

  }
}
using UnityEngine;

public class WaveRunningState : IWaveState
{
  private WaveController waveController;
  private EnemyWaveController enemyWaveController;
  private float timeRemaining;

  public WaveRunningState(WaveController waveController, EnemyWaveController enemyWaveController)
  {
    this.waveController = waveController;
    this.enemyWaveController = enemyWaveController;
  }
  public void Update()
  {
    timeRemaining -= Time.deltaTime;
    waveController.InvokeWaveTimeChanged(timeRemaining);
    if (timeRemaining <= 0f)
    {
      waveController.SwitchState(waveController.FinishedState);
    }
  }

  public void Exit()
  {
    enemyWaveController.OnAllEnemiesDefeated -= HandleAllEnemiesDefeated;

  }

  public void Enter()
  {

    timeRemaining = waveController.TimeRemaining;
    waveController.ChangeWaveState(WaveController.WaveState.Running);
    waveController.InvokeWaveStarted();
    enemyWaveController.OnAllEnemiesDefeated += HandleAllEnemiesDefeated;
    enemyWaveController.Spawn();
  }
  private void HandleAllEnemiesDefeated()
  {
    waveController.SwitchState(waveController.FinishedState);
  }
}
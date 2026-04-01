
using System;
using UnityEngine;

public class WavePreparingState : IWaveState
{
  private WaveController waveController;
  private WaveMenuControllerUI menuControllerUI;
  private bool isTimer = false;
  [NonSerialized] public float timer;
  public WavePreparingState(WaveController waveController, WaveMenuControllerUI menuControllerUI)
  {
    this.waveController = waveController;
    this.menuControllerUI = menuControllerUI;

  }
  public void Update()
  {
    if (!isTimer) return;
    timer -= Time.deltaTime;
    waveController.InvokePreparingTimeChanged(timer);
    if (timer <= 0 && isTimer)
    {
      isTimer = false;
      waveController.SwitchState(waveController.RunningState);

    }
  }

  public void Exit()
  {
    menuControllerUI.OnClickedStartWave -= StartTimer;

  }

  public void Enter()
  {
    waveController.ChangeWaveState(WaveController.WaveState.Preparing);
    menuControllerUI.OnClickedStartWave += StartTimer;
    timer = waveController.maxAmountTimer;
    waveController.InvokeWavePreparing();
  }

  public void StartTimer()
  {
    isTimer = true;
    menuControllerUI.HideWavePanel();
    waveController.InvokePreparingTimeChanged(timer);
  }
}
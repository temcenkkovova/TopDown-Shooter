public class WaveFinishedState : IWaveState
{
  private WaveController waveController;
  public WaveFinishedState(WaveController waveController)
  {
    this.waveController = waveController;
  }
  public void Update()
  {
    waveController.SwitchState(waveController.PreparingState);
  }

  public void Exit()
  {

  }

  public void Enter()
  {
    waveController.ChangeWaveState(WaveController.WaveState.Finished);
    waveController.InvokeWaveFinished();

  }
}
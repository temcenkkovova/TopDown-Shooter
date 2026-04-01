using UnityEngine;

public class WaveReward : MonoBehaviour
{


  private WaveController waveController;
  private GameFlowController gameFlowController;

  void Awake()
  {
    waveController = GetComponent<WaveController>();
    waveController.OnWaveFinished += ApplyWaveReward;
    gameFlowController = GetComponent<GameFlowController>();
  }

  void OnDisable()
  {
    waveController.OnWaveFinished -= ApplyWaveReward;
  }

  public void ApplyWaveReward()
  {
    GameEconomy.Instance.CoinForWave(gameFlowController.CurrentWaveConfig.waveFinishReward);
  }
}
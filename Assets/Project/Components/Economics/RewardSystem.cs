using UnityEngine;

public class RewardSystem : MonoBehaviour
{
  public PlayerLevel playerLevel;
  public GameFlowController flowController;
  private int waveIndex => flowController.CurrentIndexWave + 1;
  public void HandleCoinReward(float coin)
  {
    GameEconomy.Instance.CoinForKill(coin);

  }
  public void HandleExpReward(float exp)
  {
    float value = waveIndex * exp;
    playerLevel.AddExp(value);
  }

}
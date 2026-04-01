using System;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
  private float totalCoin;
  public float TotalCoin => totalCoin;
  public event Action<float> OnTotalCoinChanged;
  public EconomyConfig config;
  public static GameEconomy Instance;
  private IWaveProvider waveProvider;
  void Awake()
  {
    Instance = this;
  }
  void Start()
  {

    Init(config);
  }
  public void Init(EconomyConfig economyConfig)
  {
    AddCoin(economyConfig.coins);


  }
  public void InitProvider(IWaveProvider provider)
  {
    waveProvider = provider;
  }
  public void AddCoin(float coinAmount)
  {

    totalCoin += coinAmount;
    OnTotalCoinChanged?.Invoke(totalCoin);

  }


  public bool TrySpendCoin(float amount)
  {
    if (totalCoin >= amount)
    {
      totalCoin -= amount;
      OnTotalCoinChanged?.Invoke(totalCoin);
      return true;
    }
    else
    {
      Debug.Log("You don`t have enough money");
      return false;
    }
  }

  public bool HasMoney(float amount)
  {
    return totalCoin >= amount;
  }

  public void CoinForKill(float amount)
  {

    float reward = amount + waveProvider.CurrentIndexWave * config.Growth;
    totalCoin += reward;
    OnTotalCoinChanged?.Invoke(totalCoin);
  }
  public void CoinForWave(float amount)
  {

    float reward = amount + waveProvider.CurrentIndexWave * config.rewardMultiplierPerWave;
    totalCoin += reward;
    OnTotalCoinChanged?.Invoke(totalCoin);
  }

}
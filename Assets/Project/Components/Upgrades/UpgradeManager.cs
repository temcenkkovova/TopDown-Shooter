using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
  public PlayerStats playerStats;
  public bool CanUpdate; // Надо для того что бы только одно улучшение можно было купить между волнами

  public event Action<bool> OnUpdateStatusChanged;
  public PlayerLevel playerLevel;
  private WaveController waveController;

  void Awake()
  {
    waveController = GetComponent<WaveController>();
    if (waveController != null)
      waveController.OnWaveStarted += ResetUpdateData;
  }

  void OnDisable()
  {
    if (waveController != null)
      waveController.OnWaveStarted -= ResetUpdateData;
  }

  public void Init(PlayerStats playerData)
  {
    playerStats = playerData;
  }
  public void Upgrade(PlayerUpgradeConfig upgradeConfig)
  {
    float costToUpgrade = upgradeConfig.cost * Mathf.Pow(1.25f, playerLevel.Level);
    if (!upgradeConfig.CanApply(playerStats) || !CanUpdate || !GameEconomy.Instance.HasMoney(costToUpgrade)) return;

    upgradeConfig.Apply(playerStats);

    GameEconomy.Instance.TrySpendCoin(costToUpgrade);
    CanUpdate = false;
    OnUpdateStatusChanged?.Invoke(CanUpdate);
  }

  public void ResetUpdateData()
  {
    CanUpdate = true;
    OnUpdateStatusChanged?.Invoke(CanUpdate);
  }
}
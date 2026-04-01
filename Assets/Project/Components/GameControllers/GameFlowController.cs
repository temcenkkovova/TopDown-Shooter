using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour, IWaveProvider
{
  public List<WaveConfig> waveConfigs;
  private int currentIndexWave;

  private WaveController waveController;
  private EnemyWaveController enemyWaveController;
  private WaveMenuControllerUI waveMenuControllerUI;
  public event Action<int> OnWaveIndexChanged;
  public WaveConfig CurrentWaveConfig => waveConfigs[currentIndexWave];

  public int CurrentIndexWave => currentIndexWave;



  void Awake()
  {
    enemyWaveController = GetComponent<EnemyWaveController>();
    waveController = GetComponent<WaveController>();
    waveMenuControllerUI = GetComponent<WaveMenuControllerUI>();
    waveController.OnWaveFinished += ChangeIndexWave;
    waveController.OnWaveStarted += SetDataWave;
    waveController.SetTime(CurrentWaveConfig.waveDuration);
  }

  void OnDisable()
  {
    waveController.OnWaveFinished -= ChangeIndexWave;
    waveController.OnWaveStarted -= SetDataWave;

  }
  public void SetDataWave()
  {
    if (waveConfigs == null) return;
    if (currentIndexWave == waveConfigs.Count) return;
    waveMenuControllerUI.Init(CurrentWaveConfig);
    enemyWaveController.SetEnemies(CurrentWaveConfig.EnemiesConfig, CurrentWaveConfig.radiusSpawn, CurrentWaveConfig.spawnInterval);
  }

  public void ChangeIndexWave()
  {
    if (waveConfigs == null) return;
    if (currentIndexWave >= waveConfigs.Count)
    {
      currentIndexWave = 0;
      OnWaveIndexChanged?.Invoke(currentIndexWave);
    }
    else
    {
      currentIndexWave++;
      OnWaveIndexChanged?.Invoke(currentIndexWave);
    }

  }
}
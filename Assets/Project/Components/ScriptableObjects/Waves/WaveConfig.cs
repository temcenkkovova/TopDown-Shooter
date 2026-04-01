using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Basic")]
public class WaveConfig : ScriptableObject
{
  public float spawnInterval;
  public float radiusSpawn;
  public List<EnemyConfig> EnemiesConfig;
  public float waveDuration;
  public float waveFinishReward;
  public bool hasBoss;
  public int indexWave;
  public List<PlayerUpgradeConfig> playerUpgradeConfigs;
}
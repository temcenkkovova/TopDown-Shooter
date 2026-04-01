using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/SpawnEnemy")]
public class SpawnEnemyAbilityConfig : BossAbilityConfig
{
  public List<EnemyConfig> enemyConfigs;
  public float spawnInterval;


}
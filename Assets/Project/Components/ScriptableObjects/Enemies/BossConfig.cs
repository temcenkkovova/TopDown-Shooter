
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Boss")]
public class BossConfig : EnemyConfig
{
  public List<BossAbilityConfig> abilityConfigs;
  public List<DropData> dropItems;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Upgrade/HP")]
public class UpgradeHP : PlayerUpgradeConfig
{
  public float addHp;
  public override bool CanApply(PlayerStats stats) => true;
  public override void Apply(PlayerStats stats)
  {
    stats.Health += addHp;

  }



}
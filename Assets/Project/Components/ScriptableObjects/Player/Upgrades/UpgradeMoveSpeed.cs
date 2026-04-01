using UnityEngine;

[CreateAssetMenu(menuName = "Player/Upgrade/MovSpeed")]
public class UpgradeMoveSpeed : PlayerUpgradeConfig
{
  public float addMoveSpeed;
  public override bool CanApply(PlayerStats stats) => true;
  public override void Apply(PlayerStats stats)
  {
    stats.MoveSpeed += addMoveSpeed;

  }

}
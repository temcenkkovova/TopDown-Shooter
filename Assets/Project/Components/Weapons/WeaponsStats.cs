using UnityEngine;

public class WeaponsStats
{
  public float Damage;
  public float FireRate;
  public float UpdateLevelCost;

  public WeaponsStats(WeaponLevelData config)
  {

    Damage = config.damage;
    FireRate = config.fireRate;
    UpdateLevelCost = config.updateLevelCost;


  }

}
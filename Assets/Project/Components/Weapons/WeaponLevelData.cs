using System.Collections.Generic;

[System.Serializable]
public class WeaponLevelData
{
  public float damage;
  public float fireRate;
  public float updateLevelCost;

  public List<WeaponAbilityConfig> abilities;
}
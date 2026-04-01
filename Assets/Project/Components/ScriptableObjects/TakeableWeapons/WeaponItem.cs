using UnityEngine;

[CreateAssetMenu(
    fileName = "WeaponItem",
    menuName = "Takeable/Weapon"
)]
public class WeaponItem : Item
{
  public AttackConfig attackConfig;
  public RarityConfig rarityConfig;
}
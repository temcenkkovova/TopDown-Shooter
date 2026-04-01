using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Slam")]
public class SlamAbilityConfig : BossAbilityConfig
{
  public AbilityProjectile indicatorPrefab;
  public GameObject effectPrefab;
  public LayerMask layerMask;
  public float windupTime;
  public float activeTime;

}
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Ranged")]
public class RangedAttackConfig : AttackConfig
{
  public float projectileSpeed;
  public Projectile projectilePrefab;
  public float radius;
  public float maxAmountProjectile;
  public float reloadTime;
  public float projectileTimeLife;



}
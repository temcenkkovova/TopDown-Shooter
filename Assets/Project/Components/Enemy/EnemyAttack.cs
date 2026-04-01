using System;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  private AttackConfig config;
  private RangedAttackConfig rangedAttack;
  public GameObject projectileStartPoint;
  private Transform playerTr;
  private WeaponsStats weaponsStats;
  private float nextFireTime;
  public event Action OnShoot;
  public bool finishedAttack = true;

  public void Init(AttackConfig config, Transform playerTransform)
  {
    this.config = config;
    playerTr = playerTransform;
    weaponsStats = new WeaponsStats(config.levels[0]);
    if (config is RangedAttackConfig ranged)
    {
      rangedAttack = ranged;

    }
  }

  public void Shoot()
  {
    if (Time.time < nextFireTime) return;
    Vector3 dir = (playerTr.position - projectileStartPoint.transform.position).normalized;
    nextFireTime = Time.time + 1f / weaponsStats.FireRate;
    if (rangedAttack != null)
    {
      var ownerCollider = GetComponent<Collider>();
      var ownerTag = gameObject.tag;

      Projectile projectile = Instantiate(
          rangedAttack.projectilePrefab,
          projectileStartPoint.transform.position,
          projectileStartPoint.transform.rotation);

      //dir.y = transform.position.y;
      dir.y = 0f;
      dir.Normalize();
      projectile.Init(weaponsStats, rangedAttack, dir, ownerCollider, ownerTag);
      OnShoot?.Invoke();
    }
    else
    {
      if (!finishedAttack) return;
      IDamageable damageable = playerTr.GetComponent<IDamageable>();
      damageable.TakeDamage(weaponsStats.Damage);
      finishedAttack = false;
      OnShoot?.Invoke();

      StartCoroutine(FinishAttackTime());
    }

  }
  public IEnumerator FinishAttackTime()
  {
    yield return new WaitForSeconds(1f);
    finishedAttack = true;
  }
}
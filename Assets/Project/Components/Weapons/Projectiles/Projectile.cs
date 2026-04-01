using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  private Rigidbody projectileRigidbody;
  private RangedAttackConfig rangeStats;
  private WeaponsStats baseStats;
  private float lifeTimer;
  private Collider projectileCollider;
  private Collider ownerCollider;
  [NonSerialized] public string ownerProjectileTag;
  public GameObject hitEffectPrefab;
  void Awake()
  {

    projectileCollider = GetComponent<Collider>();
    projectileRigidbody = GetComponent<Rigidbody>();
  }
  public void Init(WeaponsStats weaponsStats, RangedAttackConfig rangedConfig, Vector3 direction, Collider owner, string ownerTag)
  {
    rangeStats = rangedConfig;
    baseStats = weaponsStats;
    ownerCollider = owner;
    ownerProjectileTag = ownerTag;
    if (ownerCollider != null && projectileCollider != null)
    {
      Physics.IgnoreCollision(projectileCollider, ownerCollider, true);
    }
    projectileRigidbody.velocity = direction * rangeStats.projectileSpeed;
    lifeTimer = rangeStats.projectileTimeLife;

  }
  public bool IsOwner(Collider other) => ownerCollider != null && other == ownerCollider;
  void Update()
  {
    lifeTimer -= Time.deltaTime;
    if (lifeTimer <= 0f)
    {
      Destroy(gameObject);
    }
  }

  public void ReleaseAttack(IDamageable target)
  {

    target.TakeDamage(baseStats.Damage);


  }
}
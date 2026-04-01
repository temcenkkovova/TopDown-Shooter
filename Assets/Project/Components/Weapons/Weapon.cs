using System;
using System.Collections;
using UnityEngine;


public class Weapon : MonoBehaviour
{

  private RangedAttackConfig rangedConfig;
  private int level;
  public int Level => level;
  private float projectiles;

  public float Projectiles => projectiles;
  [NonSerialized] public float maxProjectiles;
  public event Action<float> ProjectileChanged;
  public event Action WeaponStatsChanged;
  [NonSerialized] public WeaponsStats weaponStats;
  public bool IsReloading;
  private Collider ownerCollider;
  private string ownerTag;
  private float nextFireTime;
  public Transform projectileSpawnPoint;
  public event Action Fired;

  private Coroutine reloadCoroutine;
  public event Action ReloadWeapon;
  public event Action<int> OnLevelChanged;

  public float CostUpdateLevel => weaponStats.UpdateLevelCost;
  [SerializeField] private LineRenderer lineRenderer;
  [SerializeField] private float rayDistance = 50f;

  void Awake()
  {
    ownerCollider = GetComponent<Collider>();
    ownerTag = gameObject.tag;
    lineRenderer = GetComponent<LineRenderer>();
  }
  public void Init(WeaponLevelData data)
  {
    if (data == null) return;
    weaponStats = new WeaponsStats(data);
    WeaponStatsChanged?.Invoke();

  }
  public void InitRange(RangedAttackConfig range)
  {
    rangedConfig = range;
    projectiles = range.maxAmountProjectile;
    maxProjectiles = range.maxAmountProjectile;
    ProjectileChanged?.Invoke(projectiles);
  }

  public void TryFire()
  {

    if (Time.time < nextFireTime)
      return;
    if (IsReloading) return;

    if (rangedConfig != null)
    {
      if (projectiles <= 0 && !IsReloading)
      {
        StartReload();
        return;
      }

      nextFireTime = Time.time + 1f / weaponStats.FireRate;
      Fire();
    }
    else
    {
      Debug.Log("Melee Attack will be in future");
    }
  }

  public void Fire()
  {
    Projectile projectile = Instantiate(rangedConfig.projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
    projectile.Init(weaponStats, rangedConfig, projectileSpawnPoint.transform.forward, ownerCollider, ownerTag);
    projectiles--;
    ProjectileChanged?.Invoke(projectiles);
    Fired?.Invoke();

  }

  public void StartReload()
  {
    if (rangedConfig == null) return;
    if (reloadCoroutine != null) return;
    reloadCoroutine = StartCoroutine(Reload());
  }

  private IEnumerator Reload()
  {

    if (projectiles == maxProjectiles)
    {
      Debug.Log("You have all bullets");
      yield break;
    }
    IsReloading = true;
    ReloadWeapon?.Invoke();

    yield return new WaitForSeconds(rangedConfig.reloadTime);
    projectiles = maxProjectiles;
    ProjectileChanged?.Invoke(projectiles);
    IsReloading = false;
    reloadCoroutine = null;
    ReloadWeapon?.Invoke();

  }
  public void UpdateLevel()
  {
    level++;
    GameEconomy.Instance.TrySpendCoin(CostUpdateLevel);
    // OnLevelChanged?.Invoke(level);


  }
  public void TryUpdateWeaponLevel()
  {
    if (GameEconomy.Instance.HasMoney(CostUpdateLevel))
    {
      UpdateLevel();

    }
    else
    {
      Debug.Log("You don`t have money to update weapon Level");
    }
  }

  void Update()
  {
    DrawAimLine();
  }

  void DrawAimLine()
  {
    Vector3 start = projectileSpawnPoint.position;
    Vector3 end = start + projectileSpawnPoint.forward * rayDistance;

    if (Physics.Raycast(start, projectileSpawnPoint.forward, out RaycastHit hit, rayDistance))
    {
      end = hit.point;
    }

    lineRenderer.SetPosition(0, start);
    lineRenderer.SetPosition(1, end);
  }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWeaponController : MonoBehaviour, IWeaponEquipment
{
  public GameObject weaponGrid;
  [NonSerialized] public Sprite weaponIcon;

  [NonSerialized] public AttackConfig attackConfig;
  [NonSerialized] public RarityConfig rareConfig;
  public WeaponItem startWeapon;

  [NonSerialized] public Weapon currentWeapon;

  private Animator animator;
  private WeaponAudio weaponAudio;
  public event Action OnWeaponChanged;
  public event Action OnWeaponUpdate;
  public event Action OnProjectileChanged;


  void Awake()
  {
    EquipWeapon(startWeapon);


  }
  void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void EquipWeapon(WeaponItem itemWeapon)
  {
    if (itemWeapon == null) return;
    if (currentWeapon != null) // надо удалять старое оружие
    {
      currentWeapon.Fired -= OnWeaponFired;
      currentWeapon.ReloadWeapon -= OnWeaponReload;
      Destroy(currentWeapon.gameObject);
    }
    AttackConfig config = itemWeapon.attackConfig;
    rareConfig = itemWeapon.rarityConfig;
    attackConfig = config;
    currentWeapon = Instantiate(config.weaponPrefab, weaponGrid.transform);
    weaponAudio = null;
    weaponAudio = currentWeapon.GetComponent<WeaponAudio>();
    currentWeapon.transform.localPosition = Vector3.zero;
    currentWeapon.transform.localRotation = Quaternion.identity;
    WeaponLevelData data = config.levels[0];
    weaponIcon = itemWeapon.icon;
    currentWeapon.Init(data);
    OnWeaponChanged?.Invoke();


    if (config is RangedAttackConfig ranged)
      currentWeapon.InitRange(ranged);
    OnProjectileChanged?.Invoke();
    currentWeapon.Fired += OnWeaponFired;
    currentWeapon.ReloadWeapon += OnWeaponReload;

  }

  void OnDestroy()
  {
    if (currentWeapon == null) return;
    currentWeapon.Fired -= OnWeaponFired;
    currentWeapon.ReloadWeapon -= OnWeaponReload;
  }

  public void HandleFire()
  {
    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
      return;
    if (currentWeapon == null) return;
    currentWeapon.TryFire();
  }
  public void HandleReload()
  {
    currentWeapon.StartReload();
  }
  public void HandleUpdate()
  {
    if (!GameEconomy.Instance.HasMoney(currentWeapon.CostUpdateLevel)) return;
    if (currentWeapon.Level + 1 >= attackConfig.levels.Count) return;

    currentWeapon.UpdateLevel();
    currentWeapon.Init(attackConfig.levels[currentWeapon.Level]);
    OnWeaponUpdate?.Invoke();
    // InitWeapon();
  }

  public void OnWeaponFired()
  {

    if (attackConfig.muzzleFlashPrefab != null && weaponAudio != null)
    {
      OnProjectileChanged?.Invoke();
      weaponAudio.PlayFired();
      Instantiate(attackConfig.muzzleFlashPrefab, currentWeapon.projectileSpawnPoint.position, currentWeapon.projectileSpawnPoint.rotation);

    }
    animator.SetTrigger("IsFiring");
  }
  public void OnWeaponReload()
  {
    animator.SetTrigger("IsReloading");
  }

  public void Equip(Item item)
  {
    if (item is WeaponItem weaponItem)
    {
      EquipWeapon(weaponItem);
    }

  }
}
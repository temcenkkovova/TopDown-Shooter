using TMPro;
using UnityEngine;

public class ProjectileWeaponUI : MonoBehaviour
{
  public TMP_Text projectileField;

  public PlayerWeaponController weaponController;

  void Start()
  {
    weaponController.OnProjectileChanged += ShowProjectile;
    ShowProjectile();
  }

  void OnDisable()
  {
    weaponController.OnProjectileChanged -= ShowProjectile;
  }

  public void ShowProjectile()
  {
    projectileField.text = weaponController.currentWeapon.Projectiles + " / " + weaponController.currentWeapon.maxProjectiles;
  }


}
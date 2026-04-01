using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class WeaponStatsUI : MonoBehaviour
{
  public TMP_Text levelField;
  public TMP_Text damageField;
  public TMP_Text rarityField;
  public Image imageField;

  public PlayerWeaponController weaponController;

  void Awake()
  {
    weaponController.OnWeaponUpdate += ShowNewUpdateData;
    weaponController.OnWeaponChanged += ShowNewWeaponData;
  }
  void OnDisable()
  {

    weaponController.OnWeaponUpdate -= ShowNewUpdateData;
    weaponController.OnWeaponChanged -= ShowNewWeaponData;
  }
  void Start()
  {
    ShowNewWeaponData();
    /*МОжет быть надо будет так сделать при старте что бы отобразились сразу данные */
  }

  public void ShowNewWeaponData()
  {

    damageField.text = weaponController.currentWeapon.weaponStats.Damage.ToString();
    levelField.text = (weaponController.currentWeapon.Level + 1).ToString();
    rarityField.text = weaponController.rareConfig.rarity.ToString();
    rarityField.color = weaponController.rareConfig.color;
    imageField.sprite = weaponController.weaponIcon;
  }
  public void ShowNewUpdateData()
  {
    damageField.text = weaponController.currentWeapon.weaponStats.Damage.ToString();
    levelField.text = (weaponController.currentWeapon.Level + 1).ToString();

  }
}
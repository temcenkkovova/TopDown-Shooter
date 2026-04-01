using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapon")]
public class AttackConfig : ScriptableObject
{
  public List<WeaponLevelData> levels;
  public Weapon weaponPrefab;
  public ParticleSystem muzzleFlashPrefab;


}
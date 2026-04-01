
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Basic")]
public class EnemyConfig : ScriptableObject
{
  public Enemy prefab;
  public int maxHealth;
  public int coinReward;
  public float xpReward;
  public float moveSpeed;
  public float rangeAttack;
  public AttackConfig attackConfig;
}
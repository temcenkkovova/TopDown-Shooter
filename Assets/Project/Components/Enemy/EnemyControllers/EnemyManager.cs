using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [NonSerialized] public List<Enemy> spawnedEnemies = new();

  public event Action<int> OnEnemyAmountChanged;
  public int maxAmount;
  public void AddEnemy(Enemy enemy)
  {
    spawnedEnemies.Add(enemy);



  }

  public void RemoveEnemy(Enemy enemy)
  {
    if (!spawnedEnemies.Contains(enemy)) return;
    spawnedEnemies.Remove(enemy);
    maxAmount--;
    OnEnemyAmountChanged?.Invoke(maxAmount);
  }

  public void ChangeMaxAmount(int amount)
  {
    maxAmount += amount;
    OnEnemyAmountChanged?.Invoke(maxAmount);
  }
}
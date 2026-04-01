using System.Collections.Generic;
using UnityEngine;

public class DropItemController : MonoBehaviour
{
  private List<DropData> dropItems = new();
  private Enemy enemy;

  public static DropItemController Instance;


  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }
  public void Init(List<DropData> items)
  {
    dropItems = items;

  }
  public void SetEnemy(Enemy enemy)
  {
    this.enemy = enemy;
    enemy.OnEnemyDied += DropItem;
  }

  public void DropItem(Enemy enemy)
  {
    Vector3 pos = enemy.transform.position;
    if (dropItems.Count == 0) return;
    float totalWeight = 0f;

    foreach (var drop in dropItems)
      totalWeight += drop.dropChance;

    float randomPoint = Random.value * totalWeight;

    float current = 0f;
    foreach (var drop in dropItems)
    {
      current += drop.dropChance;
      if (randomPoint <= current)
      {
        SpawnDropItem(drop, pos);
        return;
      }
    }


  }
  public void SpawnDropItem(DropData drop, Vector3 position)
  {
    GameObject item = Instantiate(drop.item.prefab, position, Quaternion.identity);
    DropItemRarity itemRarity = item.GetComponent<DropItemRarity>();
    if (itemRarity)
    {
      itemRarity.SetRarity(drop.rarity);
    }
  }

}